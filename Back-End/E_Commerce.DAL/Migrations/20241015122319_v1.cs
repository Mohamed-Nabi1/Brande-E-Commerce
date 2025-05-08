using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.ProductId, x.CartId, x.Size, x.Color });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.ProductID, x.ImageURL });
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInfo",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInfo", x => new { x.ProductID, x.Color, x.Size });
                    table.ForeignKey(
                        name: "FK_ProductsInfo_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WishListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MidName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartId");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_WishLists_WishListID",
                        column: x => x.WishListID,
                        principalTable: "WishLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductWishList",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishListsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWishList", x => new { x.ProductsId, x.WishListsId });
                    table.ForeignKey(
                        name: "FK_ProductWishList_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWishList_WishLists_WishListsId",
                        column: x => x.WishListsId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomersReviews",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersReviews", x => new { x.ProductId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CustomersReviews_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.ProductId, x.OrderId, x.Color, x.Size });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CardNumber", "CartID", "City", "ConcurrencyStamp", "Country", "Discriminator", "Email", "EmailConfirmed", "ExpireDate", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MidName", "NameOnCard", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Street", "TwoFactorEnabled", "UserName", "WishListID" },
                values: new object[,]
                {
                    { "07d96ed8-155d-49c7-a77a-615f109d77c3", 0, 1234567890123456m, null, "New York", "30a2b3e9-4fa5-4383-acee-0b1dadbd3f93", "Ukraine", "Customer", "johndoe@example.com", false, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe", false, null, "E", " John E Doe", null, null, null, "123-456-7890", false, "User", "3915136e-70d7-42bd-8ae5-14d2af39f473", "123 Main St", false, null, null },
                    { "0e67a2e5-df53-4a92-9854-8e1ad46a4e61", 0, 5432101234567890m, null, "Paris", "e3a75204-633f-4726-950d-214bd73d88d3", "France", "Customer", "oliviabrown@example.com", false, new DateTime(2022, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olivia", "Brown", false, null, "N", "Olivia N Brown", null, null, null, "888-777-6666", false, "User", "39f4076a-9363-4c0a-8136-8408fe602ffe", "123 Cherry St", false, null, null },
                    { "22ac8dc9-4385-48ae-90a3-7d8c898c6d5d", 0, 1234554321098765m, null, "Seoul", "39ea4c6c-6f74-49c0-8af2-b1ea3c2eb629", "Serbia", "Customer", "sophialee@example.com", false, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophia", "Lee", false, null, "K", "Sophia K Lee", null, null, null, "222-333-4444", false, "User", "2c1643b7-6e73-4e9c-a751-045a9797c109", "456 Cedar St", false, null, null },
                    { "23456789-01ab-cdef-0123-456789abcdef", 0, 5432109876543210m, null, "Madrid", "10dd92a2-990d-4324-9f22-fb7c19ab64d9", "Spain", "Customer", "isabellatmartinez@example.com", false, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Isabella", "Martinez", false, null, "T", "Isabella T Martinez", null, null, null, "888-777-6666", false, "User", "ce9da349-dffe-4e56-a7ce-64202fffb1ce", "123 Cherry St", false, null, null },
                    { "2345cdef-0123-4567-89ab-cdef01234567", 0, 1234554321098765m, null, "Seattle", "2005c356-eb00-4775-95a8-62d6e5cad068", "Kiribati", "Customer", "noahajohnson@example.com", false, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Noah", "Johnson", false, null, "A", "Noah A Johnson", null, null, null, "222-333-4444", false, "User", "a2981c3f-d040-4c59-9531-5e5a87f67174", "456 Cedar St", false, null, null },
                    { "234cdf89-12a3-45b6-789c-0123456789de", 0, 9876543298765432m, null, "New York", "7b1663c8-1919-4333-9587-444d634165a9", "Bangladesh", "Customer", "emmajdavis@example.com", false, new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma", "Davis", false, null, "J", "Emma J Davis", null, null, null, "444-555-6666", false, "User", "b98d9e57-4516-4ea7-bc61-89f4186eca12", "456 Maple Ave", false, null, null },
                    { "456789ab-cdef-0123-4567-89abcdef0123", 0, 5432167890123456m, null, "Rome", "680150cf-e486-449f-a921-ca04af36c7eb", "Italy", "Customer", "miasjohnson@example.com", false, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mia", "Johnson", false, null, "S", "Mia S Johnson", null, null, null, "777-888-9999", false, "User", "e5445304-226d-42f0-a413-1256790d0419", "789 Oak St", false, null, null },
                    { "56789abc-def0-1234-5678-9abcdef01234", 0, 1234987654321098m, null, "Tokyo", "e186dc43-25c7-46df-ae9e-1eb1bf709072", "Japan", "Customer", "logantmartinez@example.com", false, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Logan", "Martinez", false, null, "T", "Logan T Martinez", null, null, null, "555-666-7777", false, "User", "b0172a67-f109-4c2a-8fbe-56ddc260df3f", "123 Walnut Ave", false, null, null },
                    { "6789abcd-ef01-2345-6789-abcd01234567", 0, 1234987654321098m, null, "Los Angeles", "9f3a6a3f-50b3-43ef-9366-0d62115fac9b", "Somalia", "Customer", "liammwilson@example.com", false, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liam", "Wilson", false, null, "M", "Liam M Wilson", null, null, null, "777-888-9999", false, "User", "5f2d417b-c2b9-4311-8dfa-4415ef02a540", "789 Oak St", false, null, null },
                    { "724587e6-9314-4fe6-9c3e-6fd612f50234", 0, 1234567812345678m, null, "London", "620327ed-6e33-48ce-a960-0098eff9cbd6", "Andorra", "Customer", "williamtaylor@example.com", false, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "William", "Taylor", false, null, "G", "William G Taylor", null, null, null, "111-222-3333", false, "User", "778743d7-2a7e-48ac-af27-087f6397a5d9", "123 Elm St", false, null, null },
                    { "74f5b2b3-3d10-4a85-93b5-8c6d0c992b58", 0, 5432167890123456m, null, "Chicago", "9d6eda67-ced3-4606-8d2f-8ff57e1fffeb", "Zimbabwe", "Customer", "alexjohnson@example.com", false, new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alex", "Johnson", false, null, "S", " Alex S Johnson", null, null, null, "777-888-666", false, "User", "0d1c062c-a73a-4b40-abb9-da8442f89e7f", "789 Oak St", false, null, null },
                    { "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7", 0, 9876543210123456m, null, "San Francisco", "895758c4-e362-427a-bb09-9310e0fd4c33", "Australia", "Customer", "emilyanderson@example.com", false, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily", "Anderson", false, null, "R", "Emily R Anderson", null, null, null, "111-222-3333", false, "User", "394baf87-ba70-408c-a500-e42fc0ddf9bf", "789 Elm St", false, null, null },
                    { "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8", 0, 1234987654321098m, null, "London", "80ae0073-5974-4299-963b-06b94a18fb75", "Albania", "Customer", "michaelwilson@example.com", false, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Wilson", false, null, "J", "Michael J Wilson", null, null, null, "444-555-6666", false, "User", "b7550307-d3ff-4e5d-94b1-3b11747ec31a", "456 Maple Ave", false, null, null },
                    { "8901def0-1234-5678-9abc-def012345678", 0, 9876543298765432m, null, "San Francisco", "f14babb9-0a2d-46d5-98fb-c41f2c90f40d", "Uruguay", "Customer", "avaklee@example.com", false, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ava", "Lee", false, null, "K", "Ava K Lee", null, null, null, "555-666-7777", false, "User", "7a1a4737-cd9d-4015-a759-b3db58704245", "789 Walnut Ave", false, null, null },
                    { "b6a76b15-33e5-4d26-98b9-c948c7823b84", 0, 9876543210012345m, null, "Madrid", "0367630e-118e-4cce-b546-4cfec4626e45", "Spain", "Customer", "danielmartinez@example.com", false, new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", "Martinez", false, null, "T", "Daniel T Martinez", null, null, null, "555-666-7777", false, "User", "7fae07a2-fa9f-4597-bc4e-2c883808a13c", "789 Walnut Ave", false, null, null },
                    { "bcdef012-3456-789a-bcde-f01234567890", 0, 9876012345678901m, null, "Sydney", "46ff76d4-105f-43a6-8441-fc69429cebf2", "Australia", "Customer", "olivialthompson@example.com", false, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olivia", "Thompson", false, null, "L", "Olivia L Thompson", null, null, null, "777-777-8888", false, "User", "288404cb-bb7a-4428-9026-517d808b5007", "123 Pine St", false, null, null },
                    { "c7d3e80a-7a4a-4c54-91a6-89c0df051c94", 0, 9876543210987654m, null, "Los Angeles", "c3e718ca-6fe5-4645-ab16-b22606ebd5ce", "Turkey", "Customer", "janesmith@example.com", false, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith", false, null, "A", " Jane A Smith", null, null, null, "777-888-9999", false, "User", "17bab45b-0b00-4f73-8c13-4e2c89a1f554", "456 Elm St", false, null, null },
                    { "def01234-5678-9abc-def0-123456789abc", 0, 1234567812345678m, null, "Paris", "41c058ae-252c-42f7-af38-a135873b505c", "France", "Customer", "sophianbrown@example.com", false, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophia", "Brown", false, null, "N", "Sophia N Brown", null, null, null, "999-888-7777", false, "User", "1099d52f-c87d-4193-8ed1-c53bd87fe8f9", "456 Maple Ave", false, null, null },
                    { "e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc", 0, 9876012345678901m, null, "Sydney", "f88f6a8a-7c40-4c8d-9a9f-6f074f81b8cd", "Australia", "Customer", "sarahthompson@example.com", false, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah", "Thompson", false, null, "L", "Sarah L Thompson", null, null, null, "777-777-8888", false, "User", "ef30c222-9033-42bc-bbb5-af81383e0420", "789 Pine St", false, null, null },
                    { "f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f", 0, 5432109876543210m, null, "Toronto", "6f4afea9-fa47-4ff0-a7bd-8a58d72beb6b", "Canada", "Customer", "davidmiller@example.com", false, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Miller", false, null, "M", "David M Miller", null, null, null, "999-888-7777", false, "User", "f25b9c4c-ec03-4fa4-a903-37195cb56f56", "123 Oak Ave", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("053507f6-c3bf-47e3-b3fd-a9bc5eb69117"), new Guid("22ac8dc9-4385-48ae-90a3-7d8c898c6d5d") },
                    { new Guid("0866d6e6-54a4-4d42-86ba-b9e6d32a9828"), new Guid("c7d3e80a-7a4a-4c54-91a6-89c0df051c94") },
                    { new Guid("2a8817db-441b-4f62-8a52-cae1588ee67b"), new Guid("6789abcd-ef01-2345-6789-abcd01234567") },
                    { new Guid("3969cfee-3244-4013-af57-ccdb7e740b91"), new Guid("234cdf89-12a3-45b6-789c-0123456789de") },
                    { new Guid("504786e0-179b-43a6-8726-9c54be305260"), new Guid("23456789-01ab-cdef-0123-456789abcdef") },
                    { new Guid("52e4aa4e-563c-41ac-b6d7-1d044388f267"), new Guid("0e67a2e5-df53-4a92-9854-8e1ad46a4e61") },
                    { new Guid("6839b70f-3f46-468d-90b5-b0b57f849e12"), new Guid("8901def0-1234-5678-9abc-def012345678") },
                    { new Guid("6d39efc4-7644-48c9-9145-742e82f34230"), new Guid("724587e6-9314-4fe6-9c3e-6fd612f50234") },
                    { new Guid("6dc9ff7d-82bf-4cb2-879a-28795dc5cb28"), new Guid("2345cdef-0123-4567-89ab-cdef01234567") },
                    { new Guid("72f5722d-63ea-4832-b30b-55e99222bd83"), new Guid("e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc") },
                    { new Guid("93361519-1445-4c8d-9119-83bb7abaae34"), new Guid("74f5b2b3-3d10-4a85-93b5-8c6d0c992b58") },
                    { new Guid("9c222da6-ea26-4c6d-8190-7441610edfeb"), new Guid("bcdef012-3456-789a-bcde-f01234567890") },
                    { new Guid("a758f3ba-3d5d-4954-b6b9-dcc792728f4f"), new Guid("74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8") },
                    { new Guid("c5c8db66-8804-4885-8a3a-d20524bb36b0"), new Guid("f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f") },
                    { new Guid("d40d2a91-9392-4e40-831e-c32ea540fa1a"), new Guid("74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7") },
                    { new Guid("d5aaa36e-5396-4c40-95df-c07978dacfa4"), new Guid("def01234-5678-9abc-def0-123456789abc") },
                    { new Guid("d623d523-1010-4986-9206-af286b2b951d"), new Guid("b6a76b15-33e5-4d26-98b9-c948c7823b84") }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Image", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f"), "Kids's Clothing", "Kids.jpg", "Kids", null },
                    { new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d"), "Women's Clothing", "Women.jpg", "Women", null },
                    { new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583"), "Men's Clothing", "men.jpg", "Men", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Discount", "Name", "Price", "Rate" },
                values: new object[,]
                {
                    { new Guid("0299eb83-db6c-4947-85bb-dc499f02fbb6"), "Comfortable cotton t-shirt for men", 0m, "Men's T-Shirt", 15.99m, 0m },
                    { new Guid("0570eb39-f4cc-4b64-8a51-2b71e94c48be"), "Cozy sweater for women", 0m, "Women's Sweater", 39.99m, 0m },
                    { new Guid("0709be1e-4756-45d7-a8fa-56ffd486fe46"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("0930e6fd-d028-4587-954b-b16b93c5119b"), "Comfortable shorts for men", 0m, "Men's Shorts", 24.99m, 0m },
                    { new Guid("0d6c74c6-4665-4bf0-b887-2c4207c61b7b"), "Stylish trousers for kids", 0.05m, "Kids' Trousers4", 34.99m, 0m },
                    { new Guid("0dedf62c-b490-4203-a6da-aed756b98f5a"), "Fashionable skirt for women", 0m, "Women's Skirt", 27.99m, 0m },
                    { new Guid("10dbba87-75cd-4905-afac-4241a93a85ab"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("18dce8bb-aebc-48bf-a9e7-b39135cc413a"), "Stylish pants for women", 0.05m, "Women's Pants", 44.99m, 0m },
                    { new Guid("1a9bb502-cfef-4bb1-bad9-4187976be06e"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("1c773eb0-63aa-4326-a16e-d8ed399633f0"), "Casual shorts for kids", 0m, "Kids' Shorts", 15.99m, 0m },
                    { new Guid("1d9fc446-2e26-45c2-89fc-1edd2c02b432"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("2041b362-3161-4834-bef7-653eabaaea76"), "Stylish blouse for women", 0m, "Women's Blouse", 24.99m, 0m },
                    { new Guid("22c16351-59ff-48fd-9595-0bf5ea5d741d"), "Fashionable sandals for women", 0.1m, "Women's Sandals", 29.99m, 0m },
                    { new Guid("278a0f3f-3386-4949-9e71-96ea844586a9"), "Stylish pants for women", 0.05m, "Women's Pants", 44.99m, 0m },
                    { new Guid("27fb5851-80a5-481e-be19-8cd22f0f532f"), "Formal shirt for men", 0.15m, "Men's Shirt", 34.99m, 0m },
                    { new Guid("29660ce9-15dd-474d-b5bb-23bcce8291c4"), "Stylish jacket for kids", 0m, "Kids' Jacket", 39.99m, 0m },
                    { new Guid("2d5e2bc6-74cd-4ec1-8b0a-12f906610584"), "Casual t-shirt for women", 0.2m, "Women's T-Shirt", 19.99m, 0m },
                    { new Guid("2e2349ba-f5c1-4332-bc11-b94e3f733452"), "Warm jacket for kids", 0.1m, "Kids' Jacket", 39.99m, 0m },
                    { new Guid("348a6f57-cf63-4856-919c-d981b5aa2ff6"), "Fashionable skirt for women", 0m, "Women's Skirt", 27.99m, 0m },
                    { new Guid("350c166d-e40a-40ce-b3f5-618e3ab12f75"), "Cozy hoodie for kids", 0m, "Kids' Hoodie", 29.99m, 0m },
                    { new Guid("35c7c4a5-c3f1-462f-bcbf-38aed2ab531e"), "Stylish trousers for kids", 0.05m, "Kids' Trousers3", 34.99m, 0m },
                    { new Guid("3c5d34e4-56f8-488d-8f7e-8715220d177e"), "Stylish jacket for women", 0m, "Women's Jacket", 54.99m, 0m },
                    { new Guid("469c77a0-214c-466e-b230-4ac7033b4202"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("4c74f0a3-2050-49e5-a602-b1a1cc406b84"), "Elegant blazer for women", 0.2m, "Women's Blazer", 59.99m, 0m },
                    { new Guid("4d18bd48-eaa2-408a-9c37-fbd9d38481a1"), "Sporty sneakers for women", 0m, "Women's Sneakers", 49.99m, 0m },
                    { new Guid("4d969d4f-54cc-4303-b3b7-30676f4aa6a3"), "Warm hoodie for men", 0.05m, "Men's Hoodie", 39.99m, 0m },
                    { new Guid("4ef2612f-ea2a-4bf8-974c-fd4340e4b9ac"), "Comfortable sandals for women", 0.2m, "Women's Sandals", 34.99m, 0m },
                    { new Guid("4f2599a8-b3f3-4da9-add8-1ba6592f4687"), "Warm hoodie for men", 0.05m, "Men's Hoodie", 39.99m, 0m },
                    { new Guid("5212b1cb-28de-4318-a27d-8c684950b248"), "Warm jacket for men", 0.2m, "Men's Jacket", 59.99m, 0m },
                    { new Guid("52dc928e-2c90-4b03-941f-fe05679ca377"), "Formal shirt for men", 0.15m, "Men's Shirt", 34.99m, 0m },
                    { new Guid("53245fcf-4380-43e7-9bf0-c58ce1868ee7"), "Fashionable skirt for women", 0m, "Women's Skirt", 27.99m, 0m },
                    { new Guid("53f14730-f488-41c9-9625-0033422ebd45"), "Sporty sneakers for men", 0.1m, "Men's Sneakers", 54.99m, 0m },
                    { new Guid("5e724a47-e645-41e5-837d-7fb7dc7a5916"), "Stylish pants for women", 0.05m, "Women's Pants", 44.99m, 0m },
                    { new Guid("620368aa-41bb-441e-8934-e2ea70be6f15"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("6742fa2b-f6bd-4301-8520-bc1975a462ca"), "Stylish jacket for kids", 0m, "Kids' Jacket", 39.99m, 0m },
                    { new Guid("701ad482-67cd-4573-bb88-1e9970a564b1"), "Stylish jacket for kids", 0m, "Kids' Jacket", 39.99m, 0m },
                    { new Guid("71ccb227-ed9a-4391-b439-762464c019c4"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("7255146d-b4ff-4820-83e1-c36cf35ad5d0"), "Casual shorts for men", 0.1m, "Men's Shorts", 17.99m, 0m },
                    { new Guid("77cc99a5-b091-4fbd-b734-d52ee8e519bf"), "Comfortable cotton t-shirt for men", 0m, "Men's T-Shirt", 15.99m, 0m },
                    { new Guid("79a8d0e0-393f-48b2-9f85-6089ce4ff223"), "Comfortable cotton t-shirt for men", 0m, "Men's T-Shirt", 15.99m, 0m },
                    { new Guid("7b0be0dd-3642-4a3c-9366-6e7af191ec7c"), "Spacious backpack for kids", 0m, "Kids' Backpack", 19.99m, 0m },
                    { new Guid("7e28961a-35f5-4dfc-ad62-21973a666c9b"), "Comfortable hoodie for men", 0m, "Men's Hoodie", 29.99m, 0m },
                    { new Guid("8038c059-8fdd-4db9-919a-c396999f1231"), "Cozy hoodie for kids", 0m, "Kids' Hoodie", 29.99m, 0m },
                    { new Guid("81251da5-afe6-4252-844c-7e1ccbf7a4a8"), "Warm jacket for men", 0.2m, "Men's Jacket", 59.99m, 0m },
                    { new Guid("8543b5af-a8dc-4c3a-9b57-e15eb703c72f"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("87a3231b-a70e-45f6-a71a-116f15908be9"), "Formal shirt for men", 0.15m, "Men's Shirt", 34.99m, 0m },
                    { new Guid("8a1376da-1f96-44a4-92f7-d4dd873730f6"), "Casual trousers for kids", 0.1m, "Kids' Trousers", 21.99m, 0m },
                    { new Guid("8bfdb731-9d49-49ff-b034-2f5ac63d4243"), "Warm sweater for kids", 0m, "Kids' Sweater", 34.99m, 0m },
                    { new Guid("8c9912b5-685d-48d1-b367-faed83fcf3f1"), "Comfortable hoodie for men", 0m, "Men's Hoodie", 29.99m, 0m },
                    { new Guid("8dc416df-9f4d-4933-b85e-93c4fa4176e9"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("921a17aa-0f3f-4864-948d-e4e59cb8bc90"), "Classic polo shirt for men", 0m, "Men's Polo Shirt", 22.99m, 0m },
                    { new Guid("9d2bac39-ccc5-4401-b258-57850e901e2c"), "Warm jacket for men", 0.2m, "Men's Jacket", 59.99m, 0m },
                    { new Guid("9e850923-0ee6-4c9d-af5b-147ec55647a9"), "Cozy hoodie for kids", 0m, "Kids' Hoodie", 29.99m, 0m },
                    { new Guid("a0716f64-206b-484b-800d-82458ee38742"), "Stylish sneakers for men", 0.15m, "Men's Sneakers", 59.99m, 0m },
                    { new Guid("a5d94674-2583-4bc0-a87d-7658c9ecb162"), "Classic pants for men", 0.1m, "Men's Pants", 49.99m, 0m },
                    { new Guid("a695159d-f99c-47c9-9285-f2f4f8ad5555"), "Classic denim jeans for men", 0.05m, "Men's Jeans", 39.99m, 0m },
                    { new Guid("a8db936f-5def-40a6-abb6-a4fab2c4d2ad"), "Warm jacket for men", 0.2m, "Men's Jacket", 59.99m, 0m },
                    { new Guid("aa55c9fa-dc98-4fff-bf6e-f3001f097864"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("b07a7b3b-ff3f-46b7-8358-7cbc861bd43a"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("b217a748-3594-4eb8-9b02-6857a67c8db9"), "Warm hoodie for men", 0.05m, "Men's Hoodie", 39.99m, 0m },
                    { new Guid("b64e17d0-929c-4ae3-bee2-e862ca50d8d8"), "Spacious backpack for kids", 0m, "Kids' Backpack", 19.99m, 0m },
                    { new Guid("b6b566fe-337c-4bed-b284-c646ba9ea4da"), "Stylish trousers for kids", 0.05m, "Kids' Trousers", 34.99m, 0m },
                    { new Guid("b906bba4-7aa7-487c-a446-a6981772b7b4"), "Formal shirt for men", 0.15m, "Men's Shirt", 34.99m, 0m },
                    { new Guid("b9427f2c-372a-4783-8dad-a3b4653dbbc1"), "Adorable shirt for kids", 0.15m, "Kids' Shirt", 17.99m, 0m },
                    { new Guid("bcbed3b7-08cd-4d8c-b38d-64b25ba3116c"), "Warm sweater for men", 0.1m, "Men's Sweater", 39.99m, 0m },
                    { new Guid("c0dce517-1a02-407f-b895-0740fdb4cad0"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("c20bc813-d93c-4192-809a-63b4055ca4f4"), "Cute dress for kids", 0m, "Kids' Dress", 32.99m, 0m },
                    { new Guid("c27c5431-2c5a-4710-b22b-0631e0bf0c47"), "Cozy hoodie for kids", 0m, "Kids' Hoodie", 29.99m, 0m },
                    { new Guid("c505436b-ee14-470a-b337-e091254f505c"), "Warm hoodie for men", 0.05m, "Men's Hoodie", 39.99m, 0m },
                    { new Guid("d3f62614-114f-41d5-9201-309b0e0844e2"), "Comfortable hoodie for men", 0m, "Men's Hoodie", 29.99m, 0m },
                    { new Guid("d51175a4-ab9f-469a-87d2-58c24b4b1c6f"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("d884ed58-96b0-483e-91b8-bcd63de17605"), "Classic pants for men", 0.1m, "Men's Pants", 49.99m, 0m },
                    { new Guid("d93c5ea2-e97d-4228-9704-dfb66c946e1d"), "Comfortable cotton t-shirt for men", 0m, "Men's T-Shirt", 15.99m, 0m },
                    { new Guid("d9682623-8bc3-4ab3-8ca6-4138581101ac"), "Fashionable skirt for women", 0m, "Women's Skirt", 27.99m, 0m },
                    { new Guid("dab09a2f-b2e1-48e7-b1b5-a85334edcb7a"), "Sporty sneakers for men", 0.1m, "Men's Sneakers", 54.99m, 0m },
                    { new Guid("e08fa94a-bf8f-40e6-872b-a9d57cbfbacb"), "Stylish pants for women", 0.05m, "Women's Pants", 44.99m, 0m },
                    { new Guid("e178df7b-3cbf-427d-adb2-053c675b282b"), "Stylish trousers for kids", 0.05m, "Kids' Trousers2", 34.99m, 0m },
                    { new Guid("e5cd2ce2-9bb7-4457-b6ee-a74fa97fba13"), "Warm sweater for kids", 0m, "Kids' Sweater", 34.99m, 0m },
                    { new Guid("e7efd64e-3f68-4124-9b57-136e99143af7"), "Warm hoodie for men", 0.05m, "Men's Hoodie", 39.99m, 0m },
                    { new Guid("e97a5e43-ec38-4131-8245-9b6d989067b0"), "Elegant dress for women", 0.1m, "Women's Dress", 49.99m, 0m },
                    { new Guid("eb4fa6e2-df95-45fb-be6b-e644033645c7"), "Colorful shoes for kids", 0.15m, "Kids' Shoes", 29.99m, 0m },
                    { new Guid("ee82e597-0b57-4a2d-9e56-7f50bcc2ae0f"), "Comfortable hoodie for men", 0m, "Men's Hoodie", 29.99m, 0m },
                    { new Guid("efd57f3d-a938-44c4-9588-73f9cf6d32ec"), "Classic pants for men", 0.1m, "Men's Pants", 49.99m, 0m },
                    { new Guid("f4367324-ceb3-483f-8c3b-558e1f8176a7"), "Comfortable cotton t-shirt for men", 0m, "Men's T-Shirt", 15.99m, 0m },
                    { new Guid("f8f5e586-2367-4d5b-82e4-4862a382da49"), "Adorable t-shirt for kids", 0m, "Kids' T-Shirt", 12.99m, 0m },
                    { new Guid("f9c53125-aa86-4ec6-a1ac-3dc965328f08"), "Classic pants for men", 0.1m, "Men's Pants", 49.99m, 0m },
                    { new Guid("fa086638-4562-45ab-8b28-5566818b4b65"), "Stylish denim jeans for women", 0.05m, "Women's Jeans", 44.99m, 0m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Image", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("1d53debe-03e6-487f-9b34-6b26c68fc1e5"), "Kids Pants's Clothing", "Kids Pants.jpg", "Pants", new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f") },
                    { new Guid("35b303b9-25a0-4379-89b3-64e4ae51291f"), "Women Shoes's Clothing", "Women Shoes.jpg", "Shoes", new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d") },
                    { new Guid("47a38a48-8747-4461-ba32-7e573be663ee"), "Women Jackets's Clothing", "Women Jackets.jpg", "Jackets", new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d") },
                    { new Guid("6b3c4ef5-01ad-49c7-a8ff-36ae55d0ce8d"), "Men Shoes's Clothing", "men Shoes.jpg", "Shoes", new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583") },
                    { new Guid("6f6c6c4c-9e6e-4e0c-97cc-8b52c055918b"), "Men Jackets's Clothing", "men Jackets.jpg", "Jackets", new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583") },
                    { new Guid("8a6d4a19-47cc-4a4e-822b-cac1de2efc8d"), "Kids shirts's Clothing", "Kids shirts.jpg", "Shirts", new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f") },
                    { new Guid("9a938bc1-0717-4b8d-8f8b-3a2f55de49db"), "Men Pants's Clothing", "men Pants.jpg", "Pants", new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583") },
                    { new Guid("a6c4de53-33c5-48e1-9f21-5649726d3a3d"), "Women shirts's Clothing", "Women shirts.jpg", "Shirts", new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d") },
                    { new Guid("a6d7e8b5-2f4d-4f51-b24b-4fcb52e36f5f"), "Men Hoodies's Clothing", "men Hoodies.jpg", "Hoodies", new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583") },
                    { new Guid("b19a53a3-04e7-4804-84bc-84da64d738a6"), "Kids Jackets's Clothing", "Kids Jackets.jpg", "Jackets", new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f") },
                    { new Guid("c2ae51c9-913a-4e7d-a7b5-ef1efc8f9d3e"), "Kids Hoodies's Clothing", "Kids Hoodies.jpg", "Hoodies", new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f") },
                    { new Guid("ca09f6a1-5b87-4b56-9ee3-c6fb6ad070c2"), "Kids Shoes's Clothing", "Kids Shoes.jpg", "Shoes", new Guid("52d40b0a-7039-4bc6-899d-0c36adff6b8f") },
                    { new Guid("d9f02e92-d14c-4b6d-86ad-6e4e6c48020a"), "Women Pants's Clothing", "Women Pants.jpg", "Pants", new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d") },
                    { new Guid("e18e42b7-799e-4b3b-a084-c55d4bb5da3f"), "Women Hoodies's Clothing", "Women Hoodies.jpg", "Hoodies", new Guid("a6c4de53-33c5-48e1-9f21-5649726d2a3d") },
                    { new Guid("f032f788-2340-431f-9f8f-eeb176a35177"), "Mens shirts's Clothing", "men shirts.jpg", "Shirts", new Guid("edc6b9e0-9252-4e9d-b4d3-9203b6de2583") }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ArrivalDate", "City", "Country", "CustomerId", "Discount", "OrderData", "OrderStatus", "PaymentMethod", "PaymentStatus", "Street", "TotalPrice" },
                values: new object[,]
                {
                    { new Guid("07d96ed8-155d-49c7-a77a-615f109d75c3"), new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", "Zimbabwe", "b6a76b15-33e5-4d26-98b9-c948c7823b84", 1.0, new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CashOnDelivery", "Paid", "789 Oak St", 0m },
                    { new Guid("07d96ed8-155d-49c7-a77a-615f109d77c3"), new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", "Zimbabwe", "e23edc32-bd6a-4b6b-a28e-ccf60b5c32dc", 1.0, new DateTime(2026, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CashOnDelivery", "Paid", "789 Oak St", 0m },
                    { new Guid("0e67a2e5-df53-4a92-9854-8e1ad46a4e61"), new DateTime(2027, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "New York", "Belgium", "0e67a2e5-df53-4a92-9854-8e1ad46a4e61", 0.0, new DateTime(2027, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CreditCard", "Unpaid", "123 Elm St", 0m },
                    { new Guid("22ac8dc9-4385-48ae-90a3-7d8c898c6d5d"), new DateTime(2027, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles", "Belize", "74f5b2b3-3d10-4a85-93b5-8c6d0c992b58", 0.5, new DateTime(2027, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", "CashOnDelivery", "Paid", "456 Main St", 0m },
                    { new Guid("23456789-01ab-cdef-0123-456789abcdef"), new DateTime(2027, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Francisco", "Oman", "234cdf89-12a3-45b6-789c-0123456789de", 0.10000000000000001, new DateTime(2027, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CreditCard", "Paid", "321 Maple Ave", 0m },
                    { new Guid("2345cdef-0123-4567-89ab-cdef11234567"), new DateTime(2027, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seattle", "Taiwan", "6789abcd-ef01-2345-6789-abcd01234567", 0.20000000000000001, new DateTime(2027, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CreditCard", "Paid", "567 Pine St", 0m },
                    { new Guid("6789abcd-ef01-2345-6789-abcd01234567"), new DateTime(2029, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seattle", "Libya", "c7d3e80a-7a4a-4c54-91a6-89c0df051c94", 0.20000000000000001, new DateTime(2029, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CreditCard", "Paid", "789 Elm St", 0m },
                    { new Guid("724587e6-9314-4fe6-9c3e-7fd612f50232"), new DateTime(2029, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Francisco", "Senegal", "def01234-5678-9abc-def0-123456789abc", 0.29999999999999999, new DateTime(2029, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CreditCard", "Paid", "123 Maple Ave", 0m },
                    { new Guid("724587e6-9314-4fe6-9c3e-7fd612f50233"), new DateTime(2029, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seattle", "Samoa", "c7d3e80a-7a4a-4c54-91a6-89c0df051c94", 0.20000000000000001, new DateTime(2029, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CashOnDelivery", "Paid", "789 Pine St", 0m },
                    { new Guid("724587e6-9314-4fe6-9c3e-7fd612f50234"), new DateTime(2029, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dallas", "Samoa", "b6a76b15-33e5-4d26-98b9-c948c7823b84", 0.10000000000000001, new DateTime(2029, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CashOnDelivery", "Paid", "987 Cedar St", 0m },
                    { new Guid("74f5b2b3-3d10-4a85-93b5-8c6d0c992b58"), new DateTime(2029, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Diego", "Samoa", "bcdef012-3456-789a-bcde-f01234567890", 0.0, new DateTime(2029, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CashOnDelivery", "Unpaid", "456 Oak St", 0m },
                    { new Guid("8901def0-1234-5678-9abc-def012345678"), new DateTime(2029, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "San Francisco", "Afghanistan", "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb7", 0.0, new DateTime(2029, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CashOnDelivery", "Unpaid", "123 Pine St", 0m },
                    { new Guid("b6a76b15-33e5-4d26-98b9-c948c7823b84"), new DateTime(2029, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Los Angeles", "Andorra", "74f5b2b3-3d10-4a85-93b5-8c6d0c992bb8", 0.10000000000000001, new DateTime(2029, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CashOnDelivery", "Paid", "456 Maple Ave", 0m },
                    { new Guid("c7d3e80a-7a4a-4c54-91a6-89c0df051c94"), new DateTime(2029, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", "Iraq", "07d96ed8-155d-49c7-a77a-615f109d77c3", 0.10000000000000001, new DateTime(2029, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CashOnDelivery", "Paid", "567 Oak St", 0m },
                    { new Guid("def01234-5678-9abc-def0-113456789abc"), new DateTime(2028, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miami", "Fiji", "bcdef012-3456-789a-bcde-f01234567890", 0.29999999999999999, new DateTime(2028, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CreditCard", "Paid", "901 Cherry Ln", 0m },
                    { new Guid("e23edc32-bd6a-4b6b-a28e-ccf90b5c32dc"), new DateTime(2028, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Boston", "Denmark", "2345cdef-0123-4567-89ab-cdef01234567", 0.14999999999999999, new DateTime(2028, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", "CashOnDelivery", "Paid", "246 Elm St", 0m },
                    { new Guid("f0e7f09e-c7ad-4cb0-8f19-6540b4c7c49f"), new DateTime(2029, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", "Canada", "724587e6-9314-4fe6-9c3e-6fd612f50234", 0.20000000000000001, new DateTime(2028, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CreditCard", "Unpaid", "789 Elm St", 0m },
                    { new Guid("f0e7f09e-c7ad-4cb0-8f19-6540b5c7c49f"), new DateTime(2029, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", "Canada", "8901def0-1234-5678-9abc-def012345678", 0.20000000000000001, new DateTime(2029, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "CreditCard", "Unpaid", "789 Elm St", 0m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageURL", "ProductID" },
                values: new object[,]
                {
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("0299eb83-db6c-4947-85bb-dc499f02fbb6") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("0709be1e-4756-45d7-a8fa-56ffd486fe46") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("1d9fc446-2e26-45c2-89fc-1edd2c02b432") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("469c77a0-214c-466e-b230-4ac7033b4202") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("71ccb227-ed9a-4391-b439-762464c019c4") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("79a8d0e0-393f-48b2-9f85-6089ce4ff223") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("8dc416df-9f4d-4933-b85e-93c4fa4176e9") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("aa55c9fa-dc98-4fff-bf6e-f3001f097864") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("d93c5ea2-e97d-4228-9704-dfb66c946e1d") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("e97a5e43-ec38-4131-8245-9b6d989067b0") },
                    { "https://townteam.com/cdn/shop/files/SSH23SAER19684TM1-Multicolor-3_600x.jpg?v=1684071642", new Guid("f4367324-ceb3-483f-8c3b-558e1f8176a7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartID",
                table: "AspNetUsers",
                column: "CartID",
                unique: true,
                filter: "[CartID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishListID",
                table: "AspNetUsers",
                column: "WishListID",
                unique: true,
                filter: "[WishListID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_CartId",
                table: "CartProduct",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersReviews_CustomerId",
                table: "CustomersReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishList_WishListsId",
                table: "ProductWishList",
                column: "WishListsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "CustomersReviews");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductsInfo");

            migrationBuilder.DropTable(
                name: "ProductWishList");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "WishLists");
        }
    }
}
