using E_Commerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Customer> _customerManager;

        public CustomerManager(IUnitOfWork unitOfWork, UserManager<Customer> customerManager)
        {
            _unitOfWork = unitOfWork;
            _customerManager = customerManager;
        }

        public bool checkHashPassWord(Customer customer, string passWord)
        {




            var check = _customerManager.CheckPasswordAsync(customer, passWord).Result;
            return check;
        }

        public List<CustomerListDataDto> GetAllCustomers()
        {
            var Customers = _unitOfWork.CustomerRepo.GetAll().Where(c => c.Role == UserRole.User).ToList();
            List<CustomerListDataDto> CustomerList = Customers.Select(c => new CustomerListDataDto
            {
                Id = new Guid(c.Id),
                FirstName = c.FirstName,
                MidName = c.MidName,
                LastName = c.LastName,
                Email = c.Email,
                Street = c.Street,
                City = c.City,
                Country = c.Country,
                PhoneNumber = c.PhoneNumber
            }).ToList();

            return CustomerList;
        }

        public CustomerListDataDto? GetCustomerById(Guid Id)
        {

            Customer? customer = _unitOfWork.CustomerRepo.GetById(Id.ToString());
            if (customer == null)
            {
                return null;
            }
            var customerDetalis = new CustomerListDataDto
            {
                Id = new Guid(customer.Id),
                FirstName = customer.FirstName,
                MidName = customer.MidName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Street = customer.Street,
                City = customer.City,
                Country = customer.Country,
                Email = customer.Email,

            };
            return customerDetalis;
        }
        public void AddCustomer(CustomerAddDto customer)
        {

            Customer newCustomer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = customer.FirstName,
                MidName = customer.MidName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Street = customer.Street,
                City = customer.City,
                Country = customer.Country,
                Role = UserRole.User,

            };

            //Hashing Password
            var creation = _customerManager.CreateAsync(newCustomer, customer.Password).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,newCustomer.Id),
                new(UserRole.User.ToString(),"Customer")
            };

            var addClaims = _customerManager.AddClaimsAsync(newCustomer, claims).Result;



            _unitOfWork.CustomerRepo.Add(newCustomer);
            _unitOfWork.SaveChange();
        }

        




         bool ICustomerManager.UpadateCustomerPassword(CustomerUpdatePassDto customer,string customerId)
        {
            var CurrentwithOldPass = _unitOfWork.CustomerRepo.GetById(customerId);

            var check = checkHashPassWord(CurrentwithOldPass!, customer.CurrentPassword);
            IdentityResult res = IdentityResult.Failed();
            if (check)
            {
                res = _customerManager.ChangePasswordAsync(CurrentwithOldPass!, customer.CurrentPassword, customer.NewPassword).Result;
                if (res.Succeeded)
                {
                    _unitOfWork.SaveChange();
                    return true;
                }
            }
            return false;
        }

        public bool UpdateCustomerData(CustomerUpdateDto customerUpdate,Guid customerId)
        {
           Customer? customeroldData = _unitOfWork.CustomerRepo.GetById(customerId.ToString()); 
            if(customeroldData == null)
            {
                return false;
            }
            var checkPass = _customerManager.CheckPasswordAsync(customeroldData, customerUpdate.Password).Result;
            if (!checkPass)
            {
                return false;
            }

            customeroldData.Email = customerUpdate.Email;
            customeroldData.FirstName = customerUpdate.FirstName;
            customeroldData.LastName = customerUpdate.LastName;
            customeroldData.MidName = customerUpdate.MidName;
            customeroldData.Street = customerUpdate.Street;
            customeroldData.City = customerUpdate.City;
            customeroldData.Country = (Countries)Enum.Parse(typeof(Countries), customerUpdate.Country);

            customeroldData.PhoneNumber = customerUpdate.PhoneNumber;

            _unitOfWork.SaveChange();

            return true;
           
        }

        public bool DeleteCustomerById(CustomerDeleteDto customerdel)
        {
            Customer? customer = _unitOfWork.CustomerRepo.GetById(customerdel.Id.ToString());
            if(customer == null)
            {
                return false;
            }

            _unitOfWork.CustomerRepo.Delete(customer);
            return _unitOfWork.SaveChange()>0;
        }
    }
}
