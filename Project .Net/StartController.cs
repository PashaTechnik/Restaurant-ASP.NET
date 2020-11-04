using System.Collections.Generic;
using AutoMapper;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
//
// namespace PresentationLayer
// {
//     public class HomeController : Controller
//     {
//         IOrderService orderService;
//         public HomeController(IOrderService serv)
//         {
//             orderService = serv;
//         }
//         public ActionResult Index()
//         {
//             IEnumerable<BusinessLogic.Menu> dish = orderService.GetDish();
//             var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BusinessLogic.Menu, MenuViewModel>()).CreateMapper();
//             var dishs = mapper.Map<IEnumerable<BusinessLogic.Menu>, List<MenuViewModel>>(dish);
//             return View(dishs);
//         }
//  
//         public ActionResult MakeOrder(int? id)
//         {
//             try
//             {
//                 BusinessLogic.Menu dish = orderService.GetDish(id);
//                 var order = new OrderViewModel { Orderid = dish.Positionid };
//                  
//                 return View(order);
//             }
//             catch (ValidationException ex)
//             {
//                 return Content(ex.Message);
//             }
//         }
//         [HttpPost]
//         public ActionResult MakeOrder(OrderViewModel order)
//         {
//             try
//             {
//                 var orderDto = new BusinessLogic.Orders
//                 {
//                     Orderid = order.Orderid,
//                     Clientname = order.Clientname,
//                     Price = order.Price 
//                 };
//                 orderService.MakeOrder(orderDto);
//                 return Content("<h2>Ваш заказ успешно оформлен</h2>");
//             }
//             catch (ValidationException ex)
//             {
//                 ModelState.AddModelError(ex.Property, ex.Message);
//             }
//             return View(order);
//         }
//         protected override void Dispose(bool disposing)
//         {
//             orderService.Dispose();
//             base.Dispose(disposing);
//         }
//     }
// }