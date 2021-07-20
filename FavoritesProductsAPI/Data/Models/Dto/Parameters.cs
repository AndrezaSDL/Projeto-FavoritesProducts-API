using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Data.Models.Dto
{
    public class Parameters
    {
        [JsonProperty("page")]
        public int Page { get; set; } = 1;

        const int limit = 50;
        private int _Size = 10;
        [JsonProperty("size")]
        public int Size
        {
            get => _Size;
            set => _Size = (value > limit) ? limit : value;
        }
    }
}
