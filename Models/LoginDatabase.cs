using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using yazlab2_1.Models;


namespace yazlab2_1.Models
{
    public class LoginDatabase
    {
        DateTime d;

        string connection = "server=localhost;port=5432;Database=database;" +
           " user ID=postgres;" + "password=1234";
        public bool LoginCheck()
        {


            using (var connect = new NpgsqlConnection(connection))
            {
               
               
                try
                {
                    connect.Open();
                    NpgsqlDataReader reader;
                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT user_id FROM userinfo WHERE username ='" + LoginInfo.username + "' AND password ='" + LoginInfo.password + "'", connect);
                    reader = cmd.ExecuteReader();
                    Console.WriteLine("Basarili");
                    if (reader.Read())
                    {
                        
                        LoginInfo.user_id = reader.GetInt32(0);
                        d = DateTime.Now;
                        update(d);
                       


                        connect.Close();

                        return true;

                    }
                    else
                    {
                        connect.Close();

                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Basarisiz");


                    return false;
                    throw;
                }
            }
        }

        public void update(DateTime d)
        {
            using (var connect = new NpgsqlConnection(connection))
            {


                try
                {
                    connect.Open();
                    NpgsqlDataReader reader;
                    NpgsqlCommand cmd = new NpgsqlCommand("Update userinfo SET login_date='"+d+"' WHERE user_id ='" + LoginInfo.user_id+"'", connect);
                    reader = cmd.ExecuteReader();
                }
                catch { }
                }

        }


    }
}
