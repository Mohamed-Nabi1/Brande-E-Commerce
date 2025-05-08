using E_Commerce.DAL;

namespace E_Commerce.BL;

public interface IUsersManagers
{

    public bool DeleteUser(Guid UserId);
    public void UpdateUser(UserUpdateDto editUserDto);
    public List<UserProfileInfoDto> GetAllUsersByRole(UserRole role);
    public  List<UserProfileInfoDto> GetAllUsersByCity(string city, UserRole role);
    public CustomerListDataDto? GetUserByEmail(string email);
    public CustomerListDataDto? GetUserByPhonNumber(string phonenumber);


}
