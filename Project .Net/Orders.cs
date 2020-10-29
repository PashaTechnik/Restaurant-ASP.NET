using System;
using System.Collections.Generic;

namespace Project_.Net
{
    public partial class Orders
    {
        public Orders()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        public int Orderid { get; set; }
        public string Clientname { get; set; }
        public int? Price { get; set; }

        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
    }
}
