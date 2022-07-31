using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class AuctionDAO : IAuctionDataService
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<ItemModel> GetAllAvailableAuctions()
        {
            List<ItemModel> allAuctions = new List<ItemModel>();

            string sqlStatement = @"SELECT DISTINCT [auc].[item_id], [it].[name], [it].[category_id], [off].[auction_history_id]
                                    FROM [Symulator].[dbo].[offer] AS [off]
                                    JOIN [Symulator].[dbo].[auction_history] AS [auc] ON [off].[auction_history_id] = [auc].[auction_history_id]
                                    JOIN [Symulator].[dbo].[item] AS [it] ON [it].[item_id] = [auc].[item_id]
                                    WHERE [off].[auction_history_id] NOT IN
                                    (SELECT DISTINCT [auction_history_id] FROM [Symulator].[dbo].[offer] WHERE [status] = 'finished')
                                    ORDER BY [it].[name] ASC, [off].[auction_history_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allAuctions.Add(new ItemModel { Id = (int)reader[0], Name = (string)reader[1], CategoryId = (int)reader[2], AuctionId = (int)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allAuctions;
        }
    }
}
