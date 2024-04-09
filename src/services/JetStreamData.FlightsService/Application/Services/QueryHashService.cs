using System.Security.Cryptography;
using System.Text;
using JetStreamData.FlightsService.Application.Queries.Models;

namespace JetStreamData.FlightsService.Application.Services;

public static class QueryHashService
{
    public static string GenerateUniqueName(SearchFlights.Query query)
    {
        var inputString = $"SearchFlightsQuery:{query.Keyword}:{query.Page}:{query.PageSize}:{query.OrderBy}:{query.Direction}";
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(inputString));

        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}
