using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using HW6.Models;
using HW6.DAL.Abstract;
using HW6.DAL.Concrete;
using HW6.Services;

namespace HW6;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //Add our dbcontext to the Dependency Injection container
        builder.Services.AddDbContext<CoffeeShopDbContext>(
            options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopConnection")));   
            //.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));   

        builder.Services.AddScoped<DbContext,CoffeeShopDbContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        builder.Services.AddScoped<IOrderedItemRepository, OrderedItemRepository>();
        builder.Services.AddScoped<IStationRepository, StationRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
