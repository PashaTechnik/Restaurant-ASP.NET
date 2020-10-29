using System;
using System.Collections.Generic;

namespace Project_.Net
{
    public partial class Dishdetails
    {
        public int Dishid { get; set; }
        public int Ingredientid { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
