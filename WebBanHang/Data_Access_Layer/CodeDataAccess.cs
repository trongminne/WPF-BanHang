using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class CodeDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public CodeDataAccess(DatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }
        public class Code : INotifyPropertyChanged
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
            public string MaCode { get; set; }
            public int Price { get; set; }

            public Brush BackgroundColor { get; set; }

            // Triển khai INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool CheckCode(string email, string password)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "SELECT COUNT(*) FROM Codes WHERE Email = @Email AND Password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public List<Code> GetCodes()
        {
            var codes = new List<Code>();
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand("SELECT * FROM Code", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var code = new Code
                    {
                        Id = reader.IsDBNull(0) ? 0 : (int)reader["Id"],
                        Name = reader.IsDBNull(1) ? string.Empty : (string)reader["Name"],
                        MaCode = reader.IsDBNull(1) ? string.Empty : (string)reader["Code"],
                        Price = reader.IsDBNull(0) ? 0 : (int)reader["Price"],

                    };
                    codes.Add(code);
                }
            }
            return codes;
        }

        public void DeleteItemFromDatabase(Code code)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM Code WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", code.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool IsCodeExists(string codeName)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string checkQuery = "SELECT COUNT(*) FROM Code WHERE Name = @Name";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@Name", codeName);

                int count = (int)checkCommand.ExecuteScalar();

                return count > 0;
            }
        }

        public bool AddCode(string codeName, string  codeMa, int codePrice)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "INSERT INTO Code (Name, Code, Price) VALUES (@Name, @Code, @Price)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", codeName);
                command.Parameters.AddWithValue("@Code", codeMa);
                command.Parameters.AddWithValue("@Price", codePrice);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool editCode(string codeName, int codeId, string codeMa, int codePrice)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                

                string query = "UPDATE Code SET name = @name, code = @code, price = @price  WHERE id = @id";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", codeName);
                command.Parameters.AddWithValue("@id", codeId);
                command.Parameters.AddWithValue("@code", codeMa);
                command.Parameters.AddWithValue("@price", codePrice);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
