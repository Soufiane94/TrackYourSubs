using TrackYourSubs.Models;

namespace TrackYourSubs.ViewModels;

public static class BillingFrequencyValues
{
    public static BillingFrequency[] All { get; } =
        (BillingFrequency[])Enum.GetValues(typeof(BillingFrequency));
}
