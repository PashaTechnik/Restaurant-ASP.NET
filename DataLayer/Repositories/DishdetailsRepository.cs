using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DishdetailsRepository : IRepository<Dishdetails>
    {
        private RestaurantContext db;

        public DishdetailsRepository(RestaurantContext context)
        {
            this.db = context;
        }

        public IEnumerable<Dishdetails> GetAll()
        {
            return db.Dishdetails;
        }

        public Dishdetails Get(int id)
        {
            return db.Dishdetails.Find(id);
        }

        public IEnumerable<Dishdetails> Find(Func<Dishdetails, bool> predicate)
        {
            return db.Dishdetails.Where(predicate).ToList();
        }

        public void Create(Dishdetails item)
        {
            db.Dishdetails.Add(item);
        }

        public void Update(Dishdetails item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dishdetails dish = db.Dishdetails.Find(id);
            if (dish != null)
                db.Dishdetails.Remove(dish);
        }
    }
}