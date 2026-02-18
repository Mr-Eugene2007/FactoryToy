using System.Collections.Generic;
using System.Linq;
using Dapper;
using Factory_Toy.Models;

namespace Factory_Toy.Services
{
    public static class UserService
    {
        public static List<User> GetAll()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.Query<User>("SELECT * FROM users ORDER BY iduser").ToList();
            }
        }

        private static int GetNextId()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                return conn.ExecuteScalar<int>("SELECT COALESCE(MAX(iduser), 0) + 1 FROM users");
            }
        }

        public static void Add(User user)
        {
            user.IdUser = GetNextId();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"INSERT INTO users 
                      (iduser, login, password, fullname, email, phone, idrole)
                      VALUES (@IdUser, @Login, @Password, @FullName, @Email, @Phone, @IdRole)",
                    user);
            }
        }

        public static void Update(User user)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute(
                    @"UPDATE users SET
                        login = @Login,
                        password = @Password,
                        fullname = @FullName,
                        email = @Email,
                        phone = @Phone,
                        idrole = @IdRole
                      WHERE iduser = @IdUser",
                    user);
            }
        }

        public static void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM users WHERE iduser = @id", new { id });
            }
        }
    }
}