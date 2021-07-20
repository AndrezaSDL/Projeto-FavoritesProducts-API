using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FavoritesProductsAPI.Models
{
    public class Client
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 carateres")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [JsonProperty("name")]
        public string Name { get; set; }
       
        [StringLength(200, ErrorMessage = "O email não pode passar de 200 carateres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email formato incorreto")]
        [Required(ErrorMessage = "Email é obrigatório")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
