using AutoMapper;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoritesProductsAPI.Data.Profiles
{
    public class FavoritesProductProfile : Profile
    {
        public FavoritesProductProfile()
        {
            CreateMap<ClientRequestDto, Client>();
        }
    }
}
