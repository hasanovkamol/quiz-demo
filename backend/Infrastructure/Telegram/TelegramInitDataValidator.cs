using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QuizApi.Infrastructure.Telegram;

public static class TelegramInitDataValidator
{
    public static bool Validate(string initData, string botToken, TimeSpan? maxAge = null)
    {
        if (string.IsNullOrWhiteSpace(initData) || string.IsNullOrWhiteSpace(botToken))
            return false;

        try
        {
            var parsedQuery = HttpUtility.ParseQueryString(initData);
            var hash = parsedQuery["hash"];
            if (string.IsNullOrEmpty(hash))
                return false;

            // Optional auth_date check
            if (maxAge.HasValue && long.TryParse(parsedQuery["auth_date"], out var authDateUnix))
            {
                var authDate = DateTimeOffset.FromUnixTimeSeconds(authDateUnix);
                if (DateTimeOffset.UtcNow - authDate > maxAge.Value)
                {
                    return false;
                }
            }

            // Build data_check_string: sorted key=value lines excluding 'hash'
            var dataCheckList = new List<string>();
            foreach (string? key in parsedQuery.AllKeys)
            {
                if (string.IsNullOrEmpty(key) || key.Equals("hash", StringComparison.OrdinalIgnoreCase))
                    continue;

                dataCheckList.Add($"{key}={parsedQuery[key]}");
            }

            dataCheckList.Sort(StringComparer.Ordinal);
            var dataCheckString = string.Join("\n", dataCheckList);

            // Secret key = HMAC_SHA256("WebAppData", botToken)
            byte[] secretKey = HMACSHA256.HashData(Encoding.UTF8.GetBytes("WebAppData"), Encoding.UTF8.GetBytes(botToken));

            // Calculated hash = HMAC_SHA256(secretKey, dataCheckString)
            byte[] calculatedHashBytes = HMACSHA256.HashData(secretKey, Encoding.UTF8.GetBytes(dataCheckString));
            string calculatedHashHex = Convert.ToHexString(calculatedHashBytes).ToLowerInvariant();

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(calculatedHashHex),
                Encoding.UTF8.GetBytes(hash.ToLowerInvariant())
            );
        }
        catch
        {
            return false;
        }
    }
}
