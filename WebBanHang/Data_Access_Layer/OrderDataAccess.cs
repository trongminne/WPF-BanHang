using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Media;

namespace WebBanHang.Data_Access_Layer
{
    public class OrderDataAccess
    {
        private readonly DatabaseConnection databaseConnection;

        public OrderDataAccess()
        {
            databaseConnection = new DatabaseConnection();
        }
        public class Order : INotifyPropertyChanged
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
            public string NameUser { get; set; }
            public string NameProduct { get; set; }
            public string Phone { get; set; }
            public double Pay { get; set; }
            public int Quantity { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }


            public Brush BackgroundColor { get; set; }

            // Triển khai INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        

        // Lưu lấy họ tên tài khoản khi đăng nhập thành công
        public class OrderInfo
        {
            public string LastName { get; }
            public string FirstName { get; }

            public OrderInfo(string lastName, string firstName)
            {
                LastName = lastName;
                FirstName = firstName;
            }
        }

        public OrderInfo GetOrderInfo(string email)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "SELECT LastName, FirstName FROM Orders WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string lastName = reader.GetString(0);
                            string firstName = reader.GetString(1);
                            return new OrderInfo(lastName, firstName);
                        }
                    }
                }
            }

            return null;
        }



        public List<Order> GetOrders()
        {
            var orders = new List<Order>();
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var command = new SqlCommand(
                    @"SELECT od.Id, u.FirstName, u.LastName, u.Phone, p.Name, od.Quantity, od.Pay, od.Status
              FROM OrderDetail od
              INNER JOIN Users u ON od.idUser = u.Id
              INNER JOIN Product p ON od.ProductId = p.id", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = reader.IsDBNull(0) ? 0 : (int)reader["Id"],
                        NameUser = reader.IsDBNull(1) ? string.Empty : ((string)reader["FirstName"]) + (reader.IsDBNull(2) ? string.Empty : (string)reader["LastName"]),
                        Phone = reader.IsDBNull(3) ? string.Empty : (string)reader["Phone"],
                        NameProduct = reader.IsDBNull(4) ? string.Empty : (string)reader["Name"],
                        Quantity = reader.IsDBNull(5) ? 0 : (int)reader["Quantity"],
                        Pay = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                        Status = reader.IsDBNull(7) ? 0 : (int)reader["Status"]
                    };

                    // Map Status to StatusName
                    switch (order.Status)
                    {
                        case 0:
                            order.StatusName = "Chưa giao";
                            break;
                        case 1:
                            order.StatusName = "Đang giao";
                            break;
                        case 2:
                            order.StatusName = "Đã nhận";
                            break;
                        default:
                            order.StatusName = "Trạng thái không xác định";
                            break;
                    }

                    orders.Add(order);
                }
            }
            return orders;
        }


        public void DeleteItemFromDatabase(Order order)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "DELETE FROM OrderDetail WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", order.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditItemFromDatabase(Order order)
        {
           using (var connection = databaseConnection.OpenConnection())
            {
                
                var query = "UPDATE OrderDetail SET Status = 1 WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", order.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
