using Bookstore.Data;
using Bookstore.Data.Seeders;
using Bookstore.Helpers;
using Bookstore.Services.Interfaces;
using Bookstore.Services;
using Bookstore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //connect to DB
        var connectionString = configuration.GetConnectionString("DefaultConnection"); 
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        
        //Service registrations
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<DBSeeder>();
        services.AddScoped<JWT>();
        return services;
    }
}