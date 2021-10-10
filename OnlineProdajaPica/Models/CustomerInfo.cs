using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.Models
{
    public class CustomerInfo
    {
        [Required]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name="Ime i prezime")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Broj telefona")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Poštanski broj")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Mjesto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Država")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        public int OrderId { get; set; }
    }
}