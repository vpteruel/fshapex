using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace fshapex.app.Services.FacePlusPlus
{
    public class Api
    {
        // https://console.faceplusplus.com/dashboard
        // 60 per minute
        public string AppKey { get; set; } = "RPetJkDlB0BXQtqa8PF5yQ2BiTTJ0PHI";
        public string AppSecret { get; set; } = "TEHISiFjlrPxQXgTjGBqupsYQ_fQV5Jt";
        public string BaseAddress { get; set; } = "https://api-us.faceplusplus.com/";

        public async Task<Model> GetDataAsync(string uri)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("app_id", AppKey);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("app_key", AppSecret);

                using (var content = new StringContent(""))
                {
                    using (var response = await httpClient.PostAsync($"facepp/v3/detect?api_key={AppKey}&api_secret={AppSecret}&image_url={uri}&return_landmark=2&return_attributes=age,beauty,ethnicity,gender,skinstatus", content))
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
