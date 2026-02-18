using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Toy.Models
{
    public class OrderItem
    {
        public int IdOrderItem { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public bool IsCustom { get; set; }
        public string CustomDescription { get; set; }
        public string CustomSketch { get; set; }
    }

}
