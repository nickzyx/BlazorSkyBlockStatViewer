using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class HypixelService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public HypixelService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<string> GetPlayerUUIDAsync(string username)
    {
        string url = $"https://api.mojang.com/users/profiles/minecraft/{username}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            return json["id"]?.ToString();
        }

        return null;
    }

    public async Task<JObject> FetchSkyblockStatsAsync(string uuid)
    {
        string url = $"https://api.hypixel.net/skyblock/profiles?key={_apiKey}&uuid={uuid}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        return null;
    }
}
