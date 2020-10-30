using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private RestaurantContext db;
        
        public IngredientRepository(RestaurantContext context)
        {
            this.db = context;
        }
        
        public IEnumerable<Ingredient> GetAll()
        {
            return db.Ingredient;
        }

        public Ingredient Get(int id)
        {
            return db.Ingredient.Find(id);
        }

        public IEnumerable<Ingredient> Find(Func<Ingredient, bool> predicate)
        {
            return db.Ingredient.Where(predicate).ToList();
        }

        public void Create(Ingredient item)
        {
            db.Ingredient.Add(item);
        }

        public void Update(Ingredient item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Ingredient dish = db.Ingredient.Find(id);
            if (dish != null)
                db.Ingredient.Remove(dish);
        }
    }
}