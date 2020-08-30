using SyncthingStatus.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SyncthingStatus
{
    class ApiClient
    {
        private static readonly string X_API_KEY = "X-Api-Key";

        private static HttpClient client;

        internal static void Initialize()
        {
            if (client != null)
            {
                client.Dispose();
            }

            client = new HttpClient
            {
                BaseAddress = new System.Uri(Util.GetSyncthingAddress())
            };
            client.DefaultRequestHeaders.Add(X_API_KEY, Properties.Settings.Default.ApiKey);

            System.Diagnostics.Debug.WriteLine("[HttpClient] Address: " + Util.GetSyncthingAddress());
            System.Diagnostics.Debug.WriteLine("[HttpClient] API key: " + Properties.Settings.Default.ApiKey);
        }

        internal static async Task<PingResponse> Ping()
        {
            try
            {
                var result = await client.GetFromJsonAsync<PingResponse>("/rest/system/ping");
                return result;
            } catch(HttpRequestException)
            {
                return null;
            }
        }

        internal static async Task<VersionResponse> Version()
        {
            try
            {
                var result = await client.GetFromJsonAsync<VersionResponse>("/rest/system/version");
                return result;
            } catch (HttpRequestException)
            {
                return null;
            }
        }

        internal static async Task<ErrorResponse.Error[]> Error()
        {
            try
            {
                var errors = new { Errors = new ErrorResponse.Error[0] };
                var result = await client.GetFromJsonAsync<ErrorResponse>("/rest/system/error");
                return result.Errors;
            } catch(HttpRequestException)
            {
                return null;
            }
        }
    }
}
