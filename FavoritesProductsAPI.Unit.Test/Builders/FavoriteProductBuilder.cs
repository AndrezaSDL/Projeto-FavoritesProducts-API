using AutoBogus;
using FavoritesProductsAPI.Models;
using System.Collections.Generic;

namespace FavoritesProductsAPI.Unit.Tests.Builder
{
    public class FavoriteProductBuilder : AutoFaker<FavoriteProduct>
    {
        public FavoriteProductBuilder()
        {
            RuleFor(x => x.ClientId, faker => faker.Random.Int(1))
                .RuleFor(x => x.ProductId, faker => faker.Random.Guid());
        }
    }
}
