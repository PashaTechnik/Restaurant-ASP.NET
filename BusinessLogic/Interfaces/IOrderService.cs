using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IOrderService
    {
        void MakeOrder(BusinessLogic.Orders orderDto);
        void MakeOrderDetails(BusinessLogic.Orderdetails orderDetailsDto);
        Menu GetDish(int? id);
        IEnumerable<Menu> GetDish();
        IEnumerable<Dish> GetDishName();
        IEnumerable<Orders> GetOrder();
        void Dispose();
    }
}