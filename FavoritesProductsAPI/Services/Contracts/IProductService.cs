using FavoritesProductsAPI.Data.Models.Dto;
using System;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services.Contracts
{
    public interface IProductService
    {
        void GetAllAsync(int page);
        Task<ProductResponseDto> GetByIdAsync(Guid id);
        Task<bool> ExistsInApi(Guid id);
    }
}
