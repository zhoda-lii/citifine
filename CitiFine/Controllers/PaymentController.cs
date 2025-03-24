using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.EntityFrameworkCore;
using CitiFine.Areas.Identity.Data;
using CitiFine.Models;

public class PaymentController : Controller
    {
    private readonly CitiFineDbContext _context;
    public PaymentController(CitiFineDbContext context)
    {
        _context = context;
        StripeConfiguration.ApiKey = "sk_test_51R0wbJ2cWP9xDke2x906NQGPzi6TNxh7S0mNLNq3GLv39C6IOMJXh9x4aHdgQDnqUL2gatnpcf95gBvA90Bv9Hri00odpdkIw8"; // Secret Key from Stripe Dashboard
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
                            Name = "Fine Payment for Violation",
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
        }

        // Pass the violation ID and a success message to the view
        ViewBag.ViolationId = violationId;
        ViewBag.Message = "The payment was successfully processed. Thank you for your payment.";

        return View(); // This will automatically look for PaymentSuccess.cshtml
    }

    public async Task<IActionResult> PaymentCancel(int violationId)
    {
        var violation = await _context.Violations.FindAsync(violationId);
        if (violation == null)
        {
            return NotFound();
        }
        ViewBag.Message = "You canceled the payment. The fine is still unpaid.";
        return View();
    }
}
