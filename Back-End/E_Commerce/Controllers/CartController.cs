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
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartmanager;
        private readonly UserManager<Customer> _customer;

        public CartController(ICartManager cartmanager,UserManager<Customer> customer)
        {
            _cartmanager = cartmanager;
            _customer = customer;
        }


        [HttpPost]
        [Route("AddCart")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult AddProductToCart( AddToCartDto addToCartDto)
        {
           var customerID= _customer.GetUserAsync(User).Result.Id;
            try
            {
    
                _cartmanager.AddToCart(addToCartDto,customerID);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("CartProducts")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult<GetCartProductByCustomerIdDto> GetCartProductsByCustomerId()
        {
            try{
                Guid customerId = new Guid(_customer.GetUserAsync(User).Result.Id);
                GetCartProductByCustomerIdDto cartDto = _cartmanager.GetCartProductsByCustomerId(customerId);
                return cartDto;
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("DeletePrdouctFromCart")]
        [Authorize(Policy = "ForCustomer")]
        public IActionResult DeleteCartProduct(DeleteCardProductDto deleteCardProduct)
        {
            _cartmanager.DeleteCartProduct(deleteCardProduct);
            return Ok();
        }


        [HttpDelete]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult DeleteCart()
        {
            try
            {
                Guid customerId = new Guid(_customer.GetUserAsync(User).Result.Id);
                bool isDeleted = _cartmanager.DeleteCart(customerId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }


        [HttpPatch]
        [Route("Increase")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult UpdateCart(AddToCartDto cartProduct)
        {
            try
            {
                Guid customerId = new Guid(_customer.GetUserAsync(User).Result.Id);
                var Updated =  _cartmanager.UpdateCartProduct(customerId, cartProduct);
                if (!Updated)
                    return BadRequest();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpPatch]
        [Route("Decrease")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult UpdateCartDec(AddToCartDto cartProduct)
        {
            try
            {
                Guid customerId = new Guid(_customer.GetUserAsync(User).Result.Id);
                var Updated = _cartmanager.UpdateCartProductDecr(customerId, cartProduct);
                if (Updated)
                    return Ok();
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
            


        }

    }
}
