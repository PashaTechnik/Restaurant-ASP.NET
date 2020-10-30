using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MenuRepository : IRepository<Menu>
    {
        private RestaurantContext db;
        
        public MenuRepository(RestaurantContext context)
        {
            this.db = context;
        }
        
        public IEnumerable<Menu> GetAll()
        {
            return db.Menu;
        }

        public Menu Get(int id)
        {
            return db.Menu.Find(id);
        }

        public IEnumerable<Menu> Find(Func<Menu, bool> predicate)
        {
            return db.Menu.Where(predicate).ToList();
        }

        public void Create(Menu item)
        {
            db.Menu.Add(item);
        }

        public void Update(Menu item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Menu dish = db.Menu.Find(id);
            if (dish != null)
                db.Menu.Remove(dish);
        }
    }
}