using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services.Contracts
{
    public interface IFavoriteProductService
    {
        Task<IEnumerable<FavoriteProductResponseDto>> Get(Parameters clientParameters);
        Task<FavoriteProductResponseDto> GetByClientId(int id);
        Task<FavoriteProductResponseDto> Save(FavoriteProduct favoriteDto);
        Task Delete(FavoriteProduct favoriteDto);
        FavoriteProduct GetFavoriteProduct(FavoriteProduct favoriteProduct);

    }
}
