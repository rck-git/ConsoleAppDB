using ConsoleAppD.Services;
using DataAccess.Contexts;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ConsoleAppD;

internal class Program
{
    static void Main(string[] args)
    {


        var app = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\r\Downloads\db\ConsoleAppD\DataAccess\Data\database.mdf;Integrated Security=True"));

            services.AddScoped<AdressRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<RoleRepository>();

            services.AddScoped<AdressService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<ProductService>();
            services.AddScoped<RoleService>();

            services.AddScoped<MenuService>();



        }).Build();
        app.Start();
        Console.Clear();

        var menuservice = app.Services.GetRequiredService<MenuService>();
        menuservice.ShowMenu();

    }
}

