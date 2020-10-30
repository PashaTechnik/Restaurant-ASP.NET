using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class OrderdetailsRepository : IRepository<Orderdetails>
    {
        private RestaurantContext db;

        public OrderdetailsRepository(RestaurantContext context)
        {
            this.db = context;
        }

        public IEnumerable<Orderdetails> GetAll()
        {
            return db.Orderdetails;
        }

        public Orderdetails Get(int id)
        {
            return db.Orderdetails.Find(id);
        }

        public IEnumerable<Orderdetails> Find(Func<Orderdetails, bool> predicate)
        {
            return db.Orderdetails.Where(predicate).ToList();
        }

        public void Create(Orderdetails item)
        {
            db.Orderdetails.Add(item);
        }

        public void Update(Orderdetails item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Orderdetails dish = db.Orderdetails.Find(id);
            if (dish != null)
                db.Orderdetails.Remove(dish);
        }
    }
}