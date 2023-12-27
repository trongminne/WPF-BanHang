using System.Security.Cryptography;
using System.Text;
using WebBanHang.Data_Access_Layer; // Thay thế tên namespace tương ứng nếu cần thiết

namespace WebBanHang.Business_Layer
{
    public class LoginManager
    {
        private UserDataAccess userDataAccess = new UserDataAccess(); // Tạo đối tượng tìm kiếm user

        public bool Login(string email, string password)
        {
            string hashedPassword = GetMD5Hash(password);
            return userDataAccess.CheckUser(email, hashedPassword); // kiểm tra có user nhập vào không
        }
        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
