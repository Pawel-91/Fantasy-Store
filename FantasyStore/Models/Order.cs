using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Add name and surname, please.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the first line of address, please.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Enter city name, please.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter state/land name, please.")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Enter name of country, please.")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
