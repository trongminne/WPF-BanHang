using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebBanHang.Business_Layer; // Thay thế tên namespace tương ứng nếu cần thiết
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.UserDataAccess;

namespace WebBanHang.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text;
            string password = txtPass.Password;

            // Tạo một đối tượng LoginManager
            var loginManager = new LoginManager();

            // Gọi phương thức Login từ LoginManager để kiểm tra thông tin đăng nhập
            bool isLoggedIn = loginManager.Login(email, password);
            if (isLoggedIn)
            {
                CustomMessageBox successMessageBox = new CustomMessageBox("Đăng nhập thành công!");
                successMessageBox.ShowDialog();

                // Lấy thông tin người dùng
        
                UserDataAccess userDataAccess = new UserDataAccess();
                UserInfo userInfo = userDataAccess.GetUserInfo(email);

                if (userInfo != null)
                {
                    StorageClass.LastName = userInfo.LastName;
                    StorageClass.FirstName = userInfo.FirstName;
                }

                // Truyền thông tin sang các tab khác

                // truyền sang panel
                Order order = new Order();
                order.LastName = StorageClass.LastName;
                order.FirstName = StorageClass.FirstName;
                order.Show();
                Close(); // Đóng cửa sổ hiện tại nếu cần

                // Truyền sang brand
                WebBanHang.Presentation_Layer.Brand.Brand brand = new WebBanHang.Presentation_Layer.Brand.Brand();

                brand.LastName = StorageClass.LastName;
                brand.FirstName = StorageClass.FirstName;
                
            }

            else
            {
                CustomMessageBox errorMessageBox = new CustomMessageBox("Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin đăng nhập!");
                errorMessageBox.ShowDialog();
            }

        }

        public static class StorageClass
        {
            public static string FirstName { get; set; }
            public static string LastName { get; set; }
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
