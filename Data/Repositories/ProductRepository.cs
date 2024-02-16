
using Business.Interfaces.Repositories;
using Business.Models;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT Id, Name, Price, Stock, Active FROM Products";
            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Product product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    Stock = reader.GetInt32(3),
                    Active = reader.GetBoolean(4)
                };

                products.Add(product);
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT Id, Name, Price, Stock, Active FROM Products WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var product = new Product
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                Active = reader.GetBoolean(reader.GetOrdinal("Active"))
                            };

                            return product;
                        }
                    }
                }
            }

            return null;
        }

        public async Task AddProductAsync(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "INSERT INTO Products (Name, Price, Stock, Active, CreatedAt) VALUES (@Name, @Price, @Stock, @Active, @CreatedAt)";
            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Active", product.Active);
            command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateProductAsync(Product updatedProduct)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock, UpdatedAt = @UpdatedAt WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Name", updatedProduct.Name);
                    command.Parameters.AddWithValue("@Price", updatedProduct.Price);
                    command.Parameters.AddWithValue("@Stock", updatedProduct.Stock);
                    command.Parameters.AddWithValue("@UpdatedAt", updatedProduct.UpdatedAt);
                    command.Parameters.AddWithValue("@Id", updatedProduct.Id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Products SET Active = @Active, DeletedAt = @DeletedAt WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Active", true);
                    command.Parameters.AddWithValue("@DeletedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@Id", productId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }
    }


}
