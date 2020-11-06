using System.Collections.Generic;
using AutoMapper;
using DataLayer;

namespace BusinessLogic
{
    public class DishCreator : IDishCreator
    {
        IUnitOfWork Database { get; set; }

        public DishCreator(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public void EditDish(Dish dishDto)
        {
            DataLayer.Dish dish = new DataLayer.Dish
            {
                Name = dishDto.Name,
            };
            
            Database.Dishs.Create(dish);
            Database.Save();
        }

        public void EditDetails(Dishdetails detailsDto)
        {
            DataLayer.Dishdetails dishDetail = new DataLayer.Dishdetails
            {
                Dishid = detailsDto.Dishid,
                Ingredientid = detailsDto.Ingredientid
            };
            
            Database.Dishdetails.Create(dishDetail);
            Database.Save();
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Ingredient, Ingredient>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Ingredient>, List<Ingredient>>(Database.Ingredients.GetAll());
        }

        public IEnumerable<Dish> GetDish()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Dish, Dish>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dish>, List<Dish>>(Database.Dishs.GetAll());
        }

        public IEnumerable<Dishdetails> GetDetails()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Dishdetails, Dishdetails>()).CreateMapper();
            return mapper.Map<IEnumerable<DataLayer.Dishdetails>, List<Dishdetails>>(Database.Dishdetails.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}