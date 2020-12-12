using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AutoMapper;
using BusinessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {

         // private IOrderService orderService;
         // private IMenuEditor menuEditor;
         // private IDishCreator dishCreator;
         
         private IRestaurantService restaurantService;
         public HomeController(IRestaurantService service)
         {
             restaurantService = service;
         }
         
         
         
         public ActionResult Index()
         {
             return View();
         }
         
         public ViewResult MakeOrder()
         {
             IEnumerable<BusinessLogic.Orders> order = restaurantService.GetOrder();
             IEnumerable<BusinessLogic.Menu> menu = restaurantService.GetMenu();
             IEnumerable<BusinessLogic.Dish> dish = restaurantService.GetDish();
             
             
             
             var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Menu, MenuViewModel>()).CreateMapper();
             var menus = mapper1.Map<IEnumerable<BusinessLogic.Menu>, List<MenuViewModel>>(menu);
             
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Orders, OrderViewModel>()).CreateMapper();
             var orders = mapper2.Map<IEnumerable<BusinessLogic.Orders>, List<OrderViewModel>>(order);
             
             var mapper3 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Dish, DishViewModel>()).CreateMapper();
             var dishs = mapper3.Map<IEnumerable<BusinessLogic.Dish>, List<DishViewModel>>(dish);
             
             dynamic mymodel = new ExpandoObject();
             mymodel.Orders = orders;
             mymodel.Menu = menus;
             
             foreach (var i in menus)
             {
                 foreach (var k in dishs)
                 {
                     if (i.Dishid == k.Dishid)
                     {
                         i.DishName = k.Name;
                     }
                 }
             }
             
             return View(mymodel);
         }
         
         [BindProperty]
         public List<int> AreCheckedDish { get; set; }
         
         [HttpPost]
         public ActionResult MakeOrder(string name, int quantity)
         {
             IEnumerable<BusinessLogic.Menu> menu = restaurantService.GetMenu();
             var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Menu, MenuViewModel>()).CreateMapper();
             var menus = mapper1.Map<IEnumerable<BusinessLogic.Menu>, List<MenuViewModel>>(menu);
             IEnumerable<BusinessLogic.Orders> order = restaurantService.GetOrder();
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Orders, OrderViewModel>()).CreateMapper();
             var orders = mapper2.Map<IEnumerable<BusinessLogic.Orders>, List<OrderViewModel>>(order);

             var last = orders.LastOrDefault().Orderid;
             int price = 0;
             

             var IDs = AreCheckedDish.ToArray();
             var quantities = Points.ToArray();

             for (int i = 0; i < IDs.Length; i++)
             {
                 var detailsDto = new BusinessLogic.Orderdetails
                 {
                     Orderid = last + 1,
                     Quantity = quantities[i],
                     Positionid = IDs[i]
                 };
                 restaurantService.MakeOrderDetails(detailsDto);
                 foreach (var item in menus)
                 {
                     if (IDs[i] == item.Positionid)
                     {
                         price += quantities[i] * (item.Price ?? 0);
                     }
                 }

             }
             var orderDto = new BusinessLogic.Orders
             {
                 Clientname = name,
                 Price = price
             };
             restaurantService.MakeOrder(orderDto);

             return new RedirectToPageResult("/");

         }

         public ViewResult EditMenu()
         {
             IEnumerable<BusinessLogic.Menu> menu = restaurantService.GetMenu();
             IEnumerable<BusinessLogic.Dish> dish = restaurantService.GetDish();
             
             var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Menu, MenuViewModel>()).CreateMapper();
             var menus = mapper1.Map<IEnumerable<BusinessLogic.Menu>, List<MenuViewModel>>(menu);
             
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Dish, DishViewModel>()).CreateMapper();
             var dishs = mapper2.Map<IEnumerable<BusinessLogic.Dish>, List<DishViewModel>>(dish);
             
             dynamic mymodel = new ExpandoObject();
             mymodel.Menu = menus;
             mymodel.Dishs = dishs;

             foreach (var i in menus)
             {
                 foreach (var k in dishs)
                 {
                     if (i.Dishid == k.Dishid)
                     {
                         i.DishName = k.Name;
                     }
                 }
             }
             
             return View(mymodel);
         }
         [HttpPost]
         public IActionResult EditMenu(string id, string price, string size)
         {
             var menuDto = new BusinessLogic.Menu
             {
                 Dishid = int.Parse(id),
                 Size = int.Parse(size),
                 Price = int.Parse(price)
             };
             restaurantService.EditMenu(menuDto);

             return new RedirectToPageResult("/");
             
         }
         
         public ViewResult CreateDish()
         {
             IEnumerable<BusinessLogic.Ingredient> ingredient = restaurantService.GetIngredients();
             IEnumerable<BusinessLogic.Dish> dish = restaurantService.GetDish();
             
             var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Ingredient, IngredientViewModel>()).CreateMapper();
             var ingredients = mapper1.Map<IEnumerable<BusinessLogic.Ingredient>, List<IngredientViewModel>>(ingredient);
             
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Dish, DishViewModel>()).CreateMapper();
             var dishs = mapper2.Map<IEnumerable<BusinessLogic.Dish>, List<DishViewModel>>(dish);
             
             dynamic mymodel = new ExpandoObject();
             mymodel.Ingredients = ingredients;
             mymodel.Dishs = dishs;
             
             
             return View(mymodel);
         }
         
         [BindProperty]
         public List<int> AreChecked { get; set; }

         [BindProperty] 
         public List<int> Points { get; set; }

         [HttpPost]
         public IActionResult CreateDish(string name)
         {
             IEnumerable<BusinessLogic.Dish> dish = restaurantService.GetDish();
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Dish, DishViewModel>()).CreateMapper();
             var dishs = mapper2.Map<IEnumerable<BusinessLogic.Dish>, List<DishViewModel>>(dish);
             var last = dishs.LastOrDefault().Dishid;
             
             var dishDto = new BusinessLogic.Dish
             {
                 Name = name,
             };
             restaurantService.EditDish(dishDto);

             var IDs = AreChecked.ToArray();

             for (int i = 0; i < IDs.Length; i++)
             {
                 var detailsDto = new BusinessLogic.Dishdetails
                 {
                     Dishid = last + 1,
                     Ingredientid = IDs[i],
                     
                 };
                 restaurantService.EditDetails(detailsDto);
             }

             return new RedirectToPageResult("/");
             
         }

         protected override void Dispose(bool disposing)
         {
             restaurantService.Dispose();
             base.Dispose(disposing);
         }
    }
}