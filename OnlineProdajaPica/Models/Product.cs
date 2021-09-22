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
        public string Name { get; set; }

        [Display(Name="Neto količina")]
        public string Description { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName="money")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
    }
}