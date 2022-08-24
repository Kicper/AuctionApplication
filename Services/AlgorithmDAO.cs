using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class AlgorithmDAO : IAlgorithmDataService
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<(int ItemId, int Frequency)> ItemsFrequency()
        {
            List<(int ItemId, int Frequency)> itemsFrequency = new List<(int ItemId, int Frequency)>();

            string sqlStatement = @"SELECT [item_id], COUNT([item_id])
                                    FROM [Symulator].[dbo].[auction_history]
                                    GROUP BY [item_id] ORDER BY [item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        itemsFrequency.Add( ((int)reader[0], (int)reader[1]) );
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return itemsFrequency;
        }

        public List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> MinMaxPriceAndDate()
        {
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = new List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)>();

            string sqlStatement = @"SELECT [au].[item_id], MIN([off].[price]), MAX([off].[price]), MIN([off].[date]), MAX([off].[date])
                                    FROM [Symulator].[dbo].[offer] AS [off] INNER JOIN
									[Symulator].[dbo].[auction_history] AS [au] ON [off].[auction_history_id] = [au].[auction_history_id]
                                    GROUP BY [au].[item_id] ORDER BY [au].[item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        minMaxPriceAndDate.Add( ((int)reader[0], (int)reader[1], (int)reader[2], reader[3].ToString(), reader[4].ToString()) );
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return minMaxPriceAndDate;
        }

        public List<(int ItemId, int Rating)> ItemPreferences(int userId)
        {
            List<(int ItemId, int Rating)> itemPreferences = new List<(int ItemId, int Rating)>();

            string sqlStatement = @"SELECT [item_id], [rating]
                                    FROM [Symulator].[dbo].[item_preference]
                                    WHERE [person_id] = @userId ORDER BY [item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        itemPreferences.Add(((int)reader[0], (int)reader[1]));
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return itemPreferences;
        }

        public List<(int CategoyId, int Rating)> CategoryPreferences(int userId)
        {
            List<(int CategoyId, int Rating)> categoryPreferences = new List<(int CategoyId, int Rating)>();

            string sqlStatement = @"SELECT [category_id], [rating]
                                    FROM [Symulator].[dbo].[category_preference]
                                    WHERE [person_id] = @userId ORDER BY [category_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        categoryPreferences.Add(((int)reader[0], (int)reader[1]));
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return categoryPreferences;
        }

        public List<(int ItemId, int AvgPrice, string StartTime, string EndTime)> AveragePriceStartEndDate()
        {
            List<(int ItemId, int AvgPrice, string StartTime, string EndTime)> averagePriceStartEndDate = new List<(int ItemId, int AvgPrice, string StartTime, string EndTime)>();

            string sqlStatement = @"SELECT [au].[item_id], AVG([off].[price]), MIN([off].[date]), MAX([off].[date])
                                    FROM [Symulator].[dbo].[offer] AS [off] INNER JOIN
									[Symulator].[dbo].[auction_history] AS [au] ON [off].[auction_history_id] = [au].[auction_history_id]
                                    GROUP BY [au].[item_id], [au].[auction_history_id] ORDER BY [au].[item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        averagePriceStartEndDate.Add( ((int)reader[0], (int)reader[1], reader[2].ToString(), reader[3].ToString()) );
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return averagePriceStartEndDate;
        }
    }
}
