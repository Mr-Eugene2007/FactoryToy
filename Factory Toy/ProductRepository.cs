using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Factory_Toy.Models;


namespace Factory_Toy
{
    public class ProductRepository
    {
        public List<Product> GetAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.Query<Product>("SELECT * FROM products").ToList();
            }
        }

        public void Add(Product p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    "INSERT INTO products (name, price) VALUES (@Name, @Price)",
                    p);
            }
        }

        public void Update(Product p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    "UPDATE products SET name=@Name, price=@Price WHERE id=@Id",
                    p);
            }
        }

        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM products WHERE id=@id", new { id });
            }
        }
    }

}
