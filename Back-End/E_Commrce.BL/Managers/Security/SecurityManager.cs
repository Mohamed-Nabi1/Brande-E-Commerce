using E_Commerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.BL;

public class SecurityManager : ISecurityManager
{
    private readonly IConfiguration configuration;
    private readonly UserManager<Customer> userManager;

    public SecurityManager(IConfiguration _configuration,
        UserManager<Customer> _userManager)
    {
        userManager = _userManager;
        configuration = _configuration;
    }



    public IEnumerable<IdentityError>? Register(RegisterDto register)
    {
        var newUser = new Customer
        {
            FirstName = register.FirstName,
            MidName = register.LastName,
            LastName = register.LastName,
            PhoneNumber = register.PhoneNumber,
            Email = register.Email,
            City = register.City,
            Street = register.Street,
            Country = register.Country,
            UserName = register.Email
        };

        var creationResult = userManager.CreateAsync(newUser, register.Password).Result;
        if (!creationResult.Succeeded)
        {
            return creationResult.Errors;

        }

        var claims = new List<Claim> {
            new (ClaimTypes.NameIdentifier,newUser.Id),
            new (ClaimTypes.Role,"Customer")
        };

        var addClaims = userManager.AddClaimsAsync(newUser, claims).Result;
        if (!addClaims.Succeeded)
        {
            return addClaims.Errors;
        }


        return null;
    }

    public TokenDto? Login(LoginDto login)
    {
        Customer? customer = userManager.FindByEmailAsync(login.Email).Result;
        if (customer == null)
        {
            return null;
        }

        bool passwordCorrect = userManager.CheckPasswordAsync(customer, login.Password).Result;
        if (!passwordCorrect)
        {
            return null;
        }

        List<Claim> claims = userManager.GetClaimsAsync(customer).Result.ToList();

        //Key
        string? keyString = configuration.GetSection("SecretKey").Value;
        byte[] keyBytes = Encoding.ASCII.GetBytes(keyString);
        SymmetricSecurityKey key = new SymmetricSecurityKey(keyBytes);

        //Hashing
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //Token
        DateTime exp = DateTime.Now.AddMinutes(60);
        
        JwtSecurityToken newToken = new JwtSecurityToken
        (
            claims: claims,
            signingCredentials: signingCredentials,
            expires: exp

        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(newToken);

        return new TokenDto
        {
            Token = token,
            exp=exp,
        };

    }
}
