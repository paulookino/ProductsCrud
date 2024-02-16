using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProductsCrud.Repositories

public class ProductRepository
{
    private readonly string connectionString;

    public ProductRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT ProductId, ProductName, Price, Stock FROM Products";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = Convert.ToString(reader["ProductName"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Stock = Convert.ToInt32(reader["Stock"])
                        };

                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }

    public Product GetProductById(int productId)
    {
        // Implement code to retrieve a product by its Id
    }

    public void AddProduct(Product product)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Products (ProductName, Price, Stock) VALUES (@ProductName, @Price, @Stock)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateProduct(Product product)
    {
        // Implement code to update a product
    }

    public void DeleteProduct(int productId)
    {
        // Implement code to delete a product by its Id
    }
}
