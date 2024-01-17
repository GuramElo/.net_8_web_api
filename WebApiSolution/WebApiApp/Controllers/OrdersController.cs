using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WebApiSolution.Domain.Models;
using WebApiSolution.Services;

namespace WebApiApp_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderInteface _orderService;

        public OrdersController(IOrderInteface orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// იძლევა ყველა შეკვეთას
        /// </summary>
        /// <remarks>
        /// იძლევა ყველა შეკვეთას
        /// </remarks>
        /// <returns>ყველა შეკვეთა</returns>
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            } catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
        /// <summary>
        /// იძლევა შეკვეთას სრულად ID - ით
        /// </summary>
        /// <remarks>
        /// იძლევა შეკვეთას სრულად ID - ით
        /// </remarks>
        /// <returns>შეკვეთა ID - ით.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound("შეკვეთა ვერ მოიძებნა");
                }
                return Ok(order);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// ამატებს შეკვეთას
        /// </summary>
        /// <remarks>
        /// ამატებს შეკვეთას
        /// </remarks>
        /// <returns>შეკვეთის ობიექტს</returns>
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(AddOrderRequest newOrder)
        {
            try
            {
                Order? status = await _orderService.AddOrderAsync(newOrder);
                if (status == null)
                {
                    return BadRequest("არასწორი input პარამეტრები");
                }
                return Ok(status);

            } catch (Exception ex)
            {
                return BadRequest($"შეცდომაა: {ex.Message}");
            }
            
        }

        /// <summary>
        /// ანახლებს შეკვეთას
        /// </summary>
        /// <remarks>
        /// ანახლებს შეკვეთას
        /// </remarks>
        /// <returns>განახლებულ შეკვეთას</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, AddOrderRequest updatedOrder)
        {
            try
            {
                var order = await _orderService.UpdateOrderAsync(id, updatedOrder);
                if (order == null)
                {
                    return NotFound("ასეთი შეკვეთა არ მოიძებნა");
                }
                return Ok(order);
            } catch (Exception ex) {
                return BadRequest($"არასწორი input: {ex.Message}");
            }
           
        }

        /// <summary>
        /// შლის შეკვეთას
        /// </summary>
        /// <remarks>
        /// შლის
        /// </remarks>
        /// <returns>წაშლილი შეკვეთის ობიექტს</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try {
                var result = await _orderService.DeleteOrderAsync(id);
                if (!result)
                {
                    return NotFound("ასეთი შეკვეთა არ მოიძებნა");
                }
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
          
        }

        /// <summary>
        /// იძლევა კონკრეტული მომხმარებლის შეკვეთების ჩამონათვალს
        /// </summary>
        /// <remarks>
        /// იძლევა კონკრეტული მომხმარებლის შეკვეთების ჩამონათვალს
        /// </remarks>
        /// <returns>კონკრეტული მომხმარებლის შეკვეთების ჩამონათვალს</returns>
        [HttpGet("customer/{customerId:int}")]
        public async Task<ActionResult<List<Order>>> GetOrdersForCustomer(int customerId)
        {
            try
            {
                var orders = await _orderService.GetOrdersForCustomerAsync(customerId);
                return Ok(orders);
            } catch (Exception ex) { 
            return BadRequest($"ცუდი ინფუთი: {ex.Message}");
                    }
            
        }

        /// <summary>
        /// იძლევა კონკრეტული მომხმარებლის შეკვეთების ჯამურ ღირებულებას
        /// </summary>
        /// <remarks>
        /// იძლევა კონკრეტული მომხმარებლის შეკვეთების ჯამურ ღირებულებას
        /// </remarks>
        /// <returns>კონკრეტული მომხმარებლის შეკვეთების ჯამურ ღირებულებას</returns>
        [HttpGet("customer/{customerId:int}/totalprice")]
        public async Task<ActionResult<decimal>> GetTotalPriceForCustomer(int customerId)
        {
            try
            {
                var totalPrice = await _orderService.GetTotalPriceForCustomerAsync(customerId);
                return Ok(totalPrice);
            } catch (Exception ex)
            {
                return BadRequest($"ცუდი input: {ex.Message}");
            }
           
        }

        /// <summary>
        /// იძლევა ყველა მომხმარებლის შეკვეთების ჯამურ ღირებულებას მომხმარებელ / მომხმარებელ
        /// </summary>
        /// <remarks>
        /// იძლევა ყველა მომხმარებლის შეკვეთების ჯამურ ღირებულებას მომხმარებელ / მომხმარებელ
        /// </remarks>
        /// <returns>ყველა მომხმარებლის შეკვეთების ჯამურ ღირებულებას მომხმარებელ / მომხმარებელ</returns>
        [HttpGet("totalpricepercustomer")]
        public async Task<ActionResult<Dictionary<string, decimal>>> GetTotalPricePerCustomer()
        {
            try
            {
                var totalPricePerCustomer = await _orderService.GetTotalPricePerCustomerAsync();
                return Ok(totalPricePerCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
