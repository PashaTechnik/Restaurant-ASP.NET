using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Moq;
using Xunit;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Controllers;
using BusinessLogic;

namespace Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfUsers()
        {
            // Arrange
            var mock = new Mock<IRestaurantService>();
            mock.Setup(repo=>repo.GetDish()).Returns(GetTestDishsRet());
            var controller = new HomeController(mock.Object);
 
            // Act
            //var result = controller.Index();
            var result = controller.CreateDish();
            dynamic mymodel = new ExpandoObject();

            // Assert
            mymodel.Dishs = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Dish>>(mymodel);

            Assert.Equal(GetTestDishs().Dishs.Count, model.Count());
        }
        private dynamic GetTestDishs()
        {
            dynamic mymodel = new ExpandoObject();
            var dishes = new List<Dish>
            {
                new Dish { Dishid = 1, Name = "dish1"},
                new Dish { Dishid = 2, Name = "dish2"},
                new Dish { Dishid = 3, Name = "dish3"},
                new Dish { Dishid = 4, Name = "dish4"}
            };
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Ingredientid = 1, Name = "ingredient1"},
                new Ingredient { Ingredientid = 2, Name = "ingredient2"},
                new Ingredient { Ingredientid = 3, Name = "ingredient3"},
                new Ingredient { Ingredientid = 4, Name = "ingredient4"}
            };
            mymodel.Ingredients = ingredients;
            mymodel.Dishs = dishes;

            return mymodel;
        }
        
        private List<Dish> GetTestDishsRet()
        {
            var dishes = new List<Dish>
            {
                new Dish { Dishid = 1, Name = "dish1"},
                new Dish { Dishid = 2, Name = "dish2"},
                new Dish { Dishid = 3, Name = "dish3"},
                new Dish { Dishid = 4, Name = "dish4"}
            };
            return dishes;
        }
    }
}