using System;

namespace DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Dish> Dishs { get;}
        IRepository<Dishdetails> Dishdetails { get;}
        IRepository<Ingredient> Ingredients { get;}
        IRepository<Menu> Menu { get;}
        IRepository<Orders> Orders { get;}
        IRepository<Orderdetails> Orderdetails { get;}
        void Save();
    }
}