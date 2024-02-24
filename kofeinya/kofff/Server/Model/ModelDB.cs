
namespace Coffee;

using System.Data.SQLite;
using System.Collections.Generic;

public class ProductRepository
{
    private readonly string _connectionString;
    private List<Product> products = new List<Product>();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Products (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Price REAL NOT NULL,
            Stock INTEGER NOT NULL
        )";

    public ProductRepository(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
        ReadDataFromDatabase();
    }

    private void ReadDataFromDatabase()
    {
        products = GetAllProductswhithID();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString);
        Console.WriteLine($"База данных: {_connectionString} успешно создана!");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection);
        command.ExecuteNonQuery();
    }

    
    public List<Product> GetAllProductswhithID()
    {
        List<Product> products = new List<Product>();

        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Products";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product(
                            reader["Name"].ToString(),
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToDouble(reader["Price"]),
                            Convert.ToInt32(reader["Stock"])
                        );
                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }
    public Product GetProductById(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Products WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Product product = new Product(

                            reader["Name"].ToString(),
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToDouble(reader["Price"]),
                            
                            Convert.ToInt32(reader["Stock"])
                            
                        );
                        return product;
                    }
                    return null;
                }
            }
        }
    }
  


    public void AddProduct(Product product)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Products (Name, Price, Stock) VALUES (@Name, @Price, @Stock)";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.ExecuteNonQuery();
            }
        }
    }
    public void DeleteProduct(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Products WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}