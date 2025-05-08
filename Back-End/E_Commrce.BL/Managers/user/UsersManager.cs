using E_Commerce.DAL;

namespace E_Commerce.BL;

public class UsersManager : IUsersManagers
{

    private readonly IUnitOfWork _unitOfWork;

    public UsersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Delete

    public bool DeleteUser(Guid UserId)
    {
        User? userFromDb = _unitOfWork.UsersRepo.GetById(UserId);
        if (userFromDb != null)

        {
            _unitOfWork.UsersRepo.Delete(userFromDb);
        }

        int numberOfAffectedRows = _unitOfWork.SaveChange();
        return numberOfAffectedRows > 0;
    }
    #endregion

    #region Update User
    public void UpdateUser(UserUpdateDto updateUserDto)
    {

        User userFromDb = _unitOfWork.UsersRepo.GetById(updateUserDto.Id);
        if (userFromDb != null)
        {

            userFromDb.FirstName = updateUserDto.FirstName;
            userFromDb.MidName = updateUserDto.MidName;
            userFromDb.LastName = updateUserDto.LastName;
            userFromDb.PhoneNumber = updateUserDto.PhoneNumber;
            userFromDb.Email = updateUserDto.Email;

        }

    }


    #endregion

    #region Get Users by Id && City

    public List<UserProfileInfoDto>? GetAllUsersByCity(string city, UserRole role)
    {
        List<User>? userFromDb = _unitOfWork.UsersRepo.GetAllUsersByCity(city, role).ToList();
        if (userFromDb == null)
        {
            return null;
        }
        List<UserProfileInfoDto> usersDto = userFromDb.Select(x => new UserProfileInfoDto
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            MidName = x.MidName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,

        }).ToList();


        return usersDto;
    }

    #endregion

    #region Get By Email

    public CustomerListDataDto? GetUserByEmail(string email)
    {

        Customer? userFromDb = _unitOfWork.UsersRepo.GetUserByEmail(email);

        return new CustomerListDataDto
        {
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            MidName = userFromDb.MidName,
            PhoneNumber = userFromDb.PhoneNumber,
            Street=userFromDb.Street,
            City=userFromDb.City,
            Country=userFromDb.Country,
            Email= userFromDb.Email

        };

    }
    #endregion

    #region Get By PhoneNumber

    public CustomerListDataDto GetUserByPhonNumber(string phonenumber)
    {
        Customer? userFromDb = _unitOfWork.UsersRepo.GetUserByPhonNumber(phonenumber);

        return new CustomerListDataDto
        {
            Id= Guid.Parse( userFromDb.Id),
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            MidName = userFromDb.MidName,
            PhoneNumber = userFromDb.PhoneNumber,
            Street = userFromDb.Street,
            City = userFromDb.City,
            Country = userFromDb.Country,
            Email = userFromDb.Email
        };
    }
    #endregion

    #region Get Users by Role

    public List<UserProfileInfoDto> GetAllUsersByRole(UserRole role)
    {
        List<User>? userFromDb = _unitOfWork.UsersRepo.GetAllUsersByRole(role).ToList();


        List<UserProfileInfoDto> usersDto = userFromDb.Select(c => new UserProfileInfoDto
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            MidName = c.MidName,
            Role = c.Role,
            PhoneNumber = c.PhoneNumber
            

        }).ToList();

        return usersDto;

    }
    #endregion










}
