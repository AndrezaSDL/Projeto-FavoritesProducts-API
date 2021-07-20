using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services.Contracts
{
    public interface IClientService
    {
        Task<Client> Get(Expression<Func<Client, bool>> where);
        Task<IEnumerable<Client>> GetAll(Parameters clientParameters);
        Task<Client> Save(ClientRequestDto clientDto);
        Task Update(ClientRequestDto clientDto, int clientId);
        Task Delete(Client client);
        Task<bool> ExistsInDataBase(int id);
    }
}
