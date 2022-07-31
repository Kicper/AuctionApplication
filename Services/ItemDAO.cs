using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class ItemDAO : IItemDataService
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<ItemModel> GetAllItemsInCategory(int userId, int categoryId)
        {
            List<ItemModel> allItems = new List<ItemModel>();

            string sqlStatement = @"(SELECT [ite].[item_id], [ite].[name], [ite].[category_id], [pref].[rating]
                                    FROM [Symulator].[dbo].[item] AS [ite]
                                    LEFT JOIN [Symulator].[dbo].[item_preference] AS [pref] ON 
                                    ([pref].[item_id] = [ite].[item_id]) WHERE [pref].[person_id] = @userId
                                    AND [ite].[category_id] = @categoryId)
                                    UNION
                                    (SELECT [item_id], [name], [category_id], '0' FROM [Symulator].[dbo].[item]
                                    WHERE [category_id] = @categoryId
                                    AND [item_id] NOT IN (SELECT [item_id] FROM [Symulator].[dbo].[item_preference] WHERE person_id = @userId))
                                    ORDER BY [item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allItems.Add(new ItemModel { Id = (int)reader[0], Name = (string)reader[1], CategoryId = (int)reader[2], Rating = (int)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allItems;
        }

        public List<ItemModel> SearchItems(int userId, int categoryId, string searchTerm)
        {
            List<ItemModel> allItems = new List<ItemModel>();

            string sqlStatement = @"(SELECT [ite].[item_id], [ite].[name], [ite].[category_id], [pref].[rating]
                                    FROM [Symulator].[dbo].[item] AS [ite]
                                    LEFT JOIN [Symulator].[dbo].[item_preference] AS [pref] ON 
                                    ([pref].[item_id] = [ite].[item_id]) WHERE [pref].[person_id] = @userId
                                    AND [ite].[category_id] = @categoryId AND [ite].[name] LIKE @name)
                                    UNION
                                    (SELECT [item_id], [name], [category_id], '0' FROM [Symulator].[dbo].[item]
                                    WHERE [category_id] = @categoryId AND [name] LIKE @name
                                    AND [item_id] NOT IN (SELECT [item_id] FROM [Symulator].[dbo].[item_preference] WHERE person_id = @userId))
                                    ORDER BY [item_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@name", '%' + searchTerm + '%');

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allItems.Add(new ItemModel { Id = (int)reader[0], Name = (string)reader[1], CategoryId = (int)reader[2], Rating = (int)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allItems;
        }

        public ItemModel GetItemById(int id)
        {
            ItemModel foundItem = null;

            string sqlStatement = "SELECT * FROM dbo.Item WHERE Id LIKE @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundItem = new ItemModel { Id = (int)reader[0], Name = (string)reader[1] };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return foundItem;
        }

        public int Update(ItemModel item)
        {
            throw new NotImplementedException();
        }
    }
}
