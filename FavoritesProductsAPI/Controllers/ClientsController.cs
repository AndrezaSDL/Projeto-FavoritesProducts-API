using Microsoft.AspNetCore.Mvc;
using FavoritesProductsAPI.Data.Models.Dto;
using FavoritesProductsAPI.Models;
using System.Collections.Generic;
using FavoritesProductsAPI.Services.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FavoritesProductsAPI.Controllers
{
    [ApiController]
    [Route("clients")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService) =>
            _clientService = clientService;

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get([FromQuery] Parameters parameters)
        {
            var result = await _clientService.GetAll(parameters);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var result = await _clientService.Get(x => x.Id == id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post([FromBody] ClientRequestDto clientDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(new ValidationProblemDetails(ModelState));

            var result = await _clientService.Get(x => x.Email == clientDto.Email);

            if (result == null)
            {
                result = await _clientService.Save(clientDto);
                return CreatedAtAction(nameof(GetClient), new { Id = result.Id }, result);
            }

            return UnprocessableEntity(new[] { "Email já existente " + clientDto.Email });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClientRequestDto clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var existsInDataBase = _clientService.ExistsInDataBase(id).Result;

            if (!existsInDataBase) 
                return NotFound();

            await _clientService.Update(clientDto, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            var actual = await _clientService.Get(x => x.Id == id);
            if (actual == null) return NotFound();

            await _clientService.Delete(actual);

            return NoContent();
        }
    }
}
