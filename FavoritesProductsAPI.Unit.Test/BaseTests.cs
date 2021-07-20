using AutoBogus;
using Autofac.Extras.FakeItEasy;
using Bogus;
using FluentAssertions;
using Unity;

namespace FavoritesProductsAPI.Unit.Test
{
    public class BaseTests
    {
        protected Faker Faker;
        protected AutoFake AutoFake;
        protected IUnityContainer ContainerFake;

        public BaseTests()
        {
            Faker = new Faker("pt_BR");

            AutoFaker.Configure(builder =>
            {
                builder.WithRecursiveDepth(1);
            });

            AutoFake = new AutoFake();
            ContainerFake = new UnityContainer();
        }

        public bool Compare<T>(T expected, T result)
        {
            try
            {
                expected.Should().BeEquivalentTo(result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
