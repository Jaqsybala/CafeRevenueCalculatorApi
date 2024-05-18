namespace CafeRevenueCalculatorApi.Models
{
    public record RevenueCalculatorRequest(int TotalRevenue,
                                           int RevenueFromKaspi,
                                           int RevenueFromOtherSources,
                                           int RevenueFromCash);
}