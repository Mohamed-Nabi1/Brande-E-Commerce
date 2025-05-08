using E_Commerce.BL;
using E_Commerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Default

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddControllers().AddNewtonsoftJson();
#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSpolicy", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

#endregion

#region DataBase

builder.Services.AddDbContext<E_CommerceContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("E_CommerceDB")));

#endregion

#region InjectionForRepo
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<ICustomerReviewRepo, CustomerReviewRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
builder.Services.AddScoped<IUsersRepo, UsersRepo>();
builder.Services.AddScoped<IWishListRepo, WishListRepo>();
builder.Services.AddScoped<ICartProductRepo, CartsProductRepo>();
builder.Services.AddScoped<IUsersRepo, UsersRepo>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region InjectionForMangers
builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();

builder.Services.AddScoped<ICustomerReviewManager, CustomerReviewManager>();
builder.Services.AddScoped<IWishListManager, WishListManager>();

builder.Services.AddScoped<IOrderManager, OrderManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();

builder.Services.AddScoped<IUsersManagers, UsersManager>();
builder.Services.AddScoped<ICartManager, CartManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<ISecurityManager, SecurityManager>();
builder.Services.AddScoped<IFilterManger,FilterManger>();
#endregion

#region Identity

builder.Services.AddIdentity<Customer, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;

    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<E_CommerceContext>();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Def";
    options.DefaultChallengeScheme = "Def";
}).AddJwtBearer("Def", options =>
{
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey")));
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
       IssuerSigningKey = key
    };
});
#endregion

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ForAdmin", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Admin");
    });

    options.AddPolicy("ForCustomer", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Customer");
    });
});

#endregion

var app = builder.Build();
var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "Images");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/Images" //Localhost:5073/(Request Path)/Capture.PNG(Static File Name)

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSpolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();