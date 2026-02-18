using System.Collections.Generic;
using System.Linq;
using Dapper;
using Factory_Toy.Models;

namespace Factory_Toy.Services
{
    public static class OrderItemService
    {
        public static List<OrderItem> GetAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.Query<OrderItem>("SELECT * FROM orderitems ORDER BY idorderitem").ToList();
            }
        }

        private static int GetNextId()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(idorderitem), 0) + 1 FROM orderitems");
            }
        }

        public static void Add(OrderItem item)
        {
            item.IdOrderItem = GetNextId();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"INSERT INTO orderitems 
                      (idorderitem, idorder, idproduct, quantity, iscustom, customdescription, customsketch)
                      VALUES (@IdOrderItem, @IdOrder, @IdProduct, @Quantity, @IsCustom, @CustomDescription, @CustomSketch)",
                    item);
            }
        }

        public static void Update(OrderItem item)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"UPDATE orderitems SET
                        idorder = @IdOrder,
                        idproduct = @IdProduct,
                        quantity = @Quantity,
                        iscustom = @IsCustom,
                        customdescription = @CustomDescription,
                        customsketch = @CustomSketch
                      WHERE idorderitem = @IdOrderItem",
                    item);
            }
        }

        public static void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM orderitems WHERE idorderitem = @id", new { id });
            }
        }
    }
}