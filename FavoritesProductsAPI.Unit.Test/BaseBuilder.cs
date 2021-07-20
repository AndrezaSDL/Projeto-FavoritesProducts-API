using Bogus;

namespace FavoritesProductsAPI.Unit.Tests
{
    public abstract class BaseBuilder<T>
    {
        protected Faker Faker = new Faker("pt_BR");
        public abstract T InstanciarObjeto();
    }
}
