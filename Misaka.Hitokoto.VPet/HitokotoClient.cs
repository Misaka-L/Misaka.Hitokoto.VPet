using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Misaka.Hitokoto.VPet.Extenstion;
using Misaka.Hitokoto.VPet.Models;

namespace Misaka.Hitokoto.VPet
{
    public class HitokotoClient
    {
        public string BaseUrl { get; private set; }

        private readonly HttpClient _httpClient = new()
        {
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    new ProductInfoHeaderValue("Misaka.Hitokoto.VPet",
                        Assembly.GetExecutingAssembly().GetName().Version.ToString())
                }
            }
        };

        public HitokotoClient(string baseUrl = "https://v1.hitokoto.cn")
        {
            BaseUrl = baseUrl;
        }

        public void SetApiBaseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
        
        public async Task<HitokotoItem> GetHitokotoAsync()
        {
            var hitokotoItem = await GetHitokotoAsync(Array.Empty<HitokotoType>());
            return hitokotoItem;
        }

        public async Task<HitokotoItem> GetHitokotoAsync(HitokotoType hitokotoType)
        {
            var hitokotoItem = await GetHitokotoAsync(new[] { hitokotoType });
            return hitokotoItem;
        }

        public async Task<HitokotoItem> GetHitokotoAsync(IEnumerable<HitokotoType> hitokotoTypes)
        {
            var hitokotoItem = await _httpClient.GetJsonAsync<HitokotoItem>($"{BaseUrl}/?{hitokotoTypes.ToQueryString()}&encode=json&charset=utf-8");
            return hitokotoItem;
        }
    }
}