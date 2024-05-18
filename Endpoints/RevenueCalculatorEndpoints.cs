using System.Net.Mime;
using CafeRevenueCalculatorApi.Models;
using FluentValidation;

namespace CafeRevenueCalculatorApi.Endpoints
{
    public static class RevenueCalculatorEndpoints
    {
        public static void MapRevenueCalculatorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/revenue/calculate", CalculateRevenue)
                .WithDescription("Calculate cafe revenue")
                .Accepts<RevenueCalculatorRequest>(MediaTypeNames.Application.Json)
                .Produces<string>(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Calculate cafe revenue
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/revenue/calculate
        ///     {
        ///        "TotalRevenue": 1000,
        ///        "RevenueFromKaspi": 500,
        ///        "RevenueFromOtherSources": 500,
        ///        "RevenueFromCash": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">All calculations are correct</response>
        /// <response code="400">Please check entered data</response>
        private static IResult CalculateRevenue(IValidator<RevenueCalculatorRequest> validator, RevenueCalculatorRequest request)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var total = request.TotalRevenue == request.RevenueFromKaspi + request.RevenueFromOtherSources + request.RevenueFromCash;
            if (total)
                return Results.Ok("All calculations are correct");

            return Results.BadRequest("Please check entered data");
        }
    }
}