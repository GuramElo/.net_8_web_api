using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiSolution.Domain.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Unique identifier for the customer.
        [Required]
        public string Name { get; set; } // Name of the customer.
        [Required]
        public string Email { get; set; } // Email address of the customer.
        public string? Address { get; set; } // Address of the customer.
        public string? PhoneNumber { get; set; } // PhoneNumber of the customer.
    }
}
