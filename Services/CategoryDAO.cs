using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class CategoryDAO : ICategoryDataService
    {
        string connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=Symulator;Integrated Security=True;
                                    Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<CategoryModel> GetAllCategories(int userId)
        {
            List<CategoryModel> allCategories = new List<CategoryModel>();

            string sqlStatement = @"(SELECT [cat].[category_id], [cat].[name], [pref].[rating]
                                    FROM [Symulator].[dbo].[category] AS [cat]
                                    LEFT JOIN [Symulator].[dbo].[category_preference] AS [pref] ON 
                                    ([pref].[category_id] = [cat].[category_id]) WHERE [pref].[person_id] = @userId)
                                    UNION
                                    (SELECT [category_id], [name], '0' FROM [Symulator].[dbo].[category]
                                    WHERE [category_id] NOT IN (SELECT [category_id] FROM [Symulator].[dbo].[category_preference] WHERE person_id = @userId))
                                    ORDER BY [category_id] ASC";

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
                        allCategories.Add(new CategoryModel { Id = (int)reader[0], Name = (string)reader[1], Rating = (int)reader[2] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allCategories;
        }


        public List<CategoryModel> SearchCategories(int userId, string searchTerm)
        {
            List<CategoryModel> allCategories = new List<CategoryModel>();

            string sqlStatement = @"(SELECT [cat].[category_id], [cat].[name], [pref].[rating]
                                    FROM [Symulator].[dbo].[category] AS [cat]
                                    LEFT JOIN [Symulator].[dbo].[category_preference] AS [pref] ON 
                                    ([pref].[category_id] = [cat].[category_id])
                                    WHERE [pref].[person_id] = @userId AND [cat].[name] LIKE @name)
                                    UNION
                                    (SELECT [category_id], [name], '0' FROM [Symulator].[dbo].[category]
                                    WHERE [category_id] NOT IN (SELECT [category_id] FROM [Symulator].[dbo].[category_preference]
                                    WHERE person_id = @userId) AND [name] LIKE @name)
                                    ORDER BY [category_id] ASC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@name", '%' + searchTerm + '%');

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allCategories.Add(new CategoryModel { Id = (int)reader[0], Name = (string)reader[1], Rating = (int)reader[2] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allCategories;
        }

        public CategoryModel GetCategoryById(int id)
        {
            CategoryModel foundCategory = null;

            string sqlStatement = "SELECT * FROM dbo.Category WHERE Id LIKE @Id";

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
                        foundCategory = new CategoryModel { Id = (int)reader[0], Name = (string)reader[1] };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return foundCategory;
        }

        public void UpdatePreference(int userId, CategoryModel category)
        {
            string sqlStatement = @"IF EXISTS (SELECT * FROM [Symulator].[dbo].[category_preference] WHERE [category_id] LIKE @categoryId AND [person_id] LIKE @userId) 
                                    BEGIN
                                    UPDATE [Symulator].[dbo].[category_preference]
	                                SET [rating] = @categoryRating, [date_update] = GETDATE()
	                                WHERE [category_id] LIKE @categoryId AND [person_id] LIKE @userId;
                                    END
                                    ELSE
                                    BEGIN
                                    INSERT INTO [Symulator].[dbo].[category_preference]
	                                ([rating], [date_add], [category_id], [person_id])
	                                VALUES (@categoryRating, GETDATE(), @categoryId, @userId);
                                    END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@categoryId", category.Id);
                command.Parameters.AddWithValue("@categoryRating", category.Rating);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
