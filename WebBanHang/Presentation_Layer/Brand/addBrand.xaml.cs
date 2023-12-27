
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WebBanHang.Business_Layer;
using WebBanHang.Data_Access_Layer;

namespace WebBanHang.Presentation_Layer.Brand
{
    /// <summary>
    /// Interaction logic for addBrand.xaml
    /// </summary>
    public partial class addBrand : Window
    {
        private BrandBusinessLogic brandBusinessLogic;

        public addBrand()
        {
            InitializeComponent();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            brandBusinessLogic = new BrandBusinessLogic();
        }


        private void Border_MoseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Colse_MoseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void AddBrandButton_Click(object sender, RoutedEventArgs e)
        {
            string brandName = BrandName.Text;

            if (string.IsNullOrWhiteSpace(brandName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                messageBox.ShowDialog();
                return;
            }

            if (brandBusinessLogic.IsBrandExists(brandName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Thương hiệu này đã tồn tại trong hệ thống.");
                messageBox.ShowDialog();
                return;
            }

            bool success = brandBusinessLogic.AddBrand(brandName);

            if (success)
            {
                CustomMessageBox successMessageBox = new CustomMessageBox("Thêm thành công!");

                successMessageBox.ShowDialog();

                if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                {
                    Brand brand = new Brand();
                    brand.Show();
                    Close();
                }
                else
                {
                    BrandName.Text = string.Empty;
                }
            }
            else
            {
                CustomMessageBox errorMessageBox = new CustomMessageBox("Thêm thất bại!");
                errorMessageBox.ShowDialog();
            }
        }



    }
}
