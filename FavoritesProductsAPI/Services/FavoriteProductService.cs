using AutoMapper;
using FavoritesProductsAPI.Data;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using FavoritesProductsAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private IProductService _productService;
        private IClientService _clientService;
        private FavoritesProductsContext _context;

        public FavoriteProductService(FavoritesProductsContext context,
            IMapper mapper,
            IProductService productService,
            IClientService clientService)
        {
            _context = context;
            _productService = productService;
            _clientService = clientService;
        }

        public async Task<IEnumerable<FavoriteProductResponseDto>> Get(
           Parameters parameters)
        {
            var favoritesExistsInbase = _context.FavoritesProducts
                .Skip((parameters.Page - 1) * parameters.Size)
                .Take(parameters.Size)
                .AsEnumerable();

            var favoriteFieldsExternals = FillFieldsExternals(favoritesExistsInbase);

            return await Task.FromResult(favoriteFieldsExternals);
        }

        public async Task<FavoriteProductResponseDto> GetByClientId(int clientId)
        {
            var favoriteRequest = _context.FavoritesProducts
               .Where(f => f.ClientId == clientId)
                   .FirstOrDefaultAsync();

            var favoriteFieldsExternals = FillFieldsExternals(
                new List<FavoriteProduct> { favoriteRequest.Result})
                    .FirstOrDefault();

            return await Task.FromResult(favoriteFieldsExternals);
        }

        public async Task<FavoriteProductResponseDto> Save(FavoriteProduct favoriteRequest)
        {
            if(VerifyFieldsRequestExists(favoriteRequest).Result)
            {
                await _context.FavoritesProducts.AddAsync(favoriteRequest);
                await _context.SaveChangesAsync();

                return FillFieldsExternals(new List<FavoriteProduct> { favoriteRequest })
                    .FirstOrDefault();
            }
            return null;
        }

        public async Task Delete(FavoriteProduct favoriteDtos)
        {
            var favoritesProductsInDataBase = GetFavoriteProduct(favoriteDtos);

            _context.Remove(favoritesProductsInDataBase);
            await _context.SaveChangesAsync();
        }

        private IEnumerable<FavoriteProductResponseDto> FillFieldsExternals(
          IEnumerable<FavoriteProduct> favoritesProducts)
        {
            var favoritesProductsDto = favoritesProducts
              .GroupBy(c => new { c.ClientId })
                  .Select(f => new FavoriteProductResponseDto
                  {
                      Client = _clientService.Get(p => p.Id == f.Key.ClientId).Result,
                      FavoritesProducts =
                            f.Select(p =>
                                _productService.GetByIdAsync(p.ProductId)
                                    .Result).ToList()
                  });

            return favoritesProductsDto;
        }

        public FavoriteProduct GetFavoriteProduct(
           FavoriteProduct favoriteRequest)
        {
            var favoriteProduct = _context.FavoritesProducts
                    .Where(item => favoriteRequest.ClientId == item.ClientId &&
                            favoriteRequest.ProductId == item.ProductId)
                    .FirstOrDefault();
            
            return favoriteProduct;
        }

        private async Task<bool> VerifyFieldsRequestExists
          (FavoriteProduct favoriteProductRequest)
        {
            var favoritesProducts = ProductIdExistsInAPI(favoriteProductRequest.ProductId).Result && 
                ClientIdExistsInDataBase(favoriteProductRequest.ClientId).Result;

            return await Task.FromResult(favoritesProducts);
        }

        private async Task<bool> ClientIdExistsInDataBase(int clientId) =>
           await _clientService.ExistsInDataBase(clientId);

        private async Task<bool> ProductIdExistsInAPI(Guid productId) =>
            await _productService.ExistsInApi(productId);
    }
}
