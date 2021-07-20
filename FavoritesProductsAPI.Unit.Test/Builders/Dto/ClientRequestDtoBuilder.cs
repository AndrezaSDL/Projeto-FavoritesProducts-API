using AutoBogus;
using FavoritesProductsAPI.Data.Models.Dto;

namespace FavoritesProductsAPI.Unit.Tests.Builder.Dto
{
    public class ClientRequestDtoBuilder : AutoFaker<ClientRequestDto>
    {
        public ClientRequestDtoBuilder()
        {
            RuleFor(x => x.Name, faker => faker.Name.ToString())
                .RuleFor(x => x.Email, faker => faker.Internet.Email());
        }
    }
}
