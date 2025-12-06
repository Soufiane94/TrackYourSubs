namespace TrackYourSubs.Models;

public enum BillingFrequency { Day = 1, Week = 7, Month = 30, Year = 365 }
public sealed class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "EUR";
    public BillingFrequency Frequency { get; set; } = BillingFrequency.Month;
    public int IntervalCount { get; set; } = 1;                  // e.g., every 2 months
    public DateTime NextBillingDate { get; set; } = DateTime.Today;
    public string? Notes { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
}