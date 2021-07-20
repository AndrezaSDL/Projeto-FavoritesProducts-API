using AutoBogus;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System.Collections.Generic;

namespace FavoritesProductsAPI.Unit.Tests.Builder
{
    public class ProductResponseDtoBuilder : AutoFaker<ProductResponseDto>
    {
        public ProductResponseDtoBuilder()
        {
            RuleFor(x => x.Id, faker => faker.Random.Guid())
              .RuleFor(x => x.price, faker => faker.Random.Decimal().ToString())
              .RuleFor(x => x.image, faker => faker.Internet.Url())
              .RuleFor(x => x.brand, faker => faker.Random.Words())
              .RuleFor(x => x.title, faker => faker.Random.Words());
        }
    }
}
