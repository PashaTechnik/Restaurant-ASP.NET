using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class Orderdetails
    {
        public int Orderid { get; set; }
        public int Positionid { get; set; }
        public int? Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Menu Position { get; set; }
    }
}
