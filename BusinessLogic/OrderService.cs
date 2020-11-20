using System;
using System.Collections.Generic;
using AutoMapper;
using DataLayer;

namespace BusinessLogic
{
    public class OrderService : IOrderService
    {
        
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
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


        public Menu GetDish(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id блюда","");
            var dish = Database.Menu.Get(id.Value);
            if (dish == null)
                throw new ValidationException("Блюдо не найден","");
             
            return new Menu
            {
                Positionid = dish.Positionid,
                Dishid = dish.Dishid,
                Size = dish.Size,
                Price = dish.Price
            };
        }

        public IEnumerable<Menu> GetDish()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Menu, Menu>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Menu>, List<Menu>>(Database.Menu.GetAll());
        }

        public IEnumerable<Dish> GetDishName()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Dish, Dish>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dish>, List<Dish>>(Database.Dishs.GetAll());
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