using Microsoft.Data.Sqlite;
using Refactoring.Dto;

namespace Refactoring.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly SqliteConnection _connection;
        public ProductRepository(SqliteConnection connection)
        {
			_connection = connection;
        }

		public Product? GetProduct(int productId, string productType)
		{
			using (_connection)
			{	
				var command = new SqliteCommand($"SELECT Id, Name, Price FROM Products WHERE Id = @ProductId AND Type = @ProductType", _connection);
				command.Parameters.Add(new SqliteParameter("@ProductId", productId));
				command.Parameters.Add(new SqliteParameter("@ProductType", productType));

				_connection.Open();
				var reader = command.ExecuteReader();
				
				if (!reader.Read())
				{
					return null;
				}

				var product = new Product
				{
					Id = (long)reader["Id"],
					Name = (string)reader["Name"],
					Price = (double)reader["Price"]
				};

				return product;
			}
		}
	}
}
