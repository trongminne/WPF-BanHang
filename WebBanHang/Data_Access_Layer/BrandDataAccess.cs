using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class BrandDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public BrandDataAccess(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }
        public class Brand : INotifyPropertyChanged
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

            public int Id { get; set; }

            public int STT { get; set; }

            public string Name { get; set; }
           
            public Brush BackgroundColor { get; set; }

            // Triển khai INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool CheckBrand(string email, string password)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "SELECT COUNT(*) FROM Brands WHERE Email = @Email AND Password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public List<Brand> GetBrands()
        {
            var brands = new List<Brand>();
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand("SELECT * FROM Brand", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var brand = new Brand
                    {
                        Id = reader.IsDBNull(0) ? 0 : (int)reader["Id"],
                        Name = reader.IsDBNull(1) ? string.Empty : (string)reader["Name"],
                        
                    };
                    brands.Add(brand);
                }
            }
            return brands;
        }

        public void DeleteItemFromDatabase(Brand brand)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM Brand WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", brand.Id);
                    command.ExecuteNonQuery();
                }
            }
        }


        public bool IsBrandExists(string brandName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string checkQuery = "SELECT COUNT(*) FROM Brand WHERE Name = @Name";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Name", brandName);

                int count = (int)checkCommand.ExecuteScalar();

                return count > 0;
            }
        }

        public bool AddBrand(string brandName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "INSERT INTO Brand (Name) VALUES (@Name)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", brandName);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool editBrand(string brandName, int brandId)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "UPDATE Brand SET name = @name WHERE id = @id";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", brandName);
                command.Parameters.AddWithValue("@id", brandId);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
