using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IMenuEditor
    {
        void EditMenu(DataLayer.Menu orderDto);
        Menu GetDish(int? id);
        IEnumerable<Menu> GetMenu();
        IEnumerable<Dish> GetDish();
        void Dispose();
    }
}