using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiSolution.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Unique identifier for the customer.
        public string? Product { get; set; } // Name of the product in the order.
        [Required]
        public decimal Price { get; set; } // Price of the product in the order.
        [Required]
        public int CustomerId { get; set; } // Identifier indicating the customer who placed the order.
        public DateTime? OrderDate { get; set; } // Date when order was made.
        public int? Quantity { get; set; } // Quantity of ordered items.
    }
}
