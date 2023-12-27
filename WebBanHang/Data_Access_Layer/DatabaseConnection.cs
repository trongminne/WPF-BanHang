using System.Data.SqlClient;

namespace WebBanHang.Data_Access_Layer
{
    public class DatabaseConnection
    {
        private readonly string connectionString;

        public DatabaseConnection()
        {
            connectionString = "Data Source=.;Initial Catalog=WebBanHang;Integrated Security=True";

            //connectionString = "Data Source=SQL5101.site4now.net;Initial Catalog=db_a9b03b_aevantho7;User Id=db_a9b03b_aevantho7_admin;Password=aexyz111";
        }

        public SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
