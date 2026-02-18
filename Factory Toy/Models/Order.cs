using System;

namespace Factory_Toy.Models
{
    public class Order
    {
        public int IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int IdUser { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Prepayment { get; set; }
        public string Status { get; set; }
        public DateTime? DesiredDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public string DeliveryAddress { get; set; }
    }
}