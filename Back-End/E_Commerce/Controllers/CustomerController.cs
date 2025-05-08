using Azure.Core;
using E_Commerce.BL;
using E_Commerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManger;
        private readonly UserManager<Customer> _customer;
        //private readonly IMailingService mailingService;

        public CustomerController(ICustomerManager customerManger, UserManager<Customer> customer )
        {
            _customerManger = customerManger;
            _customer = customer;
            //mailingService = _mailingService;
        }

        [HttpGet]

        public ActionResult<List<CustomerListDataDto>> GetAllCustomer()
        {
            List<CustomerListDataDto> customers = _customerManger.GetAllCustomers();
            return customers;
        }

        [HttpGet]
        [Route("GetByOne")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult<CustomerListDataDto> GetCustomerBy()
        {
            Guid id = new Guid(_customer.GetUserAsync(User).Result.Id);
            CustomerListDataDto? customer = _customerManger.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerAddDto newCustomer)
        {
            _customerManger.AddCustomer(newCustomer);
            return StatusCode(StatusCodes.Status201Created, "Added Successfully");
        }


        [HttpPatch]
        [Route("RestPassword")]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult UpdateCustomerPassword(CustomerUpdatePassDto newCustomer)
        {
            string customerId = _customer.GetUserAsync(User).Result.Id;

            var check = _customerManger.UpadateCustomerPassword(newCustomer,customerId);
            if (check)
            {
                return StatusCode(StatusCodes.Status201Created, "Edit Successfully");
            }
            return StatusCode(StatusCodes.Status304NotModified, "Reset Faild");
        }

        [HttpPatch]
        [Authorize(Policy = "ForCustomer")]
        public ActionResult UpdateCustomerData(CustomerUpdateDto updateCustomer)
        {
            string id = _customer.GetUserAsync(User).Result.Id;

            var check = _customerManger.UpdateCustomerData(updateCustomer,new Guid(id));
            if (check)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete]
        public ActionResult DeleteCustomer(CustomerDeleteDto customer)
        {
            var check = _customerManger.DeleteCustomerById(customer);
            if (check)
            {
                return StatusCode(StatusCodes.Status201Created, "Deleted Successfully");
            }
            return StatusCode(StatusCodes.Status304NotModified, "Deleted Faild");
        }

        //[HttpPost]
        //[Route("forgetPassword")]

        //public async Task<IActionResult> ForgotPassword([FromBody]  ForgetDto forget)
        //{
        //    var user = await _customer.FindByEmailAsync(forget.Email);
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid email address");
        //    }

        //    /*var token = 012320212320012;
        //    var resetPasswordLink = $"http://localhost:4200/resetpassword?token={HttpUtility.UrlEncode(token.ToString())}";

        //    var message = new MailMessage("abdelrahmanemad180@gmail.com", forget.Email, "Password reset request", $"Click the following link to reset your password: {resetPasswordLink}");
        //    var client = new SmtpClient("smtp.gmail.com", 587);
        //    client.EnableSsl = true;
        //    client.Credentials = new NetworkCredential("yousefemad2411@gmail.com", "yousefemad2411@gmail.com", "Abdoemd125698541#farouk");
        //    client.UseDefaultCredentials = false;
        //    await client.SendMailAsync(message);*/

        //    await mailingService.SendEmailAsync(forget.Email,"Hi","Hallllo");

        //    return Ok();
        //}

        //[HttpPost("resetpassword2")]
        //public async Task<IActionResult> ResetPassword([FromBody] CustomerUpdatePassDto customer)
        //{
        //    var user = await _customer.FindByEmailAsync(customer.Email);
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid email address");
        //    }

        //    var result = await _customer.ResetPasswordAsync(user, customer.Token, customer.NewPassword);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest("Invalid password reset token");
        //    }

        //    return Ok();
        //}

    }
}
