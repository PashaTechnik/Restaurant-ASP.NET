
using System.Collections.Generic;
using AutoMapper;
using DataLayer;

namespace BusinessLogic
{
    public class RestaurantService : IRestaurantService
    {
        
        IUnitOfWork Database { get; set; }
        
        public RestaurantService(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public void EditDish(Dish dishDto)
        {
            DataLayer.Dish dish = new DataLayer.Dish
            {
                Name = dishDto.Name,
            };
            
            Database.Dishs.Create(dish);
            Database.Save();
        }

        public void EditDetails(Dishdetails detailsDto)
        {
            DataLayer.Dishdetails dishDetail = new DataLayer.Dishdetails
            {
                Dishid = detailsDto.Dishid,
                Ingredientid = detailsDto.Ingredientid
            };
            
            Database.Dishdetails.Create(dishDetail);
            Database.Save();
        }
        


        public void EditMenu(Menu menuDto)
        {
            DataLayer.Menu menu = new DataLayer.Menu
            {
                Positionid = menuDto.Positionid,
                Dishid = menuDto.Dishid,
                Size = menuDto.Size,
                Price = menuDto.Price
            };
            
            Database.Menu.Create(menu);
            Database.Save();
        }

        public void MakeOrder(BusinessLogic.Orders order)
        {
            
            DataLayer.Orders orders = new DataLayer.Orders
            {
                Orderid = order.Orderid,
                Clientname = order.Clientname,
                Price = order.Price
            };

            Database.Orders.Create(orders);
            Database.Save();
        }

        public void MakeOrderDetails(Orderdetails orderDetailsDto)
        {
            DataLayer.Orderdetails orderDetails = new DataLayer.Orderdetails
            {
                Orderid = orderDetailsDto.Orderid,
                Positionid = orderDetailsDto.Positionid,
                Quantity = orderDetailsDto.Quantity
            };

            Database.Orderdetails.Create(orderDetails);
            Database.Save();
        }
        
        public IEnumerable<Menu> GetMenu()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Menu, Menu>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Menu>, List<Menu>>(Database.Menu.GetAll());
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Ingredient, Ingredient>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Ingredient>, List<Ingredient>>(Database.Ingredients.GetAll());
        }

        public IEnumerable<Dish> GetDish()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Dish, Dish>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dish>, List<Dish>>(Database.Dishs.GetAll());
        }

        public IEnumerable<Dishdetails> GetDetails()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Dishdetails, Dishdetails>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dishdetails>, List<Dishdetails>>(Database.Dishdetails.GetAll());
        }
        
        public IEnumerable<Orders> GetOrder()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Orders, Orders>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Orders>, List<Orders>>(Database.Orders.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}