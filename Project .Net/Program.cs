using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DataLayer;
using Microsoft.AspNetCore;
using Dish = DataLayer.Dish;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            // IUnitOfWork uow = new EFUnitOfWork();
            //
            // RestaurantContext db = new RestaurantContext();
            // MenuRepository menu = new MenuRepository(db);
            //
            // var allMenu = menu.GetAll();
            // foreach (var u in allMenu)
            // {
            //     Console.WriteLine($"{u.Positionid}-{u.Dishid}");
            // }

            // RestaurantContext db = new RestaurantContext();
            // Dish dish = new Dish();
            // dish.Name = "dish6";
            //
            // db.Dish.Add(dish);
            // db.SaveChanges();
            //
            // var dishes = db.Dish.ToList();
            //
            // Console.WriteLine("Dish list:");
            // foreach (Dish u in dishes)
            // {
            //     Console.WriteLine($"{u.Dishid}.{u.Name}");
            // }

            // IOrderService orderService = new OrderService();
            // BusinessLogic.Orders order = new Orders();
            // order.Clientname = "Pasha";
            // order.Price = 1020;
            //
            // try
            // {
            //     orderService.MakeOrder(order);
            // }
            // catch (ValidationException ex)
            // {
            //     Console.WriteLine("Error add item");
            // }


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            var enviroment = config["environment"] ?? "Development";
            
            return WebHost.CreateDefaultBuilder(args)
                .UseEnvironment(enviroment)
                .UseStartup<Startup>()
                .Build();
        }
    }
}