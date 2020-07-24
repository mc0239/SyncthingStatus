using SyncthingStatus.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SyncthingStatus
{
    class ApiClient
    {
        private static HttpClient client;

        internal static void Initialize()
        {
            client = new HttpClient
            {
                BaseAddress = new System.Uri(Util.GetSyncthingAddress())
            };
            client.DefaultRequestHeaders.Add("X-API-Key", Properties.Settings.Default.ApiKey);
        }

        internal static async Task<PingResponse> Ping()
        {
            try
            {
                var result = await client.GetFromJsonAsync<PingResponse>("/rest/system/ping");
                return result;
            } catch(HttpRequestException e)
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
            } catch(HttpRequestException e)
            {
                return null;
            }
        }
    }
}
