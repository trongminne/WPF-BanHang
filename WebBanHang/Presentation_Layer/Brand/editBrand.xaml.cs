using System;
using System.Collections.Generic;
using System.Linq;
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
using WebBanHang.Business_Layer;
using static WebBanHang.Data_Access_Layer.BrandDataAccess;

namespace WebBanHang.Presentation_Layer.Brand
{
    /// <summary>
    /// Interaction logic for editBrand.xaml
    /// </summary>
    public partial class editBrand : Window
    {
        private BrandBusinessLogic brandBusinessLogic = new BrandBusinessLogic();

        public editBrand()
        {
            InitializeComponent();
        }

        private void Border_MoseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Colse_MoseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public string Name { get; set; }
        public string BrandId { get; set; }
        // Lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string brandName = EditBrandName.Text;
            if (int.TryParse(EditBrandId.Text, out int brandId))
            {
                if (string.IsNullOrWhiteSpace(brandName))
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                    messageBox.ShowDialog();
                    return;
                }

                if (brandBusinessLogic == null)
                {
                    // Khởi tạo đối tượng brandBusinessLogic nếu cần thiết
                    brandBusinessLogic = new BrandBusinessLogic();
                }

                if (brandBusinessLogic.IsBrandExists(brandName) && brandName != Name)
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Thương hiệu này đã tồn tại trong hệ thống.");
                    messageBox.ShowDialog();
                    return;
                }

                bool success = brandBusinessLogic.EditBrand(brandName, brandId);

                if (success)
                {
                    CustomMessageBox successMessageBox = new CustomMessageBox("Sửa thành công!");

                    successMessageBox.ShowDialog();

                    if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        Brand brand = new Brand();
                        brand.Show();
                        Close();
                    }
                    else
                    {
                        EditBrandName.Text = string.Empty;
                    }
                }
                else
                {
                    CustomMessageBox errorMessageBox = new CustomMessageBox("Sửa thất bại!");
                    errorMessageBox.ShowDialog();
                }
            }
        }


    }
}
