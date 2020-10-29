using System;
using System.Collections.Generic;

namespace Project_.Net
{
    public partial class Menu
    {
        public Menu()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        public int Positionid { get; set; }
        public int? Dishid { get; set; }
        public int? Size { get; set; }
        public int? Price { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
    }
}
