using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Proizvodi { get; set; }
        [Required]
        public string Kolicine { get; set; }
        [Required]
        public string UserId { get; set; }

        public DateTime? DatumNarudzbe { get; set; }
    }
}