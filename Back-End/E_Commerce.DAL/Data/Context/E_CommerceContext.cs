using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;

namespace E_Commerce.DAL;

public class E_CommerceContext : IdentityDbContext
{
    public E_CommerceContext(DbContextOptions options) : base(options)
    {

    }
    //Customer Tables
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerReview> CustomersReviews => Set<CustomerReview>();
    //Product Tables
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductColorSizeQuantity> ProductsInfo => Set<ProductColorSizeQuantity>();
    public DbSet<Product_IMG> ProductImages => Set<Product_IMG>();

    //Cart
    public DbSet<Cart> Carts => Set<Cart>();


    //Order
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();

    //WishList
    public DbSet<WishList> WishLists => Set<WishList>();

    //Category

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<CartProduct> CartProduct => Set<CartProduct>();




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());





        #region CustomersSeeding

        var customers = new List<Customer>
        {
            new Customer
            {
                Id = "07d96ed8-155d-49c7-a77a-615f109d77c3",
                FirstName = "John",
                MidName = "E",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Street = "123 Main St",
                City = "New York",
                Country = Countries.Ukraine,
                NameOnCard = " John E Doe",
                CardNumber = 1234567890123456,
                ExpireDate = new DateTime(2024, 12, 31),
                PhoneNumber = "123-456-7890",
                Role=UserRole.User
            },
            new Customer
            {
                Id = "c7d3e80a-7a4a-4c54-91a6-89c0df051c94",
                FirstName = "Jane",
                MidName = "A",
                LastName = "Smith",
                NameOnCard = " Jane A Smith",
                Email = "janesmith@example.com",
                Street = "456 Elm St",
                City = "Los Angeles",
                Country = Countries.Turkey,
                CardNumber = 9876543210987654,
                ExpireDate = new DateTime(2025, 11, 30),
                PhoneNumber = "777-888-9999",

                Role = UserRole.User
            },
                new Customer
            {
                Id = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7",
                FirstName = "Emily",
                MidName = "R",
                LastName = "Anderson",
                Email = "emilyanderson@example.com",
                Street = "789 Elm St",
                City = "San Francisco",
                Country = Countries.Australia,
                NameOnCard = "Emily R Anderson",
                CardNumber = 9876543210123456,
                ExpireDate = new DateTime(2025, 6, 30),
                PhoneNumber = "111-222-3333",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8",
                FirstName = "Michael",
                MidName = "J",
                LastName = "Wilson",
                Email = "michaelwilson@example.com",
                Street = "456 Maple Ave",
                City = "London",
                Country = Countries.Albania,
                NameOnCard = "Michael J Wilson",
                CardNumber = 1234987654321098,
                ExpireDate = new DateTime(2024, 9, 15),
                PhoneNumber = "444-555-6666",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc",
                FirstName = "Sarah",
                MidName = "L",
                LastName = "Thompson",
                Email = "sarahthompson@example.com",
                Street = "789 Pine St",
                City = "Sydney",
                Country = Countries.Australia,
                NameOnCard = "Sarah L Thompson",
                CardNumber = 9876012345678901,
                ExpireDate = new DateTime(2023, 4, 1),
                PhoneNumber = "777-777-8888",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f",
                FirstName = "David",
                MidName = "M",
                LastName = "Miller",
                Email = "davidmiller@example.com",
                Street = "123 Oak Ave",
                City = "Toronto",
                Country = Countries.Canada,
                NameOnCard = "David M Miller",
                CardNumber = 5432109876543210,
                ExpireDate = new DateTime(2025, 3, 20),
                PhoneNumber = "999-888-7777",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "22ac8dc9-4385-48ae-90a3-7d8c898c6d5d",
                FirstName = "Sophia",
                MidName = "K",
                LastName = "Lee",
                Email = "sophialee@example.com",
                Street = "456 Cedar St",
                City = "Seoul",
                Country = Countries.Serbia,
                NameOnCard = "Sophia K Lee",
                CardNumber = 1234554321098765,
                ExpireDate = new DateTime(2024, 2, 10),
                PhoneNumber = "222-333-4444",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "b6a76b15-33e5-4d26-98b9-c948c7823b84",
                FirstName = "Daniel",
                MidName = "T",
                LastName = "Martinez",
                Email = "danielmartinez@example.com",
                Street = "789 Walnut Ave",
                City = "Madrid",
                Country = Countries.Spain,
                NameOnCard = "Daniel T Martinez",
                CardNumber = 9876543210012345,
                ExpireDate = new DateTime(2023, 12, 5),
                PhoneNumber = "555-666-7777",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "0e67a2e5-df53-4a92-9854-8e1ad46a4e61",
                FirstName = "Olivia",
                MidName = "N",
                LastName = "Brown",
                Email = "oliviabrown@example.com",
                Street = "123 Cherry St",
                City = "Paris",
                Country = Countries.France,
                NameOnCard = "Olivia N Brown",
                CardNumber = 5432101234567890,
                ExpireDate = new DateTime(2022, 11, 1),
                PhoneNumber = "888-777-6666",
                Role = UserRole.User
            },


            new Customer
            {
                Id ="74f5b2b3-3d10-4a85-93b5-8c6d0c992b58",
                FirstName = "Alex",
                MidName = "S",
                LastName = "Johnson",
                Email = "alexjohnson@example.com",
                Street = "789 Oak St",
                City = "Chicago",
                Country = Countries.Zimbabwe,
                NameOnCard = " Alex S Johnson",
                CardNumber = 5432167890123456,
                ExpireDate = new DateTime(2026, 11, 29),
                PhoneNumber = "777-888-666",
                Role=UserRole.User

            },
            new Customer
            {
                Id = "724587e6-9314-4fe6-9c3e-6fd612f50234",
                FirstName = "William",
                MidName = "G",
                LastName = "Taylor",
                Email = "williamtaylor@example.com",
                Street = "123 Elm St",
                City = "London",
                Country = Countries.Andorra,
                NameOnCard = "William G Taylor",
                CardNumber = 1234567812345678,
                ExpireDate = new DateTime(2023, 9, 30),
                PhoneNumber = "111-222-3333",
                Role = UserRole.User
            },
        new Customer
        {
            Id = "234cdf89-12a3-45b6-789c-0123456789de",
            FirstName = "Emma",
            MidName = "J",
            LastName = "Davis",
            Email = "emmajdavis@example.com",
            Street = "456 Maple Ave",
            City = "New York",
            Country = Countries.Bangladesh,
            NameOnCard = "Emma J Davis",
            CardNumber = 9876543298765432,
            ExpireDate = new DateTime(2025, 8, 31),
            PhoneNumber = "444-555-6666",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "6789abcd-ef01-2345-6789-abcd01234567",
            FirstName = "Liam",
            MidName = "M",
            LastName = "Wilson",
            Email = "liammwilson@example.com",
            Street = "789 Oak St",
            City = "Los Angeles",
            Country = Countries.Somalia,
            NameOnCard = "Liam M Wilson",
            CardNumber = 1234987654321098,
            ExpireDate = new DateTime(2024, 7, 15),
            PhoneNumber = "777-888-9999",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "bcdef012-3456-789a-bcde-f01234567890",
            FirstName = "Olivia",
            MidName = "L",
            LastName = "Thompson",
            Email = "olivialthompson@example.com",
            Street = "123 Pine St",
            City = "Sydney",
            Country = Countries.Australia,
            NameOnCard = "Olivia L Thompson",
            CardNumber = 9876012345678901,
            ExpireDate = new DateTime(2023, 12, 31),
            PhoneNumber = "777-777-8888",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "2345cdef-0123-4567-89ab-cdef01234567",
            FirstName = "Noah",
            MidName = "A",
            LastName = "Johnson",
            Email = "noahajohnson@example.com",
            Street = "456 Cedar St",
            City = "Seattle",
            Country = Countries.Kiribati,
            NameOnCard = "Noah A Johnson",
            CardNumber = 1234554321098765,
            ExpireDate = new DateTime(2024, 6, 30),
            PhoneNumber = "222-333-4444",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "8901def0-1234-5678-9abc-def012345678",
            FirstName = "Ava",
            MidName = "K",
            LastName = "Lee",
            Email = "avaklee@example.com",
            Street = "789 Walnut Ave",
            City = "San Francisco",
            Country = Countries.Uruguay,
            NameOnCard = "Ava K Lee",
            CardNumber = 9876543298765432,
            ExpireDate = new DateTime(2025, 5, 20),
            PhoneNumber = "555-666-7777",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "23456789-01ab-cdef-0123-456789abcdef",
            FirstName = "Isabella",
            MidName = "T",
            LastName = "Martinez",
            Email = "isabellatmartinez@example.com",
            Street = "123 Cherry St",
            City = "Madrid",
            Country = Countries.Spain,
            NameOnCard = "Isabella T Martinez",
            CardNumber = 5432109876543210,
            ExpireDate = new DateTime(2023, 4, 1),
            PhoneNumber = "888-777-6666",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "def01234-5678-9abc-def0-123456789abc",
            FirstName = "Sophia",
            MidName = "N",
            LastName = "Brown",
            Email = "sophianbrown@example.com",
            Street = "456 Maple Ave",
            City = "Paris",
            Country = Countries.France,
            NameOnCard = "Sophia N Brown",
            CardNumber = 1234567812345678,
            ExpireDate = new DateTime(2024, 3, 20),
            PhoneNumber = "999-888-7777",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "456789ab-cdef-0123-4567-89abcdef0123",
            FirstName = "Mia",
            MidName = "S",
            LastName = "Johnson",
            Email = "miasjohnson@example.com",
            Street = "789 Oak St",
            City = "Rome",
            Country = Countries.Italy,
            NameOnCard = "Mia S Johnson",
            CardNumber = 5432167890123456,
            ExpireDate = new DateTime(2023, 2, 10),
            PhoneNumber = "777-888-9999",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "56789abc-def0-1234-5678-9abcdef01234",
            FirstName = "Logan",
            MidName = "T",
            LastName = "Martinez",
            Email = "logantmartinez@example.com",
            Street = "123 Walnut Ave",
            City = "Tokyo",
            Country = Countries.Japan,
            NameOnCard = "Logan T Martinez",
            CardNumber = 1234987654321098,
            ExpireDate = new DateTime(2025, 1, 5),
            PhoneNumber = "555-666-7777",
            Role = UserRole.User
        }
};





        #endregion Carts

        #region Products Seeding

        var Productss = new List<Product>
        {

        new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's T-Shirt",
                Description = "Comfortable cotton t-shirt for men",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },
        new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's T-Shirt",
                Description = "Comfortable cotton t-shirt for men",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },
        new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's T-Shirt",
                Description = "Comfortable cotton t-shirt for men",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },
        new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's T-Shirt",
                Description = "Comfortable cotton t-shirt for men",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },

            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
                new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
                  new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
                    new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
                new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
                  new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
                    new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Warm hoodie for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Warm hoodie for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
                new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Warm hoodie for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
                  new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Warm hoodie for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
                    new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Warm hoodie for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's T-Shirt",
                Description = "Comfortable cotton t-shirt for men",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Dress",
                Description = "Elegant dress for women",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
             new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
               new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shoes",
                Description = "Colorful shoes for kids",
                Price = 29.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Jeans",
                Description = "Classic denim jeans for men",
                Price = 39.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Blouse",
                Description = "Stylish blouse for women",
                Price = 24.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Backpack",
                Description = "Spacious backpack for kids",
                Price = 19.99m,
                Discount = 0,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Backpack",
                Description = "Spacious backpack for kids",
                Price = 19.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shorts",
                Description = "Casual shorts for men",
                Price = 17.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Sandals",
                Description = "Comfortable sandals for women",
                Price = 34.99m,
                Discount = 0.2m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' T-Shirt",
                Description = "Adorable t-shirt for kids",
                Price = 12.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Sneakers",
                Description = "Stylish sneakers for men",
                Price = 59.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Skirt",
                Description = "Fashionable skirt for women",
                Price = 27.99m,
                Discount = 0,
                Rate = 0
            },
             new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Skirt",
                Description = "Fashionable skirt for women",
                Price = 27.99m,
                Discount = 0,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Skirt",
                Description = "Fashionable skirt for women",
                Price = 27.99m,
                Discount = 0,
                Rate = 0
            },
               new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Skirt",
                Description = "Fashionable skirt for women",
                Price = 27.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Jacket",
                Description = "Warm jacket for kids",
                Price = 39.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Polo Shirt",
                Description = "Classic polo shirt for men",
                Price = 22.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Jeans",
                Description = "Stylish denim jeans for women",
                Price = 44.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Dress",
                Description = "Cute dress for kids",
                Price = 32.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Jacket",
                Description = "Warm jacket for men",
                Price = 59.99m,
                Discount = 0.2m,
                Rate = 0
            },
             new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Jacket",
                Description = "Warm jacket for men",
                Price = 59.99m,
                Discount = 0.2m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Jacket",
                Description = "Warm jacket for men",
                Price = 59.99m,
                Discount = 0.2m,
                Rate = 0
            },
               new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Jacket",
                Description = "Warm jacket for men",
                Price = 59.99m,
                Discount = 0.2m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Sneakers",
                Description = "Sporty sneakers for women",
                Price = 49.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Trousers",
                Description = "Casual trousers for kids",
                Price = 21.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shirt",
                Description = "Formal shirt for men",
                Price = 34.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shirt",
                Description = "Formal shirt for men",
                Price = 34.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shirt",
                Description = "Formal shirt for men",
                Price = 34.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shirt",
                Description = "Formal shirt for men",
                Price = 34.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Jacket",
                Description = "Stylish jacket for women",
                Price = 54.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Hoodie",
                Description = "Cozy hoodie for kids",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
             new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Hoodie",
                Description = "Cozy hoodie for kids",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Hoodie",
                Description = "Cozy hoodie for kids",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
               new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Hoodie",
                Description = "Cozy hoodie for kids",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Sweater",
                Description = "Warm sweater for men",
                Price = 39.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Blazer",
                Description = "Elegant blazer for women",
                Price = 59.99m,
                Discount = 0.2m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shorts",
                Description = "Casual shorts for kids",
                Price = 15.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Pants",
                Description = "Classic pants for men",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
             new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Pants",
                Description = "Classic pants for men",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
              new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Pants",
                Description = "Classic pants for men",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
               new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Pants",
                Description = "Classic pants for men",
                Price = 49.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Sweater",
                Description = "Cozy sweater for women",
                Price = 39.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Shirt",
                Description = "Adorable shirt for kids",
                Price = 17.99m,
                Discount = 0.15m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Comfortable hoodie for men",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Comfortable hoodie for men",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Comfortable hoodie for men",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Hoodie",
                Description = "Comfortable hoodie for men",
                Price = 29.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Pants",
                Description = "Stylish pants for women",
                Price = 44.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Pants",
                Description = "Stylish pants for women",
                Price = 44.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Pants",
                Description = "Stylish pants for women",
                Price = 44.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Pants",
                Description = "Stylish pants for women",
                Price = 44.99m,
                Discount = 0.05m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Sweater",
                Description = "Warm sweater for kids",
                Price = 34.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Sweater",
                Description = "Warm sweater for kids",
                Price = 34.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Sneakers",
                Description = "Sporty sneakers for men",
                Price = 54.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Sneakers",
                Description = "Sporty sneakers for men",
                Price = 54.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's T-Shirt",
                Description = "Casual t-shirt for women",
                Price = 19.99m,
                Discount = 0.2m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Jacket",
                Description = "Stylish jacket for kids",
                Price = 39.99m,
                Discount = 0,
                Rate = 0
            },
            
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Jacket",
                Description = "Stylish jacket for kids",
                Price = 39.99m,
                Discount = 0,
                Rate = 0
            },
            
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Jacket",
                Description = "Stylish jacket for kids",
                Price = 39.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Men's Shorts",
                Description = "Comfortable shorts for men",
                Price = 24.99m,
                Discount = 0,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Women's Sandals",
                Description = "Fashionable sandals for women",
                Price = 29.99m,
                Discount = 0.1m,
                Rate = 0
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Trousers",
                Description = "Stylish trousers for kids",
                Price = 34.99m,
                Discount = 0.05m,
                Rate = 0
            }
            ,
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Trousers2",
                Description = "Stylish trousers for kids",
                Price = 34.99m,
                Discount = 0.05m,
                Rate = 0
            }
            ,
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Trousers3",
                Description = "Stylish trousers for kids",
                Price = 34.99m,
                Discount = 0.05m,
                Rate = 0
            }
            ,
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Kids' Trousers4",
                Description = "Stylish trousers for kids",
                Price = 34.99m,
                Discount = 0.05m,
                Rate = 0
            }


        };


        #endregion

        #region Customers

        modelBuilder.Entity<Customer>().HasData(

            new Customer
            {
                Id = "07d96ed8-155d-49c7-a77a-615f109d77c3",
                FirstName = "John",
                MidName = "E",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Street = "123 Main St",
                City = "New York",
                Country = Countries.Ukraine,
                NameOnCard = " John E Doe",
                CardNumber = 1234567890123456,
                ExpireDate = new DateTime(2024, 12, 31),
                PhoneNumber = "123-456-7890",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "c7d3e80a-7a4a-4c54-91a6-89c0df051c94",
                FirstName = "Jane",
                MidName = "A",
                LastName = "Smith",
                NameOnCard = " Jane A Smith",
                Email = "janesmith@example.com",
                Street = "456 Elm St",
                City = "Los Angeles",
                Country = Countries.Turkey,
                CardNumber = 9876543210987654,
                ExpireDate = new DateTime(2025, 11, 30),
                PhoneNumber = "777-888-9999",

                Role = UserRole.User
            },
                new Customer
                {
                    Id = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7",
                    FirstName = "Emily",
                    MidName = "R",
                    LastName = "Anderson",
                    Email = "emilyanderson@example.com",
                    Street = "789 Elm St",
                    City = "San Francisco",
                    Country = Countries.Australia,
                    NameOnCard = "Emily R Anderson",
                    CardNumber = 9876543210123456,
                    ExpireDate = new DateTime(2025, 6, 30),
                    PhoneNumber = "111-222-3333",
                    Role = UserRole.User
                },
            new Customer
            {
                Id = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8",
                FirstName = "Michael",
                MidName = "J",
                LastName = "Wilson",
                Email = "michaelwilson@example.com",
                Street = "456 Maple Ave",
                City = "London",
                Country = Countries.Albania,
                NameOnCard = "Michael J Wilson",
                CardNumber = 1234987654321098,
                ExpireDate = new DateTime(2024, 9, 15),
                PhoneNumber = "444-555-6666",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc",
                FirstName = "Sarah",
                MidName = "L",
                LastName = "Thompson",
                Email = "sarahthompson@example.com",
                Street = "789 Pine St",
                City = "Sydney",
                Country = Countries.Australia,
                NameOnCard = "Sarah L Thompson",
                CardNumber = 9876012345678901,
                ExpireDate = new DateTime(2023, 4, 1),
                PhoneNumber = "777-777-8888",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f",
                FirstName = "David",
                MidName = "M",
                LastName = "Miller",
                Email = "davidmiller@example.com",
                Street = "123 Oak Ave",
                City = "Toronto",
                Country = Countries.Canada,
                NameOnCard = "David M Miller",
                CardNumber = 5432109876543210,
                ExpireDate = new DateTime(2025, 3, 20),
                PhoneNumber = "999-888-7777",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "22ac8dc9-4385-48ae-90a3-7d8c898c6d5d",
                FirstName = "Sophia",
                MidName = "K",
                LastName = "Lee",
                Email = "sophialee@example.com",
                Street = "456 Cedar St",
                City = "Seoul",
                Country = Countries.Serbia,
                NameOnCard = "Sophia K Lee",
                CardNumber = 1234554321098765,
                ExpireDate = new DateTime(2024, 2, 10),
                PhoneNumber = "222-333-4444",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "b6a76b15-33e5-4d26-98b9-c948c7823b84",
                FirstName = "Daniel",
                MidName = "T",
                LastName = "Martinez",
                Email = "danielmartinez@example.com",
                Street = "789 Walnut Ave",
                City = "Madrid",
                Country = Countries.Spain,
                NameOnCard = "Daniel T Martinez",
                CardNumber = 9876543210012345,
                ExpireDate = new DateTime(2023, 12, 5),
                PhoneNumber = "555-666-7777",
                Role = UserRole.User
            },
            new Customer
            {
                Id = "0e67a2e5-df53-4a92-9854-8e1ad46a4e61",
                FirstName = "Olivia",
                MidName = "N",
                LastName = "Brown",
                Email = "oliviabrown@example.com",
                Street = "123 Cherry St",
                City = "Paris",
                Country = Countries.France,
                NameOnCard = "Olivia N Brown",
                CardNumber = 5432101234567890,
                ExpireDate = new DateTime(2022, 11, 1),
                PhoneNumber = "888-777-6666",
                Role = UserRole.User
            },


            new Customer
            {
                Id = "74f5b2b3-3d10-4a85-93b5-8c6d0c992b58",
                FirstName = "Alex",
                MidName = "S",
                LastName = "Johnson",
                Email = "alexjohnson@example.com",
                Street = "789 Oak St",
                City = "Chicago",
                Country = Countries.Zimbabwe,
                NameOnCard = " Alex S Johnson",
                CardNumber = 5432167890123456,
                ExpireDate = new DateTime(2026, 11, 29),
                PhoneNumber = "777-888-666",
                Role = UserRole.User

            },
            new Customer
            {
                Id = "724587e6-9314-4fe6-9c3e-6fd612f50234",
                FirstName = "William",
                MidName = "G",
                LastName = "Taylor",
                Email = "williamtaylor@example.com",
                Street = "123 Elm St",
                City = "London",
                Country = Countries.Andorra,
                NameOnCard = "William G Taylor",
                CardNumber = 1234567812345678,
                ExpireDate = new DateTime(2023, 9, 30),
                PhoneNumber = "111-222-3333",
                Role = UserRole.User
            },
        new Customer
        {
            Id = "234cdf89-12a3-45b6-789c-0123456789de",
            FirstName = "Emma",
            MidName = "J",
            LastName = "Davis",
            Email = "emmajdavis@example.com",
            Street = "456 Maple Ave",
            City = "New York",
            Country = Countries.Bangladesh,
            NameOnCard = "Emma J Davis",
            CardNumber = 9876543298765432,
            ExpireDate = new DateTime(2025, 8, 31),
            PhoneNumber = "444-555-6666",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "6789abcd-ef01-2345-6789-abcd01234567",
            FirstName = "Liam",
            MidName = "M",
            LastName = "Wilson",
            Email = "liammwilson@example.com",
            Street = "789 Oak St",
            City = "Los Angeles",
            Country = Countries.Somalia,
            NameOnCard = "Liam M Wilson",
            CardNumber = 1234987654321098,
            ExpireDate = new DateTime(2024, 7, 15),
            PhoneNumber = "777-888-9999",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "bcdef012-3456-789a-bcde-f01234567890",
            FirstName = "Olivia",
            MidName = "L",
            LastName = "Thompson",
            Email = "olivialthompson@example.com",
            Street = "123 Pine St",
            City = "Sydney",
            Country = Countries.Australia,
            NameOnCard = "Olivia L Thompson",
            CardNumber = 9876012345678901,
            ExpireDate = new DateTime(2023, 12, 31),
            PhoneNumber = "777-777-8888",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "2345cdef-0123-4567-89ab-cdef01234567",
            FirstName = "Noah",
            MidName = "A",
            LastName = "Johnson",
            Email = "noahajohnson@example.com",
            Street = "456 Cedar St",
            City = "Seattle",
            Country = Countries.Kiribati,
            NameOnCard = "Noah A Johnson",
            CardNumber = 1234554321098765,
            ExpireDate = new DateTime(2024, 6, 30),
            PhoneNumber = "222-333-4444",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "8901def0-1234-5678-9abc-def012345678",
            FirstName = "Ava",
            MidName = "K",
            LastName = "Lee",
            Email = "avaklee@example.com",
            Street = "789 Walnut Ave",
            City = "San Francisco",
            Country = Countries.Uruguay,
            NameOnCard = "Ava K Lee",
            CardNumber = 9876543298765432,
            ExpireDate = new DateTime(2025, 5, 20),
            PhoneNumber = "555-666-7777",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "23456789-01ab-cdef-0123-456789abcdef",
            FirstName = "Isabella",
            MidName = "T",
            LastName = "Martinez",
            Email = "isabellatmartinez@example.com",
            Street = "123 Cherry St",
            City = "Madrid",
            Country = Countries.Spain,
            NameOnCard = "Isabella T Martinez",
            CardNumber = 5432109876543210,
            ExpireDate = new DateTime(2023, 4, 1),
            PhoneNumber = "888-777-6666",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "def01234-5678-9abc-def0-123456789abc",
            FirstName = "Sophia",
            MidName = "N",
            LastName = "Brown",
            Email = "sophianbrown@example.com",
            Street = "456 Maple Ave",
            City = "Paris",
            Country = Countries.France,
            NameOnCard = "Sophia N Brown",
            CardNumber = 1234567812345678,
            ExpireDate = new DateTime(2024, 3, 20),
            PhoneNumber = "999-888-7777",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "456789ab-cdef-0123-4567-89abcdef0123",
            FirstName = "Mia",
            MidName = "S",
            LastName = "Johnson",
            Email = "miasjohnson@example.com",
            Street = "789 Oak St",
            City = "Rome",
            Country = Countries.Italy,
            NameOnCard = "Mia S Johnson",
            CardNumber = 5432167890123456,
            ExpireDate = new DateTime(2023, 2, 10),
            PhoneNumber = "777-888-9999",
            Role = UserRole.User
        },
        new Customer
        {
            Id = "56789abc-def0-1234-5678-9abcdef01234",
            FirstName = "Logan",
            MidName = "T",
            LastName = "Martinez",
            Email = "logantmartinez@example.com",
            Street = "123 Walnut Ave",
            City = "Tokyo",
            Country = Countries.Japan,
            NameOnCard = "Logan T Martinez",
            CardNumber = 1234987654321098,
            ExpireDate = new DateTime(2025, 1, 5),
            PhoneNumber = "555-666-7777",
            Role = UserRole.User
        });
        #endregion

        #region Orders


        modelBuilder.Entity<Order>().HasData(
           new Order
           {
               Id = Guid.Parse("c7d3e80a-7a4a-4c54-91a6-89c0df051c94"),
               OrderData = new DateTime(2029, 6, 10),
               PaymentStatus = PaymentStatus.Paid,
               Street = "567 Oak St",
               City = "Chicago",
               Country = Countries.Iraq,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.1,
               ArrivalDate = new DateTime(2029, 6, 15),
               CustomerId = "07d96ed8-155d-49c7-a77a-615f109d77c3"
           },

           new Order
           {
               Id = Guid.Parse("6789abcd-ef01-2345-6789-abcd01234567"),
               OrderData = new DateTime(2029, 7, 1),
               PaymentStatus = PaymentStatus.Paid,
               Street = "789 Elm St",
               City = "Seattle",
               Country = Countries.Libya,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.2,
               ArrivalDate = new DateTime(2029, 7, 5),
               CustomerId = "c7d3e80a-7a4a-4c54-91a6-89c0df051c94"
           },

           new Order
           {
               Id = Guid.Parse("8901def0-1234-5678-9abc-def012345678"),
               OrderData = new DateTime(2029, 8, 10),
               PaymentStatus = PaymentStatus.Unpaid,
               Street = "123 Pine St",
               City = "San Francisco",
               Country = Countries.Afghanistan,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Pending,
               Discount = 0,
               ArrivalDate = new DateTime(2029, 8, 15),
               CustomerId = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7"
           },

           new Order
           {
               Id = Guid.Parse("b6a76b15-33e5-4d26-98b9-c948c7823b84"),
               OrderData = new DateTime(2029, 9, 1),
               PaymentStatus = PaymentStatus.Paid,
               Street = "456 Maple Ave",
               City = "Los Angeles",
               Country = Countries.Andorra,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.1,
               ArrivalDate = new DateTime(2029, 9, 5),
               CustomerId = "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8"
           },

           new Order
           {
               Id = Guid.Parse("07d96ed8-155d-49c7-a77a-615f109d77c3"),
               OrderData = new DateTime(2026, 11, 29),
               PaymentStatus = PaymentStatus.Paid,
               Street = "789 Oak St",
               City = "Chicago",
               Country = Countries.Zimbabwe,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Pending,
               Discount = 1,
               ArrivalDate = new DateTime(2026, 11, 29),
               CustomerId = "e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc"

           },
           new Order
           {
               Id = Guid.Parse("07d96ed8-155d-49c7-a77a-615f109d75c3"),
               OrderData = new DateTime(2026, 11, 29),
               PaymentStatus = PaymentStatus.Paid,
               Street = "789 Oak St",
               City = "Chicago",
               Country = Countries.Zimbabwe,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Pending,
               Discount = 1,
               ArrivalDate = new DateTime(2026, 11, 29),
               CustomerId = "b6a76b15-33e5-4d26-98b9-c948c7823b84"
           },
           new Order
           {
               Id = Guid.Parse("0e67a2e5-df53-4a92-9854-8e1ad46a4e61"),
               OrderData = new DateTime(2027, 1, 15),
               PaymentStatus = PaymentStatus.Unpaid,
               Street = "123 Elm St",
               City = "New York",
               Country = Countries.Belgium,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Processing,
               Discount = 0,
               ArrivalDate = new DateTime(2027, 1, 18),
               CustomerId = "0e67a2e5-df53-4a92-9854-8e1ad46a4e61"
           },

           new Order
           {
               Id = Guid.Parse("22ac8dc9-4385-48ae-90a3-7d8c898c6d5d"),
               OrderData = new DateTime(2027, 3, 10),
               PaymentStatus = PaymentStatus.Paid,
               Street = "456 Main St",
               City = "Los Angeles",
               Country = Countries.Belize,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Shipped,
               Discount = 0.5,
               ArrivalDate = new DateTime(2027, 3, 15),
               CustomerId = "74f5b2b3-3d10-4a85-93b5-8c6d0c992b58"
           },


           new Order
           {
               Id = Guid.Parse("f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f"),
               OrderData = new DateTime(2028, 12, 31),
               PaymentStatus = PaymentStatus.Unpaid,
               Street = "789 Elm St",
               City = "Chicago",
               Country = Countries.Canada,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.2,
               ArrivalDate = new DateTime(2029, 1, 5),
               CustomerId = "724587e6-9314-4fe6-9c3e-6fd612f50234"
           },
           new Order
           {
               Id = Guid.Parse("23456789-01ab-cdef-0123-456789abcdef"),
               OrderData = new DateTime(2027, 4, 5),
               PaymentStatus = PaymentStatus.Paid,
               Street = "321 Maple Ave",
               City = "San Francisco",
               Country = Countries.Oman,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.1,
               ArrivalDate = new DateTime(2027, 4, 10),
               CustomerId = "234cdf89-12a3-45b6-789c-0123456789de"
           },
           new Order
           {
               Id = Guid.Parse("2345cdef-0123-4567-89ab-cdef11234567"),
               OrderData = new DateTime(2027, 5, 15),
               PaymentStatus = PaymentStatus.Paid,
               Street = "567 Pine St",
               City = "Seattle",
               Country = Countries.Taiwan,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.2,
               ArrivalDate = new DateTime(2027, 5, 20),
               CustomerId = "6789abcd-ef01-2345-6789-abcd01234567"
           },

           new Order
           {
               Id = Guid.Parse("def01234-5678-9abc-def0-113456789abc"),
               OrderData = new DateTime(2028, 11, 20),
               PaymentStatus = PaymentStatus.Paid,
               Street = "901 Cherry Ln",
               City = "Miami",
               Country = Countries.Fiji,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.3,
               ArrivalDate = new DateTime(2028, 11, 25),
               CustomerId = "bcdef012-3456-789a-bcde-f01234567890"
           },

           new Order
           {
               Id = Guid.Parse("e23edc32-bd6a-4b6b-a28e-ccf90b5c32dc"),
               OrderData = new DateTime(2028, 12, 10),
               PaymentStatus = PaymentStatus.Paid,
               Street = "246 Elm St",
               City = "Boston",
               Country = Countries.Denmark,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.15,
               ArrivalDate = new DateTime(2028, 12, 15),
               CustomerId = "2345cdef-0123-4567-89ab-cdef01234567"
           },

           new Order
           {
               Id = Guid.Parse("f0e7f09e-c7ad-4cb0-8f19-6540b5c7c49f"),
               OrderData = new DateTime(2029, 1, 1),
               PaymentStatus = PaymentStatus.Unpaid,
               Street = "789 Elm St",
               City = "Chicago",
               Country = Countries.Canada,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.2,
               ArrivalDate = new DateTime(2029, 1, 5),
               CustomerId = "8901def0-1234-5678-9abc-def012345678"
           },
           new Order
           {
               Id = Guid.Parse("724587e6-9314-4fe6-9c3e-7fd612f50234"),
               OrderData = new DateTime(2029, 2, 20),
               PaymentStatus = PaymentStatus.Paid,
               Street = "987 Cedar St",
               City = "Dallas",
               Country = Countries.Samoa,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.1,
               ArrivalDate = new DateTime(2029, 2, 25),
               CustomerId = "b6a76b15-33e5-4d26-98b9-c948c7823b84"
           },

           new Order
           {
               Id = Guid.Parse("74f5b2b3-3d10-4a85-93b5-8c6d0c992b58"),
               OrderData = new DateTime(2029, 3, 5),
               PaymentStatus = PaymentStatus.Unpaid,
               Street = "456 Oak St",
               City = "San Diego",
               Country = Countries.Samoa,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Pending,
               Discount = 0,
               ArrivalDate = new DateTime(2029, 3, 10),
               CustomerId = "bcdef012-3456-789a-bcde-f01234567890"
           },
           new Order
           {
               Id = Guid.Parse("724587e6-9314-4fe6-9c3e-7fd612f50233"),
               OrderData = new DateTime(2029, 4, 15),
               PaymentStatus = PaymentStatus.Paid,
               Street = "789 Pine St",
               City = "Seattle",
               Country = Countries.Samoa,
               PaymentMethod = PaymentMethod.CashOnDelivery,
               OrderStatus = OrderStatus.Processing,
               Discount = 0.2,
               ArrivalDate = new DateTime(2029, 4, 20),
               CustomerId = "c7d3e80a-7a4a-4c54-91a6-89c0df051c94"
           },

           new Order
           {
               Id = Guid.Parse("724587e6-9314-4fe6-9c3e-7fd612f50232"),
               OrderData = new DateTime(2029, 5, 1),
               PaymentStatus = PaymentStatus.Paid,
               Street = "123 Maple Ave",
               City = "San Francisco",
               Country = Countries.Senegal,
               PaymentMethod = PaymentMethod.CreditCard,
               OrderStatus = OrderStatus.Pending,
               Discount = 0.3,
               ArrivalDate = new DateTime(2029, 5, 5),
               CustomerId = "def01234-5678-9abc-def0-123456789abc"
           }
           );
        #endregion

        #region Categories


        modelBuilder.Entity<Category>().HasData(

       //Paren Categories

       new Category
       {
           Id = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583"),
           Name = "Men",
           Description = "Men's Clothing",
           Image = "men.jpg",
           ParentCategoryId = null
       },
       new Category
       {
           Id = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d"),
           Name = "Women",
           Description = "Women's Clothing",
           Image = "Women.jpg",
           ParentCategoryId = null
       },
       new Category
       {
           Id = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f"),
           Name = "Kids",
           Description = "Kids's Clothing",
           Image = "Kids.jpg",
           ParentCategoryId = null
       },

       //shirts

       new Category
       {
           Id = Guid.Parse("f032f788-2340-431f-9f8f-eeb176a35177"),
           Name = "Shirts",
           Description = "Mens shirts's Clothing",
           Image = "men shirts.jpg",
           ParentCategoryId = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583")
       },
       new Category
       {
           Id = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d3a3d"),
           Name = "Shirts",
           Description = "Women shirts's Clothing",
           Image = "Women shirts.jpg",
           ParentCategoryId = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d")
       },
       new Category
       {
           Id = Guid.Parse("8a6d4a19-47cc-4a4e-822b-cac1de2efc8d"),
           Name = "Shirts",
           Description = "Kids shirts's Clothing",
           Image = "Kids shirts.jpg",
           ParentCategoryId = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f")
       },
       //pants

       new Category
       {
           Id = Guid.Parse("9a938bc1-0717-4b8d-8f8b-3a2f55de49db"),
           Name = "Pants",
           Description = "Men Pants's Clothing",
           Image = "men Pants.jpg",
           ParentCategoryId = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583")
       },
       new Category
       {
           Id = Guid.Parse("d9f02e92-d14c-4b6d-86ad-6e4e6c48020a"),
           Name = "Pants",
           Description = "Women Pants's Clothing",
           Image = "Women Pants.jpg",
           ParentCategoryId = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d")
       },
       new Category
       {
           Id = Guid.Parse("1d53debe-03e6-487f-9b34-6b26c68fc1e5"),
           Name = "Pants",
           Description = "Kids Pants's Clothing",
           Image = "Kids Pants.jpg",
           ParentCategoryId = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f")
       },

       //Shoes

       new Category
       {
           Id = Guid.Parse("6b3c4ef5-01ad-49c7-a8ff-36ae55d0ce8d"),
           Name = "Shoes",
           Description = "Men Shoes's Clothing",
           Image = "men Shoes.jpg",
           ParentCategoryId = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583")
       },
       new Category
       {
           Id = Guid.Parse("35b303b9-25a0-4379-89b3-64e4ae51291f"),
           Name = "Shoes",
           Description = "Women Shoes's Clothing",
           Image = "Women Shoes.jpg",
           ParentCategoryId = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d")
       },
       new Category
       {
           Id = Guid.Parse("ca09f6a1-5b87-4b56-9ee3-c6fb6ad070c2"),
           Name = "Shoes",
           Description = "Kids Shoes's Clothing",
           Image = "Kids Shoes.jpg",
           ParentCategoryId = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f")
       },

       //Jackets

       new Category
       {
           Id = Guid.Parse("6f6c6c4c-9e6e-4e0c-97cc-8b52c055918b"),
           Name = "Jackets",
           Description = "Men Jackets's Clothing",
           Image = "men Jackets.jpg",
           ParentCategoryId = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583")
       },
       new Category
       {
           Id = Guid.Parse("47a38a48-8747-4461-ba32-7e573be663ee"),
           Name = "Jackets",
           Description = "Women Jackets's Clothing",
           Image = "Women Jackets.jpg",
           ParentCategoryId = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d")
       },
       new Category
       {
           Id = Guid.Parse("b19a53a3-04e7-4804-84bc-84da64d738a6"),
           Name = "Jackets",
           Description = "Kids Jackets's Clothing",
           Image = "Kids Jackets.jpg",
           ParentCategoryId = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f")
       },

        //Hoodies

        new Category
        {
            Id = Guid.Parse("a6d7e8b5-2f4d-4f51-b24b-4fcb52e36f5f"),
            Name = "Hoodies",
            Description = "Men Hoodies's Clothing",
            Image = "men Hoodies.jpg",
            ParentCategoryId = Guid.Parse("edc6b9e0-9252-4e9d-b4d3-9203b6de2583")
        },
       new Category
       {
           Id = Guid.Parse("e18e42b7-799e-4b3b-a084-c55d4bb5da3f"),
           Name = "Hoodies",
           Description = "Women Hoodies's Clothing",
           Image = "Women Hoodies.jpg",
           ParentCategoryId = Guid.Parse("a6c4de53-33c5-48e1-9f21-5649726d2a3d")
       },
       new Category
       {
           Id = Guid.Parse("c2ae51c9-913a-4e7d-a7b5-ef1efc8f9d3e"),
           Name = "Hoodies",
           Description = "Kids Hoodies's Clothing",
           Image = "Kids Hoodies.jpg",
           ParentCategoryId = Guid.Parse("52d40b0a-7039-4bc6-899d-0c36adff6b8f")
       });
        #endregion





        #region Test prod

        modelBuilder.Entity<Product>().HasData(Productss);

        #endregion
        //#region Products


        //modelBuilder.Entity<Product>().HasData(
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's T-Shirt",
        //        Description = "Comfortable cotton t-shirt for men",
        //        Price = 15.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Dress",
        //        Description = "Elegant dress for women",
        //        Price = 49.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Shoes",
        //        Description = "Colorful shoes for kids",
        //        Price = 29.99m,
        //        Discount = 0.15m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Hoodie",
        //        Description = "Warm hoodie for men",
        //        Price = 39.99m,
        //        Discount = 0.05m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's T-Shirt",
        //        Description = "Comfortable cotton t-shirt for men",
        //        Price = 15.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Dress",
        //        Description = "Elegant dress for women",
        //        Price = 49.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Shoes",
        //        Description = "Colorful shoes for kids",
        //        Price = 29.99m,
        //        Discount = 0.15m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Jeans",
        //        Description = "Classic denim jeans for men",
        //        Price = 39.99m,
        //        Discount = 0.05m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Blouse",
        //        Description = "Stylish blouse for women",
        //        Price = 24.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Backpack",
        //        Description = "Spacious backpack for kids",
        //        Price = 19.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Shorts",
        //        Description = "Casual shorts for men",
        //        Price = 17.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Sandals",
        //        Description = "Comfortable sandals for women",
        //        Price = 34.99m,
        //        Discount = 0.2m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' T-Shirt",
        //        Description = "Adorable t-shirt for kids",
        //        Price = 12.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Sneakers",
        //        Description = "Stylish sneakers for men",
        //        Price = 59.99m,
        //        Discount = 0.15m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Skirt",
        //        Description = "Fashionable skirt for women",
        //        Price = 27.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Jacket",
        //        Description = "Warm jacket for kids",
        //        Price = 39.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Polo Shirt",
        //        Description = "Classic polo shirt for men",
        //        Price = 22.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Jeans",
        //        Description = "Stylish denim jeans for women",
        //        Price = 44.99m,
        //        Discount = 0.05m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Dress",
        //        Description = "Cute dress for kids",
        //        Price = 32.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Jacket",
        //        Description = "Warm jacket for men",
        //        Price = 59.99m,
        //        Discount = 0.2m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Sneakers",
        //        Description = "Sporty sneakers for women",
        //        Price = 49.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Trousers",
        //        Description = "Casual trousers for kids",
        //        Price = 21.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Shirt",
        //        Description = "Formal shirt for men",
        //        Price = 34.99m,
        //        Discount = 0.15m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Jacket",
        //        Description = "Stylish jacket for women",
        //        Price = 54.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Hoodie",
        //        Description = "Cozy hoodie for kids",
        //        Price = 29.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Sweater",
        //        Description = "Warm sweater for men",
        //        Price = 39.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Blazer",
        //        Description = "Elegant blazer for women",
        //        Price = 59.99m,
        //        Discount = 0.2m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Shorts",
        //        Description = "Casual shorts for kids",
        //        Price = 15.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Pants",
        //        Description = "Classic pants for men",
        //        Price = 49.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Sweater",
        //        Description = "Cozy sweater for women",
        //        Price = 39.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Shirt",
        //        Description = "Adorable shirt for kids",
        //        Price = 17.99m,
        //        Discount = 0.15m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Hoodie",
        //        Description = "Comfortable hoodie for men",
        //        Price = 29.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Pants",
        //        Description = "Stylish pants for women",
        //        Price = 44.99m,
        //        Discount = 0.05m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Sweater",
        //        Description = "Warm sweater for kids",
        //        Price = 34.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Sneakers",
        //        Description = "Sporty sneakers for men",
        //        Price = 54.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's T-Shirt",
        //        Description = "Casual t-shirt for women",
        //        Price = 19.99m,
        //        Discount = 0.2m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Jacket",
        //        Description = "Stylish jacket for kids",
        //        Price = 39.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Men's Shorts",
        //        Description = "Comfortable shorts for men",
        //        Price = 24.99m,
        //        Discount = 0,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Women's Sandals",
        //        Description = "Fashionable sandals for women",
        //        Price = 29.99m,
        //        Discount = 0.1m,
        //        Rate = 0
        //    },
        //    new Product
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Kids' Trousers",
        //        Description = "Stylish trousers for kids",
        //        Price = 34.99m,
        //        Discount = 0.05m,
        //        Rate = 0
        //    });
        //#endregion

        #region Carts


        var carts = new List<Cart> {
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[1].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[2].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[3].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[4].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[5].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[6].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[7].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[8].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[9].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[10].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[11].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[12].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[13].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[14].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[15].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[16].Id)},
        new Cart{CartId = Guid.NewGuid(),CustomerId = Guid.Parse(customers[17].Id)},

         };




        modelBuilder.Entity<Cart>().HasData(carts);

        #endregion



        #region Product Images
        var ProductImgs = new List<Product_IMG>
        {
            new Product_IMG{ProductID=Productss[0].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[1].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[2].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[3].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[4].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[5].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[6].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[7].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[8].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[9].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},
            new Product_IMG{ProductID=Productss[10].Id, ImageURL="https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642"},

        };


        #endregion
        modelBuilder.Entity<Product_IMG>().HasData(ProductImgs);

    }


}
