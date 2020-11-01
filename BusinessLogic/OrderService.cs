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

        public void MakeOrder(DataLayer.Orders order)
        {
            DataLayer.Menu dish = Database.Menu.Get(order.Orderid);
            
            if (dish == null)
                throw new ValidationException("Блюдо не найдено","");


            Database.Orders.Create(order);
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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}