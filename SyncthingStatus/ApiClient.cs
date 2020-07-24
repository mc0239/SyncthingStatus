using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using SyncthingStatus.Data;
using System.Windows.Forms;

namespace SyncthingStatus
{
    class ApiClient
    {
        private static HttpClient client;

        internal static void Initialize()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Util.GetSyncthingAddress());
            client.DefaultRequestHeaders.Add("X-API-Key", Properties.Settings.Default.ApiKey);
        }

        internal static async Task<Ping> Ping()
        {
            try
            {
                var result = await client.GetFromJsonAsync<Ping>("/rest/system/ping");
                return result;
            } catch(HttpRequestException e)
            {
                return null;
            }
        }

        internal static async Task<Error[]> Error()
        {
            try
            {
                var errors = new { Errors = new Error[0] };
                var result = await client.GetFromJsonAsync<ErrorArr>("/rest/system/error");
                return result.Errors;
            } catch(HttpRequestException e)
            {
                return new Error[0];
            }
        }
    }
}
