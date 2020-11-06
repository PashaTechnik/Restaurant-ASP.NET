using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class Dish
    {

        public int Dishid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Dishdetails> Dishdetails { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
