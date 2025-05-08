using E_Commerce.BL;
using E_Commerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly UserManager<Customer> _customer;

        public OrdersController(IOrderManager orderManager,UserManager<Customer> customer) 
        {
            _orderManager = orderManager;
            _customer = customer;
        }
        [HttpGet]
        public ActionResult<List<OrderReadDto>> AllOrders() 
        {
            return _orderManager.GetAllOrders();
        }
        [HttpGet]
        [Route("OrdersCutomerName")]
        public ActionResult<List<OrderReadDto>> AllOrderswithCutName()
        {
            return _orderManager.GetAllOrderswithCustName();
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OrderReadDto> GetOrder(Guid id) 
        {
            return _orderManager.GetOrderById(id);
        }
        [HttpPost]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult AddOrder(OrderAddDto order) 
        {
            try
            {
                Guid customerId = new Guid(_customer.GetUserAsync(User).Result.Id);
                bool checkAddOrder = _orderManager.AddOrder(order,customerId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        //[Route("{id}")]
        public ActionResult UpdateOrder(OrderUpdateDto orderUpdate) 
        {
            bool req =_orderManager.UpdateOrder(orderUpdate);
            if(!req) return BadRequest(req);
            return Ok(new { message = "Updated Successfully" });
        }

        [HttpPatch]
        [Route("UpdateOrderStatus")]
        public ActionResult UpdateOrderStatus(Guid orderId, OrderStatus orderStatus)
        {
            bool req = _orderManager.UpdateOrderStatus(orderId, orderStatus);

            if (!req) return BadRequest(req);
            return Ok("Updated Sucessfully");
        }

        [HttpDelete]
        public ActionResult DeleteOrder(Guid id) 
        {
            bool req =_orderManager.DeleteOrder(id);
            if(!req) return BadRequest(req);
            return Ok("Deleted Sucessfully");
        }

        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult<OrderWithProductsAndCustomerReadDto> Details(Guid id) 
        {
            OrderWithProductsAndCustomerReadDto? orderDetails =_orderManager.OrderWithProductsAndCustomerRead(id);
            if (orderDetails is null) return BadRequest();
            return orderDetails;
        }

        [HttpGet]
        [Route("OrderProducts/{id}")]
        public ActionResult<OrderWithProductsReadDto> OrderProducts(Guid id)
        {
            OrderWithProductsReadDto? OrderProducts = _orderManager.OrderWithProductsRead(id);
            if (OrderProducts is null) return BadRequest();
            return OrderProducts;
        }

        [HttpGet]
        [Authorize(Policy = "ForCustomer")]
        [Route("ByCustomer")]
        public ActionResult<List<OrderWithProductsReadDto>> GetOrdersByCustomerId()
        {
            var customereId = _customer.GetUserAsync(User).Result.Id;
            var orders = _orderManager.GetOrdersByCustomerId(customereId);
            if (orders is null)
            {
                return NotFound();
            }
            return orders;
        }
    }
}
