using System.Net.Http.Json;
using Salino.DataLayer;

namespace SalinoMvc5.SalinoUtilities
{
    public class SmsIrOptions
    {
        public string BaseUrl { get; set; } = "https://RestfulSms.com/api/";
        public string UserApiKey { get; set; }
        public string SecretKey { get; set; }
    }

    /// <summary>
    /// Sends the activation code through sms.ir (RestfulSms API, the same calls the old
    /// SmsIrRestful .NET Framework package made: Token + VerificationCode).
    /// </summary>
    public static class SmsService
    {
        private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(20) };

        public static SmsIrOptions Options { get; set; } = new();

        #region SendSms
        public static int SendSms(int? userid)
        {
            using var db = new SalinoContext();
            var user = db.users.Find(userid);
            if (user == null) return 0;

            try
            {
                var baseUrl = Options.BaseUrl.TrimEnd('/') + "/";

                var tokenResponse = Http.PostAsJsonAsync(baseUrl + "Token",
                        new { UserApiKey = Options.UserApiKey, SecretKey = Options.SecretKey })
                    .GetAwaiter().GetResult();
                var token = tokenResponse.Content.ReadFromJsonAsync<TokenResult>().GetAwaiter().GetResult();
                if (token == null || !token.IsSuccessful || string.IsNullOrEmpty(token.TokenKey)) return 0;

                using var request = new HttpRequestMessage(HttpMethod.Post, baseUrl + "VerificationCode")
                {
                    Content = JsonContent.Create(new { Code = user.CodeActivate, MobileNumber = user.Mobile }),
                };
                request.Headers.Add("x-sms-ir-secure-token", token.TokenKey);
                var sendResponse = Http.Send(request);
                var result = sendResponse.Content.ReadFromJsonAsync<SendResult>().GetAwaiter().GetResult();
                return result is { IsSuccessful: true } ? 1 : 0;
            }
            catch (HttpRequestException)
            {
                return 0;
            }
            catch (TaskCanceledException)
            {
                return 0;
            }
        }
        #endregion

        private sealed class TokenResult
        {
            public string TokenKey { get; set; }
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }

        private sealed class SendResult
        {
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }
    }
}
