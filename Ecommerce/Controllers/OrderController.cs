using AutoMapper;
using Ecommerce.Dto;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {


        private readonly IMapper _mapper;
        private readonly IOrder _orderservice;

        public OrderController(IMapper mapper, IOrder orderservice)
        {
            _mapper = mapper;
            _orderservice = orderservice;

        }


        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _orderservice.GetOrdersAsync();

            if (orders == null) return NotFound("No Orders found");
            return Ok(orders);
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<string>> AddOrder(CreateOrderDto neworder)
        {
            
            var isauth = User?.Identity?.IsAuthenticated ?? false;

            if(isauth)
            {
                //var Id = User?.Claims.FirstOrDefault(x => x.Type == "Name").Value;

                var Id = User?.Claims.ToList()[1].Value;



                if (Id == null) return BadRequest("The user has no Id set");

                Guid userId = Guid.Parse(Id);
                neworder.UserId = userId;

                var mappedorder = _mapper.Map<Order>(neworder);

                var response = await _orderservice.CreateOrderAsync(mappedorder);

                return Created($"Order/{mappedorder.Id}", response);


            }

            return Unauthorized();
           

        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<Order>> GetOrder(Guid Id)
        {

            var order = await _orderservice.GetOrderAsync(Id);

            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpDelete("{Id}")]

        public async Task<ActionResult> DeleteOrder(Guid Id)
        {

            var order = await _orderservice.GetOrderAsync(Id);

            if (order == null) return NotFound();
            var response = await _orderservice.DeleteOrderAsync(order);

            return NoContent();

        }

        [HttpPut("{Id}")]

        public async Task<ActionResult<string>> UpdateOrder(Guid Id, CreateOrderDto updatedorder)
        {
            var existingorder = await _orderservice.GetOrderAsync(Id);
            var mappedproduct = _mapper.Map(updatedorder, existingorder);

            var response = await _orderservice.UpdateOrderAsync();

            return Ok(response);


        }
    }
}
