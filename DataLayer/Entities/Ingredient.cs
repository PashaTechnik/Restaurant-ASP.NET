using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Dishdetails = new HashSet<Dishdetails>();
        }

        public int Ingredientid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dishdetails> Dishdetails { get; set; }
    }
}
