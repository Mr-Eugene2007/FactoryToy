using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;


namespace Factory_Toy
{
    public static class Database
    {
        public static NpgsqlConnection GetConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["ToyFactory"].ConnectionString;
            return new NpgsqlConnection(connStr);
        }
    }

}
