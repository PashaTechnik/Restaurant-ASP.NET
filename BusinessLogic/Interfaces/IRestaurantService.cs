using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IRestaurantService
    {
        void MakeOrder(BusinessLogic.Orders orderDto);
        void EditMenu(BusinessLogic.Menu orderDto);
        void EditDish(BusinessLogic.Dish dishDto);
        void EditDetails(BusinessLogic.Dishdetails detailsDto);
        void MakeOrderDetails(BusinessLogic.Orderdetails orderDetailsDto);
        IEnumerable<Orders> GetOrder();
        IEnumerable<Menu> GetMenu();
        IEnumerable<Ingredient> GetIngredients();
        IEnumerable<Dish> GetDish();
        IEnumerable<Dishdetails> GetDetails();
        void Dispose();
    }
}