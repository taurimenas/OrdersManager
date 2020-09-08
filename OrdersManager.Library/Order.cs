using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Library
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Edited { get; set; } = DateTime.UtcNow;
        public string Delivery { get; set; }
        public string Reveiver { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; } = 1;
        public bool SelfPickUp { get; set; }
    }
}
