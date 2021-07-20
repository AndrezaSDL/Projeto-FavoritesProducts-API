using FavoritesProductsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FavoritesProductsAPI.Data.Models.Dto
{
    public class FavoriteProductResponseDto
    {
        [JsonProperty("client")]
        public Client Client { get; set; }
        [JsonProperty("favorites")]
        public IEnumerable<ProductResponseDto> FavoritesProducts { get; set; }
    }

    public class ProductResponseDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("price")]
        public string price { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("brand")]
        public string brand { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }
    }
}
