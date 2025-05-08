using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    #region CTOR

    private readonly IUsersManagers _usersManager;

    public UsersController(IUsersManagers usersManager)
    {
        _usersManager = usersManager;
    }

    #endregion



    #region Delete Users

    //[HttpDelete("Delete/{id}")]
    //public ActionResult DeleteUser(Guid id)
    //{

    //    bool request = _usersManager.DeleteUser(id);
    //    if (request == false)
    //    {
    //        return BadRequest();
    //    }
    //    return Ok("Category Deleted Successfully");

    //}


    #endregion


    #region GetByEmail

    [HttpGet("Details/email")]
    public ActionResult<CustomerListDataDto> UserDetailbyEmail(string email)
    {

        CustomerListDataDto? userDetails = _usersManager.GetUserByEmail(email);
        if (userDetails == null)
        {
            return BadRequest();
        }

        return userDetails;
    }
    #endregion


    #region get By phoneNumber

    [HttpGet("Details/phoneNumber")]
    public ActionResult<CustomerListDataDto> UserDetailbyphoneNumberl(string phoneNumber)
    {

        CustomerListDataDto? userDetails = _usersManager.GetUserByPhonNumber(phoneNumber);
        if (userDetails == null)
        {
            return BadRequest();
        }

        return userDetails;
    }
    #endregion
}