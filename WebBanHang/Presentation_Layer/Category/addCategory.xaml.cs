
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WebBanHang.Business_Layer;
using WebBanHang.Data_Access_Layer;

namespace WebBanHang.Presentation_Layer.Category
{
    /// <summary>
    /// Interaction logic for addCategory.xaml
    /// </summary>
    public partial class addCategory : Window
    {
        private CategoryBusinessLogic categoryBusinessLogic;

        public addCategory()
        {
            InitializeComponent();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            categoryBusinessLogic = new CategoryBusinessLogic();
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


        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryName.Text;

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                messageBox.ShowDialog();
                return;
            }

            if (categoryBusinessLogic.IsCategoryExists(categoryName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Thương hiệu này đã tồn tại trong hệ thống.");
                messageBox.ShowDialog();
                return;
            }

            bool success = categoryBusinessLogic.AddCategory(categoryName);

            if (success)
            {
                CustomMessageBox successMessageBox = new CustomMessageBox("Thêm thành công!");

                successMessageBox.ShowDialog();

                if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                {
                    Category category = new Category();
                    category.Show();
                    Close();
                }
                else
                {
                    CategoryName.Text = string.Empty;
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
