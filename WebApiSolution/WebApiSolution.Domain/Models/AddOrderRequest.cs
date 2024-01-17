namespace WebApiSolution.Domain.Models
{
    public class AddOrderRequest
    {
        public string? Product { get; set; } // Name of the product in the order.
        public decimal Price { get; set; } // Price of the product in the order.
        public int CustomerId { get; set; } // Identifier indicating the customer who placed the order.
        public DateTime? OrderDate { get; set; } // Date when order was made.
        public int? Quantity { get; set; } // Quantity of ordered items.
    }
}
