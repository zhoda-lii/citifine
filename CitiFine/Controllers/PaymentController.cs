using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.EntityFrameworkCore;
using CitiFine.Areas.Identity.Data;
using CitiFine.Models;
using Microsoft.Extensions.Options;

public class PaymentController : Controller
{
    private readonly CitiFineDbContext _context;
    private readonly EmailService _emailService;
    private readonly IOptions<StripeSettings> _stripeSettings;

    public PaymentController(CitiFineDbContext context, IOptions<StripeSettings> stripeSettings, EmailService emailService)
    {
        _context = context;
        _stripeSettings = stripeSettings;
        _emailService = emailService;

        StripeConfiguration.ApiKey = _stripeSettings.Value.SecretKey;
    }

    public IActionResult Index(int violationId)
    {
        return RedirectToAction("Details", "Violations", new { id = violationId });
    }

    public async Task<IActionResult> CreateCheckoutSession(int violationId)
    {
        var violation = await _context.Violations
            .FirstOrDefaultAsync(v => v.ViolationId == violationId);

        var fineAmountInCents = (int)(violation.FineAmount * 100);

        var sessionOptions = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = fineAmountInCents,  
                        Currency = "cad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Violation Type: " + violation.ViolationType.ToString(),
                            Description = "Payment For Violation ID: " + violation.ViolationId.ToString()
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = Url.Action("PaymentSuccess", "Payment", new { violationId = violationId }, protocol: Request.Scheme),
            CancelUrl = Url.Action("PaymentCancel", "Payment", new { violationId = violationId }, protocol: Request.Scheme),
        };

        var sessionService = new SessionService();
        Session session = await sessionService.CreateAsync(sessionOptions);

        return Redirect(session.Url); // Redirect to the Stripe Checkout page
    }

    public async Task<IActionResult> PaymentSuccess(int violationId)
    {
        var violation = await _context.Violations.FindAsync(violationId);

        if (violation != null)
        {
            violation.IsPaid = true; // Mark as paid
            _context.Update(violation);
            await _context.SaveChangesAsync();

            // Fetch user details and send email notification
            var user = await _context.Users.FindAsync(violation.UserId);
            if (user != null)
            {
                string subject = "Payment Confirmation for Violation Ticket";
                string body = $"Dear {user.FirstName},<br><br>" +
                               $"We have received your payment for Violation Ticket <strong>{violation.ViolationId}</strong>.<br>" +
                               $"<strong>Violation Type:</strong> {violation.ViolationType}<br>" +
                               $"<strong>Fine Amount:</strong> {violation.FineAmount:C}<br>" +
                               $"<strong>Date Issued:</strong> {violation.DateIssued}<br>" +
                               $"<strong>Date Paid:</strong> {DateTime.Now}<br><br>" +
                               "Thank you for resolving this matter.<br><br>" +
                               "–Citifine Admin";

                await _emailService.SendEmailAsync(user.Email, subject, body);
            }

        }

        ViewBag.ViolationId = violationId;
        ViewBag.Message = "The payment was successfully processed. Thank you for your payment.";

        return View();
    }

    public async Task<IActionResult> PaymentCancel(int violationId)
    {
        var violation = await _context.Violations.FindAsync(violationId);

        if (violation == null)
        {
            return NotFound();
        }

        ViewBag.ViolationId = violationId;
        ViewBag.Message = "You cancelled the payment. The fine is still unpaid.";

        return View();
    }
}
