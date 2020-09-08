using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Library
{
    public class Order
    {
        public long Id { get; }
        public DateTime Created { get; } = DateTime.UtcNow;
        public DateTime Edited { get; set; } = DateTime.UtcNow;
        public DateTime Delivery { get; set; }
        public string Receiver { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; } = 1;
        public bool SelfPickUp { get; set; } = false;
    }
}
