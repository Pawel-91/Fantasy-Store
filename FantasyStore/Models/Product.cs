using System.ComponentModel.DataAnnotations;

namespace FantasyStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Add product name please.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Add description please.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage = "Add positive price please.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Add category please.")]
        public string Category { get; set; }
    }
}
