using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Library
{
    public class SqliteDataAccess
    {
        public static List<Order> LoadOrders()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<Order>("select * from Orders", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void EditOrder(Order order)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("update Orders set Edited = @Edited, Delivery = @Delivery, Receiver = @Receiver, Address = @Address, Quantity = @Quantity, SelfPickUp = @SelfPickUp where Id = @Id", order);
            }
        }
        public static void AddOrder(Order order)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("insert into Orders (Created, Edited, Delivery, Receiver, Address, Quantity, SelfPickUp) values (@Created, @Edited, @Delivery, @Receiver, @Address, @Quantity, @SelfPickUp)", order);
            }
        }
        public static void DeleteOrder(Order order)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("delete from Orders where Id = @Id", order);
            }
        }
        private static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}
