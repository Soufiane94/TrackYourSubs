using TrackYourSubs.Models;

namespace TrackYourSubs.Services;

public static class BillingCalculator
{
    public static DateTime ComputeNext(DateTime from, BillingFrequency freq, int interval)
    {
        if (interval < 1) interval = 1;
        return freq switch
        {
            BillingFrequency.Day   => from.AddDays(1 * interval),
            BillingFrequency.Week  => from.AddDays(7 * interval),
            BillingFrequency.Month => from.AddMonths(1 * interval),
            BillingFrequency.Year  => from.AddYears(1 * interval),
            _ => from
        };
    }

    public static decimal MonthlyizedCost(decimal amount, BillingFrequency freq, int interval)
    {
        var per = freq switch
        {
            BillingFrequency.Day   => 30m / interval,
            BillingFrequency.Week  => 4.345m / interval,
            BillingFrequency.Month => 1m / interval,
            BillingFrequency.Year  => 1m / (12m * interval),
            _ => 0m
        };
        return Math.Round(amount * per, 2);
    }
}
