using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fshapex.app.Services.CognitiveServices
{
    public class Api
    {
        // https://portal.azure.com/?l=en.en-us#dashboard/private/f1dd502f-8c17-44ad-b9ac-c92f0762900f
        // 20 per minute / 30.000 per month
        public string Key1 { get; set; } = "aa2d415ee6f84eee82dd715aac0a1742";
        public string Key2 { get; set; } = "2db7b19630f54d1b9e9f030d1bfb2eda";
        public string BaseAddress { get; set; } = "https://eastus2.api.cognitive.microsoft.com/";

        public async Task<List<Model>> GetDataAsync(string uri)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", Key1);

                using (var content = new StringContent("{\"url\":\"" + uri + "\"}", Encoding.UTF8, "application/json"))
                {
                    using (var response = await httpClient.PostAsync($"face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=true", content))
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
