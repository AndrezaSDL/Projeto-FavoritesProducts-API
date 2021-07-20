using AutoBogus;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System.Collections.Generic;

namespace FavoritesProductsAPI.Unit.Tests.Builder
{
    public class FavoriteProductResponseDtoBuilder : AutoFaker<FavoriteProductResponseDto>
    {
        public FavoriteProductResponseDtoBuilder()
        {
            RuleFor(x => x.Client, faker => new ClientBuilder())
              .RuleFor(x => x.FavoritesProducts, faker =>
                new List<ProductResponseDto> {
                    new ProductResponseDtoBuilder()});
        }
    }
    
}
