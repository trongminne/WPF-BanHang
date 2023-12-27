using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class ProductDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public ProductDataAccess(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }
        public class Product : INotifyPropertyChanged
        {
            private bool isSelected;

            public bool IsSelected
            {
                get { return isSelected; }
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        OnPropertyChanged(nameof(IsSelected));
                    }
                }
            }

            public int STT { get; set; }
            public int Id { get; set; }
            public int BrandId { get; set; }
            public int CategoryId { get; set; }

            public string Name { get; set; }
            public string BrandName { get; set; }
            public string CategoryName { get; set; }
            public double Price { get; set; }
            public double PriceSale { get; set; }
            public int Quantity { get; set; }

            public Brush BackgroundColor { get; set; }

            // Triển khai INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool CheckProduct(string email, string password)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "SELECT COUNT(*) FROM Products WHERE Email = @Email AND Password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand("SELECT p.Id, p.Name, b.Name AS BrandName, b.Id AS BrandId, c.Name AS CategoryName, c.Id AS CategoryId, p.Price, p.PriceDiscount, p.quantity FROM Product p INNER JOIN Brand b ON p.BrandId = b.Id INNER JOIN Category c ON p.CategoryId = c.Id", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        BrandName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        BrandId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        CategoryName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        CategoryId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                        Price = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                        PriceSale = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                        Quantity = reader.IsDBNull(8) ? 0 : reader.GetInt32(8)
                    };
                    products.Add(product);
                }
            }
            return products;
        }




        public void DeleteItemFromDatabase(Product product)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM Product WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.ExecuteNonQuery();
                }
            }
        }



        public bool IsProductExists(string productName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string checkQuery = "SELECT COUNT(*) FROM Product WHERE Name = @Name";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Name", productName);

                int count = (int)checkCommand.ExecuteScalar();

                return count > 0;
            }
        }

        public bool AddProduct(string productName, int categoryId, int brandId, double price, double priceSale, int quantity)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                int TypeId = (priceSale > 0) ? 1 : 2; // Nếu có khuyến mãi thì thuộc sp khuyến mãi

                string query = "INSERT INTO Product (Name, CategoryId, BrandId, Price, PriceDiscount, Quantity, TypeId) VALUES (@Name, @CategoryId, @BrandId, @Price, @PriceSale, @Quantity, @TypeId)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", productName);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                command.Parameters.AddWithValue("@BrandId", brandId);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@PriceSale", priceSale);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@TypeId", TypeId);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        public bool editProduct(string productName, int productId, int categoryId, int brandId, double price, double priceSale, int quantity)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                int TypeId = (priceSale > 0) ? 1 : 2; // Nếu có khuyến mãi thì thuộc sp khuyến mãi

                string query = "UPDATE Product SET name = @name, CategoryId = @categoryId, BrandId= @brandId, Price = @price, PriceDiscount = @priceSale, quantity = @quantity, TypeId = @TypeId WHERE id = @id";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", productName);
                command.Parameters.AddWithValue("@id", productId);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@brandId", brandId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@priceSale", priceSale);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@TypeId", TypeId);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
