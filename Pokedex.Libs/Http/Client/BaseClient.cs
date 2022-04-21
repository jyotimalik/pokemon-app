using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Libs.Http.Clients
{
    public abstract class BaseClient
    {
        protected static async Task<T> SendAsync<T>(HttpClient httpClient, HttpRequestMessage requestMessage) where T : new()
        {
            T result = default;
            try
            {
                var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error when sending request to api" + ex.Message;
                throw new Exception(errorMessage);
            }
            return result;
        }
    }
}
