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
using static WebBanHang.Data_Access_Layer.CategoryDataAccess;

namespace WebBanHang.Presentation_Layer.Category
{
    /// <summary>
    /// Interaction logic for editCategory.xaml
    /// </summary>
    public partial class editCategory : Window
    {
        private CategoryBusinessLogic categoryBusinessLogic = new CategoryBusinessLogic();

        public editCategory()
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
        public string CategoryId { get; set; }
        // Lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = EditCategoryName.Text;
            if (int.TryParse(EditCategoryId.Text, out int categoryId))
            {
                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                    messageBox.ShowDialog();
                    return;
                }

                if (categoryBusinessLogic == null)
                {
                    // Khởi tạo đối tượng categoryBusinessLogic nếu cần thiết
                    categoryBusinessLogic = new CategoryBusinessLogic();
                }

                if (categoryBusinessLogic.IsCategoryExists(categoryName) && categoryName != Name)
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Danh mục này đã tồn tại trong hệ thống.");
                    messageBox.ShowDialog();
                    return;
                }

                bool success = categoryBusinessLogic.EditCategory(categoryName, categoryId);

                if (success)
                {
                    CustomMessageBox successMessageBox = new CustomMessageBox("Sửa thành công!");

                    successMessageBox.ShowDialog();

                    if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        Category category = new Category();
                        category.Show();
                        Close();
                    }
                    else
                    {
                        EditCategoryName.Text = string.Empty;
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
