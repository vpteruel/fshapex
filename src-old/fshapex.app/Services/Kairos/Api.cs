using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace fshapex.app.Services.Kairos
{
    public class Api
    {
        // https://developer.kairos.com/admin
        // 25 per minute / 1 million per month
        public string AppId { get; set; } = "27820998";
        public string AppKey { get; set; } = "c2e06534635287e8f62e28983c122cae";
        public string BaseAddress { get; set; } = "https://api.kairos.com/";

        public async Task<Model> GetDataAsync(string uri)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("app_id", AppId);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("app_key", AppKey);

                using (var content = new StringContent(""))
                {
                    using (var response = await httpClient.PostAsync($"v2/media?source={uri}&landmarks=1", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var data = Model.FromJson(responseData);
                        return data;
                    }
                }
            }
        }
    }
}
