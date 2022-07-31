using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class OfferDAO : IOfferDataService
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<OfferModel> GetAllOffersInAuction(int auctionId)
        {
            List<OfferModel> allOffers = new List<OfferModel>();

            string sqlStatement = @"SELECT [off].[offer_id], [off].[auction_history_id], [off].[date], [off].[price], [off].[person_id]
                                    FROM [dbo].[offer] AS [off] WHERE [off].[auction_history_id] LIKE @auctionId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@auctionId", auctionId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allOffers.Add(new OfferModel { Id = (int)reader[0], AuctionId = (int)reader[1], Date = reader[2].ToString(), Price = (int)reader[3], Person = (int)reader[4] });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allOffers;
        }
    }
}
