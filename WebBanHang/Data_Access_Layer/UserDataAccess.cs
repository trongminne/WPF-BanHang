using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class UserDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public UserDataAccess()
        {
            databaseConnection = new DatabaseConnection();
        }
        public class User : INotifyPropertyChanged
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
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsAdmin { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Avatar { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }

            public Brush BackgroundColor { get; set; }

            // Triển khai INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Check đăng nhập
        public bool CheckUser(string email, string password)
        {
            using (var connection = databaseConnection.OpenConnection())
            {
                var query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password AND IsAdmin = 1";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        // Lưu lấy họ tên tài khoản khi đăng nhập thành công
        public class UserInfo
        {
            public string LastName { get; }
            public string FirstName { get; }

            public UserInfo(string lastName, string firstName)
            {
                LastName = lastName;
                FirstName = firstName;
            }
        }

        public UserInfo GetUserInfo(string email)
        {
             using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "SELECT LastName, FirstName FROM Users WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string lastName = reader.GetString(0);
                            string firstName = reader.GetString(1);
                            return new UserInfo(lastName, firstName);
                        }
                    }
                }
            }

            return null;
        }



        public List<User> GetUsers()
        {
            var users = new List<User>();
             using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand("SELECT * FROM Users", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = reader.IsDBNull(0) ? 0 : (int)reader["Id"],
                        Email = reader.IsDBNull(1) ? string.Empty : (string)reader["Email"],
                        Password = reader.IsDBNull(2) ? string.Empty : (string)reader["Password"],
                        IsAdmin = reader.IsDBNull(3) ? false : (bool)reader["IsAdmin"],
                        FirstName = reader.IsDBNull(4) ? string.Empty : (string)reader["FirstName"],
                        LastName = reader.IsDBNull(5) ? string.Empty : (string)reader["LastName"],
                        Avatar = reader.IsDBNull(6) ? string.Empty : (string)reader["Avatar"],
                        Phone = reader.IsDBNull(7) ? string.Empty : (string)reader["Phone"],
                        Address = reader.IsDBNull(8) ? string.Empty : (string)reader["Address"]
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public void DeleteItemFromDatabase(User user)
        {
             using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM Users WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
