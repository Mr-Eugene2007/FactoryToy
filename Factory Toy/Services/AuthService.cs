using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BCrypt.Net;
using Factory_Toy.Models;


namespace Factory_Toy
{
    public static class AuthService
    {
        public static User Login(string login, string password)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                var user = conn.QueryFirstOrDefault<User>(
                    @"SELECT 
                    iduser AS IdUser,
                    login AS Login,
                    password AS Password,
                    fullname AS FullName,
                    email AS Email,
                    phone AS Phone,
                    idrole AS IdRole
                  FROM users
                  WHERE login = @login",
                    new { login });

                if (user == null)
                    return null;

                return user.Password == password ? user : null;
            }
        }
    }


}
