using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavoritesProductsAPI.Models
{
    public class FavoriteProduct
    {
        [Required]
        [Column(Order = 1)]
        [Display(Name = "Id do Produto fornecido pela API LuizaLabs.")]
        public Guid ProductId { get; set; }

        [Required]
        [ForeignKey("Client"), Column(Order = 2)]
        [Display(Name = "Id do Cliente, FK não incremental.")]
        public int ClientId { get; set; }
    }
}
