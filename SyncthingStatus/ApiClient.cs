using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyncthingStatus
{
    class ApiClient
    {
        private static HttpClient client;

        internal static void Initialize()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8384/");
            client.DefaultRequestHeaders.Add("X-API-Key", Properties.Settings.Default.ApiKey);
        }

        internal static async Task<string> ping()
        {
            try
            {
                var result = await client.GetStringAsync("/rest/system/ping");
                return result;
            } catch(HttpRequestException e)
            {
                return "";
            }
        }
    }
}
