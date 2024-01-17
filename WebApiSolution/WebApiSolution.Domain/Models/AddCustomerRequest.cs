
namespace WebApiSolution.Domain.Models
{
    public class AddCustomerRequest
    {
        public string? Name { get; set; } // Name of the customer.
        public string? Email { get; set; } // Email address of the customer.
        public string? Address { get; set; } // Address of the customer.
        public string? PhoneNumber { get; set; } // PhoneNumber of the customer.
    }
}
