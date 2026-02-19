using Dapper;
using Factory_Toy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Toy.Services
{
    public static class ProductService
    {
        // Получить все товары
        public static List<Product> GetAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.Query<Product>(
                    @"SELECT 
                p.idproduct,
                p.name,
                p.description,
                p.idcategory,
                p.retailprice,
                p.costprice,
                p.stockquantity,
                p.status,
                c.categoryname AS CategoryName
              FROM products p
              LEFT JOIN productcategories c ON p.idcategory = c.idcategory
              ORDER BY p.idproduct"
                ).ToList();
            }
        }

        // Получить новый ID (MAX + 1)
        private static int GetNextId()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(idproduct), 0) + 1 FROM products");
            }
        }

        // Добавить товар
        public static void Add(Product product)
        {
            product.IdProduct = GetNextId();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"INSERT INTO products 
                      (idproduct, name, description, idcategory, retailprice, costprice, stockquantity, status)
                      VALUES (@IdProduct, @Name, @Description, @IdCategory, @RetailPrice, @CostPrice, @StockQuantity, @Status)",
                    product);
            }
        }

        // Обновить товар
        public static void Update(Product product)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"UPDATE products SET
                        name = @Name,
                        description = @Description,
                        idcategory = @IdCategory,
                        retailprice = @RetailPrice,
                        costprice = @CostPrice,
                        stockquantity = @StockQuantity,
                        status = @Status
                      WHERE idproduct = @IdProduct",
                    product);
            }
        }

        // Удалить товар
        public static void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM products WHERE idproduct = @id", new { id });
            }
        }
    }

}
