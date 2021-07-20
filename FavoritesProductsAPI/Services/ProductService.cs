using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Services.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly string basic_url = "http://challenge-api.luizalabs.com/api/product/";

        public void GetAllAsync(int page)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(
                   basic_url + "?" +
                   $"page={page}").Result;

                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;

                JObject content = JObject.Parse(result);
                var products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(content["products"].ToString());
            }
        }

        public async Task<ProductResponseDto> GetByIdAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(
                   basic_url +
                   $"{id.ToString()}/").Result;

                response.EnsureSuccessStatusCode();

                string result = response.Content.ReadAsStringAsync().Result;
                return await Task.FromResult(JsonConvert.DeserializeObject<ProductResponseDto>(result.ToString()));
            }
        }

        public async Task<bool> ExistsInApi(Guid id) =>
            GetByIdAsync(id) != null ? true : false;
    }
}
