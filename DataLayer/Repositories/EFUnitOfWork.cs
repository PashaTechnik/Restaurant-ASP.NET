using System;

namespace DataLayer
{
    public class EFUnitOfWork : IUnitOfWork
    {
        
        private RestaurantContext db;
        
        private bool disposed = false;
        
        private DishRepository dishRepository;
        private OrdersRepository ordersRepository;
        private DishdetailsRepository dishdetailsRepository;
        private IngredientRepository ingredientRepository;
        private MenuRepository menuRepository;
        private OrderdetailsRepository orderdetailsRepository;
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public IRepository<Dish> Dishs
        {
            get{
                if (dishRepository == null)
                    dishRepository = new DishRepository(db);
                return dishRepository;
            }  
        }
        public IRepository<Dishdetails> Dishdetails {             
            get {
                if (dishdetailsRepository == null)
                    dishdetailsRepository = new DishdetailsRepository(db);
                return dishdetailsRepository;
            }   
        }
        public IRepository<Ingredient> Ingredients {             
            get {
                if (ingredientRepository == null)
                    ingredientRepository = new IngredientRepository(db);
                return ingredientRepository;
            }   
        }
        public IRepository<Menu> Menu {             
            get {
                if (menuRepository == null)
                    menuRepository = new MenuRepository(db);
                return menuRepository;
            }   
        }
        public IRepository<Orders> Orders {             
            get {
                if (ordersRepository == null)
                    ordersRepository = new OrdersRepository(db);
                return ordersRepository;
            }   
        }
        public IRepository<Orderdetails> Orderdetails {             
            get {
                if (orderdetailsRepository == null)
                    orderdetailsRepository = new OrderdetailsRepository(db);
                return orderdetailsRepository;
            }   
        }
        
        public void Save()
        {
            db.SaveChanges();
        }
    }
}