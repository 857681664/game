using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using game.entity;
using MySql.Data.MySqlClient;

namespace game
{
    public class SqlOperate
    {
        private static string url = "Server=localhost;Database=db;Uid=root;Pwd=13566189672";
        private static MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

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
                reader.Close();
                
            }
            return true;
        }

        public void FillMonsterTable(List<CardMonster> twoMonsters, List<CardMonster> threeMonsters, List<CardMonster> fourMonsters, int length)
        {
            connection.Open();
            try
            {
                int nowLength = 0;
                command = connection.CreateCommand();
                command.CommandText = "select * from monster";
                reader = command.ExecuteReader();
                while (reader.Read())
                    nowLength++;
                if (nowLength != length)
                {
                    command.CommandText =
                    "insert into monster values(@id, @star, @name, @descripe, @effect, @attack, @defense, @prop)";
                    foreach (CardMonster c in twoMonsters)
                    {
                        command.Parameters.AddWithValue("@id", c.Star.ToString() + c.Number);
                        command.Parameters.AddWithValue("@star", c.Star);
                        command.Parameters.AddWithValue("@name", c.Name);
                        command.Parameters.AddWithValue("@descripe", c.Descripe);
                        command.Parameters.AddWithValue("@effect", "无");
                        command.Parameters.AddWithValue("@attack", c.Attack);
                        command.Parameters.AddWithValue("@defense", c.Defense);
                        command.Parameters.AddWithValue("@prop", Const.PropList.ElementAt((int)c.Prop));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    foreach (var c in threeMonsters.Cast<ThreeStarMonster>())
                    {
                        command.Parameters.AddWithValue("@id", c.Star.ToString() + c.Number);
                        command.Parameters.AddWithValue("@star", c.Star);
                        command.Parameters.AddWithValue("@name", c.Name);
                        command.Parameters.AddWithValue("@descripe", c.Descripe);
                        command.Parameters.AddWithValue("@effect", c.Effect);
                        command.Parameters.AddWithValue("@attack", c.Attack);
                        command.Parameters.AddWithValue("@defense", c.Defense);
                        command.Parameters.AddWithValue("@prop", Const.PropList.ElementAt((int)c.Prop));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    foreach (var c in fourMonsters.Cast<FourStarMonster>())
                    {
                        command.Parameters.AddWithValue("@id", c.Star.ToString() + c.Number);
                        command.Parameters.AddWithValue("@star", c.Star);
                        command.Parameters.AddWithValue("@name", c.Name);
                        command.Parameters.AddWithValue("@descripe", c.Descripe);
                        command.Parameters.AddWithValue("@effect", c.Effect);
                        command.Parameters.AddWithValue("@attack", c.Attack);
                        command.Parameters.AddWithValue("@defense", c.Defense);
                        command.Parameters.AddWithValue("@prop", Const.PropList.ElementAt((int)c.Prop));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                command = null;
                reader.Close();
            }
        }
    }
}