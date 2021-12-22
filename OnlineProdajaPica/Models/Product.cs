using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ime proizvoda")]
        public string Name { get; set; }

        [Display(Name="Neto količina")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Zaliha")]
        public int NumberInStock { get; set; }

        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName="money")]
        [Display(Name = "Cijena")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="Unesena vrijednost nije ispravna.")]
        [Display(Name = "Količina (kom)")]
        public int Quantity { get; set; }
    }
}