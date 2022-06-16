using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class UserDAO
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindUserByLoginAndPassword(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.person WHERE login = @login AND password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@login", System.Data.SqlDbType.VarChar, 255).Value = user.Login;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 255).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return success;
        }

        public bool FindUserByLogin(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.person WHERE login = @login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@login", System.Data.SqlDbType.VarChar, 255).Value = user.Login;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return success;
        }

        public bool AddNewUser(UserModel user)
        {
            bool success = false;
            string sqlStatement = "INSERT INTO dbo.person (login, password) VALUES (@login, @password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@login", System.Data.SqlDbType.VarChar, 255).Value = user.Login;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 255).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return success;
        }

        public int GetUserId(UserModel user)
        {
            int userId = 0;
            string sqlStatement = "SELECT person_id FROM dbo.person WHERE login = @login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@login", System.Data.SqlDbType.VarChar, 255).Value = user.Login;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userId = (int)reader[0];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return userId;
        }
    }
}
