using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class CategoryDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public CategoryDataAccess(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }
        public class Category : INotifyPropertyChanged
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



        public List<Category> GetCategorys()
        {
            var categories = new List<Category>();
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand("SELECT * FROM Category", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var category = new Category
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                    };
                    categories.Add(category);
                }
            }
            return categories;
        }


        public void DeleteItemFromDatabase(Category category)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM Category WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", category.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

  
        public bool IsCategoryExists(string categoryName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string checkQuery = "SELECT COUNT(*) FROM Category WHERE Name = @Name";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Name", categoryName);

                int count = (int)checkCommand.ExecuteScalar();

                return count > 0;
            }
        }

        public bool AddCategory(string categoryName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "INSERT INTO Category (Name) VALUES (@Name)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", categoryName);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool editCategory(string categoryName, int categoryId)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "UPDATE Category SET name = @name WHERE id = @id";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", categoryName);
                command.Parameters.AddWithValue("@id", categoryId);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
