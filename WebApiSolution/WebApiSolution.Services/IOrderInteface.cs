using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSolution.Domain.Models;

namespace WebApiSolution.Services
{
    public interface IOrderInteface
    {
        // Orders CRUD Operations
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<Order?> AddOrderAsync(AddOrderRequest newOrder);
        Task<Order?> UpdateOrderAsync(int orderId, AddOrderRequest updatedOrder);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<List<Order>> GetOrdersForCustomerAsync(int customerId);
        Task<decimal> GetTotalPriceForCustomerAsync(int customerId);
        Task<Dictionary<string, decimal>> GetTotalPricePerCustomerAsync();

        // Customers CRUD Operations
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer> AddCustomerAsync(AddCustomerRequest newCustomer);
        Task<Customer?> UpdateCustomerAsync(int customerId, AddCustomerRequest updatedCustomer);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}
