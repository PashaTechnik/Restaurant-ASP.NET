using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace DataLayer
{
    public class OrdersRepository : IRepository<Orders>
    {
        private RestaurantContext db;
        
        public OrdersRepository(RestaurantContext context)
        {
            this.db = context;
        }
        
        public IEnumerable<Orders> GetAll()
        {
            return db.Orders;
        }

        public Orders Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Orders> Find(Func<Orders, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public void Create(Orders item)
        {
            db.Orders.Add(item);
        }

        public void Update(Orders item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Orders dish = db.Orders.Find(id);
            if (dish != null)
                db.Orders.Remove(dish);
        }
    }
}