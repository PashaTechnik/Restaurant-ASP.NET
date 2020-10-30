using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DishRepository : IRepository<Dish>
    {

        private RestaurantContext db;
        
        public DishRepository(RestaurantContext context)
        {
            this.db = context;
        }
        
        public IEnumerable<Dish> GetAll()
        {
            return db.Dish;
        }

        public Dish Get(int id)
        {
            return db.Dish.Find(id);
        }

        public IEnumerable<Dish> Find(Func<Dish, bool> predicate)
        {
            return db.Dish.Where(predicate).ToList();
        }

        public void Create(Dish item)
        {
            db.Dish.Add(item);
        }

        public void Update(Dish item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dish dish = db.Dish.Find(id);
            if (dish != null)
                db.Dish.Remove(dish);
        }
    }
}