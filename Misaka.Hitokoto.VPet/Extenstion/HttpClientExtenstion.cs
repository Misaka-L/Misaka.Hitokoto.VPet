using System.Net.Http;
using Newtonsoft.Json;

namespace Misaka.Hitokoto.VPet.Extenstion;

public static class HttpClientExtenstion
{
    public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string uri)
    {
        var response = await httpClient.GetStringAsync(uri);
        if (response == null) throw new ArgumentNullException(nameof(response), "The response string is null");
        return JsonConvert.DeserializeObject<T>(response) ?? throw new InvalidOperationException("Deserialize return null");
    }
}