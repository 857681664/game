using System;
using MySql.Data.MySqlClient;

namespace game
{
    public class SqlOperate
    {
        private static string url = "Server=localhost;Database=db;Uid=root;Pwd=13566189672";
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlDataReader reader;

        static SqlOperate()
        {
            connection = new MySqlConnection(url);
        }

        public void RegisterUser(string userName,string password)
        {
            connection.Open();
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `user`(`user`.`name`,`user`.`password`) values(@name,@password);";
                command.Parameters.AddWithValue("@name", userName);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connection.Close();
                command = null;
            }
        }

        public bool LoginUser(string userName, string password)
        {
            connection.Open();
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from user where user.name = @name,user.password = @password";
                command.Parameters.AddWithValue("@name", userName);
                command.Parameters.AddWithValue("@password", password);
                reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connection.Close();
                command = null;
                reader = null;
                
            }
            return true;
        }
    }
}