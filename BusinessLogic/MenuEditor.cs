using System.Collections.Generic;
using AutoMapper;
using DataLayer;

namespace BusinessLogic
{
    public class MenuEditor : IMenuEditor
    {
        IUnitOfWork Database { get; set; }

        public MenuEditor(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public void EditMenu(DataLayer.Menu menuDto)
        {
            DataLayer.Menu menu = new DataLayer.Menu
            {
                Positionid = menuDto.Positionid,
                Dishid = menuDto.Dishid,
                Size = menuDto.Size,
                Price = menuDto.Price
            };
            
            Database.Menu.Create(menuDto);
            Database.Save();
        }

        public Menu GetDish(int? id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Menu> GetMenu()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Menu, Menu>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Menu>, List<Menu>>(Database.Menu.GetAll());
        }
        
        public IEnumerable<Dish> GetDish()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Menu, Menu>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dish>, List<Dish>>(Database.Dishs.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}