using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitiFine.Areas.Identity.Data;
using CitiFine.Models;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using Microsoft.Extensions.Options;

namespace CitiFine.Controllers
{
    [Authorize]  // This ensures only logged-in users can access the controller actions
    public class ViolationsController : Controller
    {
        private readonly CitiFineDbContext _context;
        private readonly EmailService _emailService;
        private readonly IOptions<StripeSettings> _stripeSettings;

        public ViolationsController(CitiFineDbContext context, IOptions<StripeSettings> stripeSettings, EmailService emailService)
        {
            _context = context;
            _stripeSettings = stripeSettings;
            _emailService = emailService;
        }

        // GET: Violations
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // If the user is an Admin or Officer, show all violations
            if (User.IsInRole("Administrator") || User.IsInRole("Officer"))
            {
                var allViolations = _context.Violations.Include(v => v.User);
                return View(await allViolations.ToListAsync());
            }
            else
            {
                // If the user is a regular user, only show their own violations
                var userViolations = _context.Violations
                    .Include(v => v.User)
                    .Where(v => v.UserId == currentUserId);

                return View(await userViolations.ToListAsync());
            }
        }

        // GET: Violations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ViolationId == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // GET: Violations/Create
        [Authorize(Policy = "RequireOfficer")]
        public IActionResult Create(string? licensePlate = null)
        {
            // List of violation types
            var violationTypes = new List<string>
            {
                "Speeding",
                "Red Light",
                "Stop Sign Violation",
                "Illegal Parking",
                "Distracted Driving"
            };

            // Passing violation types to the view
            ViewBag.ViolationTypes = new SelectList(violationTypes);

            // Retrieve users with license plates
            ViewBag.LicensePlates = new SelectList(_context.Users
                .Where(u => !string.IsNullOrEmpty(u.LicensePlate))
                .Select(u => new { u.LicensePlate, Display = u.LicensePlate })
                .Distinct(), "LicensePlate", "Display", licensePlate);

            // Find the user associated with the selected license plate
            var selectedUser = _context.Users.FirstOrDefault(u => u.LicensePlate == licensePlate);

            // Populate UserId hidden dropdown based on the selected license plate
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Id", selectedUser?.Id);

            return View();
        }

        // POST: Violations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireOfficer")]
        public async Task<IActionResult> Create([Bind("ViolationId,ViolationType,FineAmount,DateIssued,UserId,IsPaid")] Violation violation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violation);
                await _context.SaveChangesAsync();

                // Fetch user details
                var user = await _context.Users.FindAsync(violation.UserId);
                if (user != null)
                {
                    string subject = "New Violation Ticket Issued";
                    string body = $"Dear {user.FirstName},<br><br>You have received a new violation ticket:<br>" +
                                  $"<strong>Violation ID:</strong> {violation.ViolationId}<br>" +
                                  $"<strong>Violation Type:</strong> {violation.ViolationType}<br>" +
                                  $"<strong>Fine Amount:</strong> {violation.FineAmount:C}<br>" +
                                  $"<strong>Date Issued:</strong> {violation.DateIssued}<br><br>" +
                                  $"To pay your fine, please go to your account and navigate to violation details.<br><br>" +
                                  $"–Citifine Admin";

                    await _emailService.SendEmailAsync(user.Email, subject, body);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", violation.UserId);
            return View(violation);
        }

        // GET: Violations/Edit/5
        [Authorize(Policy = "RequireOfficer")]
        public async Task<IActionResult> Edit(int? id)
        {
            // List of violation types
            var violationTypes = new List<string>
            {
                "Speeding",
                "Red Light",
                "Stop Sign Violation",
                "Illegal Parking",
                "Distracted Driving"
            };

            // Passing violation types to the view
            ViewBag.ViolationTypes = new SelectList(violationTypes);

            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations.FindAsync(id);
            if (violation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", violation.UserId);
            return View(violation);
        }

        // POST: Violations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireOfficer")]
        public async Task<IActionResult> Edit(int id, [Bind("ViolationId,ViolationType,FineAmount,DateIssued,UserId,IsPaid")] Violation violation)
        {
            if (id != violation.ViolationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationExists(violation.ViolationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", violation.UserId);
            return View(violation);
        }

        // GET: Violations/Delete/5
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ViolationId == id);
            if (violation == null)
            {
                return NotFound();
            }

            return View(violation);
        }

        // POST: Violations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var violation = await _context.Violations.FindAsync(id);
            if (violation != null)
            {
                _context.Violations.Remove(violation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationExists(int id)
        {
            return _context.Violations.Any(e => e.ViolationId == id);
        }

        // Stripe Methods ======================================================================================
        // GET: Violations/PayFine/5
        [HttpGet]
        public async Task<IActionResult> PayFine(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var violation = await _context.Violations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ViolationId == id);

            if (violation == null)
            {
                return NotFound();
            }

            // Pass the violation details to the payment page
            return View(violation);
        }
    }
}
