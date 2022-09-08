using rapsoSuiteMaster.Data;
using System.Net.Http.Headers;
using System.Text.Json;


namespace rapsoSuiteMaster.localServices
{
    public class comunicationServices
    {
        private readonly IConfiguration _conf;
        private readonly string _domain;
        public comunicationServices(IConfiguration conf)
        {
            _conf = conf;
            _domain = _conf["SystemInfo:domain"];
        }

        public async Task<taskResponse> callApiJson(string caller, StringContent _stringContent, string apiCall, string? _externalDomain, string? _token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpContent httpContent = _stringContent;
                    string _uri = _domain;

                    if (_externalDomain != "") _uri = _externalDomain;

                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.BaseAddress = new Uri(_uri);

                    if (_token != "") client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);

                    apiCall = _uri + apiCall;

                    var PostAsync = await client.PostAsync(apiCall, httpContent);
                    var ReadString = await PostAsync.Content.ReadAsStringAsync();

                    // *** ToDO: Mejorar mensajes de API ***

                    return JsonSerializer.Deserialize<taskResponse>(ReadString)!;
                }

            }
            catch (Exception ex)
            {
                taskResponse jsonEX = new(caller, "Error", ex.Message);
                return jsonEX;
            }

        }
    }
}
