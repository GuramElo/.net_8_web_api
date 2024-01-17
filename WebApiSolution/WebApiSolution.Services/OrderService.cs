using Microsoft.EntityFrameworkCore;
using WebApiSolution.Data.DBContext;
using WebApiSolution.Domain.Models;

namespace WebApiSolution.Services
{
    public class OrderService: IOrderInteface
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Orders CRUD ოპერაციები

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order?> AddOrderAsync(AddOrderRequest newOrder)
        {

            Customer customer  = await this.GetCustomerByIdAsync(newOrder.CustomerId);
            if (customer == null)
            {
                throw new Exception("ასეთი მომხმარებელი არ არსებობს");
            }
            Order order = new()
            {
                Price = newOrder.Price,
                Product = newOrder.Product,
                CustomerId = newOrder.CustomerId,
                OrderDate = newOrder.OrderDate,
                Quantity = newOrder.Quantity ?? 1,

            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(int orderId, AddOrderRequest updatedOrder)
        {
            var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (existingOrder != null)
            {
                existingOrder.Product = updatedOrder.Product;
                existingOrder.Price = updatedOrder.Price;
                existingOrder.CustomerId = updatedOrder.CustomerId;
                existingOrder.Quantity = updatedOrder.Quantity ?? existingOrder.Quantity;
                await _dbContext.SaveChangesAsync();
            }
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Order>> GetOrdersForCustomerAsync(int customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<decimal> GetTotalPriceForCustomerAsync(int customerId)
        {
            return await _dbContext.Orders.Where(o => o.CustomerId == customerId).SumAsync(o => (decimal)(o.Price * (o.Quantity ?? 1)));
        }

        public async Task<Dictionary<string, decimal>> GetTotalPricePerCustomerAsync()
        {
            var orders = await _dbContext.Orders
         .Select(o => new { o.CustomerId, o.Price, o.Quantity })
         .ToListAsync();

            var totalPricePerCustomer = orders
                .GroupBy(o => o.CustomerId)
                .ToDictionary(
                    g => $"მომხმარებელი id-ით: {g.Key}",
                    g => g.Sum(o => (decimal)(o.Price * (o.Quantity ?? 1)))
                );

            return totalPricePerCustomer;
        }

        // Customers CRUD ოპერაციები:

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<Customer> AddCustomerAsync(AddCustomerRequest newCustomer)
        {
            Customer customer = new Customer
            {
                Address = newCustomer.Address,
                Name = newCustomer.Name,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,
            };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateCustomerAsync(int customerId, AddCustomerRequest updatedCustomer)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (existingCustomer != null)
            {
                existingCustomer.Name = updatedCustomer.Name;
                existingCustomer.Email = updatedCustomer.Email;
                existingCustomer.Address = updatedCustomer.Address ?? updatedCustomer.Address;
                existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber ?? existingCustomer.PhoneNumber;
                await _dbContext.SaveChangesAsync();
            }
            return existingCustomer;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
