using AutoBogus;
using FavoritesProductsAPI.Models;

namespace FavoritesProductsAPI.Unit.Tests.Builder
{
    public class ClientBuilder : AutoFaker<Client>
    {
        public ClientBuilder()
        {
            RuleFor(x => x.Id, faker => faker.Random.Int(1))
                .RuleFor(x => x.Name, faker => faker.Name.FullName())
                .RuleFor(x => x.Email, faker => faker.Internet.Email());
        }
    }
}
