using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FavoritesProductsAPI.Services.Contracts;
using FavoritesProductsAPI.Models;
using FavoritesProductsAPI.Data.Models.Dto;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FavoritesProductsAPI.Controllers
{
    [ApiController]
    [Route("favorites")]
    public class FavoritesProductsController : ControllerBase
    {
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoritesProductsController(IFavoriteProductService favoriteProductService) =>
            _favoriteProductService = favoriteProductService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteProductResponseDto>>> Get([FromQuery] Parameters parameters)
        {
            var result = await _favoriteProductService.Get(parameters);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<FavoriteProductResponseDto>> GetFavoriteProduct(int clientId)
        {
            var result = await _favoriteProductService.GetByClientId(clientId);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteProduct>> Post([FromBody] FavoriteProduct favoriteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var favoriteExists = _favoriteProductService.GetFavoriteProduct(favoriteDto);

            if (favoriteExists != null)
                return UnprocessableEntity(new[] { "Favorite product exists: " + favoriteDto });

            var result = await _favoriteProductService.Save(favoriteDto);
          
            return Created("", result);
        }

        [HttpDelete()]
        public async Task<ActionResult> Delete([FromBody]FavoriteProduct favorite)
        {
            if (favorite == null) return NotFound();

            await _favoriteProductService.Delete(favorite);

            return NoContent();
        }
    }
}
