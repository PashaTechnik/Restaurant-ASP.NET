using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IDishCreator
    {
        void EditDish(BusinessLogic.Dish dishDto);
        void EditDetails(BusinessLogic.Dishdetails detailsDto);
        IEnumerable<Ingredient> GetIngredients();
        IEnumerable<Dish> GetDish();
        IEnumerable<Dishdetails> GetDetails();
        void Dispose();
    }
}