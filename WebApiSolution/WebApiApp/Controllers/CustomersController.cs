using Microsoft.AspNetCore.Mvc;
using WebApiSolution.Domain.Models;
using WebApiSolution.Services;

namespace WebApiApp_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public CustomersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// იძლევა მომხმარებლების ჩამონათვალს
        /// </summary>
        /// <remarks>
        /// იძლევა მომხმარებლების ჩამონათვალს
        /// </remarks>
        /// <returns>მომხმარებლების ჩამონათვალს</returns>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _orderService.GetAllCustomersAsync();
                return Ok(customers);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// იძლევა მომხმარებელს Id-ით
        /// </summary>
        /// <remarks>
        /// იძლევა მომხმარებელს Id-ით
        /// </remarks>
        /// <returns>მომხმარებელს Id-ით</returns>
        /// <param name="id">მომხმარებლის id</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _orderService.GetCustomerByIdAsync(id);
                if (customer == null)
                {
                    return NotFound("მომხმარებელი ვერ მოიძებნა");
                }
                return Ok(customer);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
        }

        /// <summary>
        /// ამატებს ახალ მომხმარებელს
        /// </summary>
        /// <remarks>
        /// ამატებს ახალ მომხმარებელს
        /// </remarks>
        /// <returns>დამატებული მომხმარებლის ობიექტს</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(AddCustomerRequest newCustomer)
        {
            try
            {
                var addedCustomer = await _orderService.AddCustomerAsync(newCustomer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = addedCustomer.Id }, addedCustomer);

            }
            catch (Exception ex)
            {
                return BadRequest($"არასწორი input:  {ex.Message}");
            }
            
        }

        /// <summary>
        /// ანახლებს მომხმარებელს
        /// </summary>
        /// <remarks>
        /// ანახლებს მომხმარებელს
        /// </remarks>
        /// <returns>განახლებული მომხმარებლის ობიექტს</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, AddCustomerRequest updatedCustomer)
        {
            try
            {
                var customer = await _orderService.UpdateCustomerAsync(id, updatedCustomer);
                if (customer == null)
                {
                    return NotFound("ასეთი მომხმარებელი ვერ მოიძებნა");
                }
                return Ok(customer);
            } catch (Exception ex)
            {
                return BadRequest("არასწორი input" + ex.Message);
            }
            
        }

        /// <summary>
        /// შლის მომხმარებელს
        /// </summary>
        /// <remarks>
        /// შლის მომხმარებელს
        /// </remarks>
        /// <returns>წაშლილი მომხმარებლის ობიექტს</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _orderService.DeleteCustomerAsync(id);
                if (!result)
                {
                    return NotFound("ასეთი მომხმარებელი არ მოიძებნა");
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
        }
    }
}
