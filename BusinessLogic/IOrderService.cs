using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IOrderService
    {
        void MakeOrder(BusinessLogic.Orders orderDto);
        Menu GetDish(int? id);
        IEnumerable<Menu> GetDish();
        void Dispose();
    }
}