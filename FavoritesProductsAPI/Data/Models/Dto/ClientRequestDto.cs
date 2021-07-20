using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FavoritesProductsAPI.Data.Models.Dto
{
    public class ClientRequestDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
