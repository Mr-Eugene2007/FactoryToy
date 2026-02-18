using System.Collections.Generic;
using System.Linq;
using Dapper;
using Factory_Toy.Models;

namespace Factory_Toy.Services
{
    public static class OrderService
    {
        public static List<Order> GetAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.Query<Order>("SELECT * FROM orders ORDER BY idorder").ToList();
            }
        }

        private static int GetNextId()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(idorder), 0) + 1 FROM orders");
            }
        }

        public static void Add(Order order)
        {
            order.IdOrder = GetNextId();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"INSERT INTO orders 
                      (idorder, orderdate, iduser, totalamount, prepayment, status, desireddate, actualdate, deliveryaddress)
                      VALUES (@IdOrder, @OrderDate, @IdUser, @TotalAmount, @Prepayment, @Status, @DesiredDate, @ActualDate, @DeliveryAddress)",
                    order);
            }
        }

        public static void Update(Order order)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"UPDATE orders SET
                        orderdate = @OrderDate,
                        iduser = @IdUser,
                        totalamount = @TotalAmount,
                        prepayment = @Prepayment,
                        status = @Status,
                        desireddate = @DesiredDate,
                        actualdate = @ActualDate,
                        deliveryaddress = @DeliveryAddress
                      WHERE idorder = @IdOrder",
                    order);
            }
        }

        public static void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM orders WHERE idorder = @id", new { id });
            }
        }
    }
}