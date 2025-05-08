using Microsoft.AspNetCore.Identity;

namespace E_Commerce.BL;

public interface ISecurityManager
{
    IEnumerable<IdentityError>? Register(RegisterDto register);
    TokenDto? Login(LoginDto login);
}
