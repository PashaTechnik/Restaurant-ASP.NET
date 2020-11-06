using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using AutoMapper;
using BusinessLogic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {

         private IOrderService orderService;
         private IMenuEditor menuEditor;
         private IDishCreator dishCreator;
         public HomeController(IOrderService serv, IMenuEditor edit, IDishCreator create)
         {
             orderService = serv;
             menuEditor = edit;
             dishCreator = create;
         }
         public ActionResult Index()
         {
             return View();
         }
  
         public ActionResult MakeOrder(int? id)
         {
             try
             {
                 BusinessLogic.Menu dish = orderService.GetDish(id);
                 var order = new OrderViewModel { Orderid = dish.Positionid };
                  
                 return View(order);
             }
             catch (ValidationException ex)
             {
                 return Content(ex.Message);
             }
         }
         [HttpPost]
         public ActionResult MakeOrder(OrderViewModel order)
         {
             try
             {
                 var orderDto = new BusinessLogic.Orders
                 {
                     Orderid = order.Orderid,
                     Clientname = order.Clientname,
                     Price = order.Price 
                 };
                 orderService.MakeOrder(orderDto);
                 return Content("<h2>Ваш заказ успешно оформлен</h2>");
             }
             catch (ValidationException ex)
             {
                 ModelState.AddModelError(ex.Property, ex.Message);
             }
             return View(order);
         }

         public ViewResult EditMenu()
         {
             IEnumerable<BusinessLogic.Menu> menu = menuEditor.GetMenu();
             IEnumerable<BusinessLogic.Dish> dish = menuEditor.GetDish();
             
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
             var menuDto = new DataLayer.Menu
             {
                 Dishid = int.Parse(id),
                 Size = int.Parse(size),
                 Price = int.Parse(price)
             };
             menuEditor.EditMenu(menuDto);

             return new RedirectToPageResult("/");
             
         }
         
         public ViewResult CreateDish()
         {
             IEnumerable<BusinessLogic.Ingredient> ingredient = dishCreator.GetIngredients();
             IEnumerable<BusinessLogic.Dish> dish = dishCreator.GetDish();
             
             var mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Ingredient, IngredientViewModel>()).CreateMapper();
             var ingredients = mapper1.Map<IEnumerable<BusinessLogic.Ingredient>, List<IngredientViewModel>>(ingredient);
             
             var mapper2 = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Dish, DishViewModel>()).CreateMapper();
             var dishs = mapper2.Map<IEnumerable<BusinessLogic.Dish>, List<DishViewModel>>(dish);
             
             dynamic mymodel = new ExpandoObject();
             mymodel.Ingredients = ingredients;
             mymodel.Dishs = dishs;
             
             
             return View(mymodel);
         }
         
         

         protected override void Dispose(bool disposing)
         {
             orderService.Dispose();
             menuEditor.Dispose();
             dishCreator.Dispose();
             base.Dispose(disposing);
         }
    }
}