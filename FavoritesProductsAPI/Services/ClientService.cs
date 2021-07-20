using AutoMapper;
using FavoritesProductsAPI.Data;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using FavoritesProductsAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FavoritesProductsAPI.Services
{
    public class ClientService : IClientService
    {
        private FavoritesProductsContext _context;
        private IMapper _mapper;

        public ClientService(FavoritesProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Client> Get (Expression<Func<Client, bool>> where) =>
            await _context.Clients.FirstOrDefaultAsync(where);

        public async Task<IEnumerable<Client>> GetAll(Parameters parameters) =>
            await _context.Clients
                .Skip((parameters.Page - 1) * parameters.Size)
                .Take(parameters.Size)
                .ToListAsync();

        public async Task<Client> Save(ClientRequestDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task Update(ClientRequestDto clientDto, int clientId)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
                _mapper.Map(clientDto, client);
                _context.Clients.Update(client);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Client client)
        {
            _context.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsInDataBase(int id) 
        {
            var client = Get(c => c.Id == id);

            return await client == null ? false: true;
        }
    }
}
