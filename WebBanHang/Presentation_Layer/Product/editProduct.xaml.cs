using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.ProductDataAccess;

namespace WebBanHang.Presentation_Layer.Product
{
    /// <summary>
    /// Interaction logic for editProduct.xaml
    /// </summary>
    public partial class editProduct : Window
    {
        private ProductBusinessLogic productBusinessLogic = new ProductBusinessLogic();
        public ObservableCollection<CategoryDataAccess.Category> Categories { get; set; }
        public ObservableCollection<BrandDataAccess.Brand> Brands { get; set; }

        // lấy giá trị id đê checked
        public int SelectedBrandId { get; set; }
        public int SelectedCategoryId { get; set; }

        public string selectedProductName { get; set; }


        public editProduct()
        {
            InitializeComponent();

            var databaseConnection = new DatabaseConnection();

            var categoryDataAccess = new CategoryDataAccess(databaseConnection);
            Categories = new ObservableCollection<CategoryDataAccess.Category>(categoryDataAccess.GetCategorys());

            var brandDataAccess = new BrandDataAccess(databaseConnection);
            Brands = new ObservableCollection<BrandDataAccess.Brand>(brandDataAccess.GetBrands());


            // Khởi tạo biến productBusinessLogic
            productBusinessLogic = new ProductBusinessLogic();


            // Gán DataContext của giao diện cho chính đối tượng addProduct
            DataContext = this;
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


        // Lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = EditProductName.Text;
            int categoryId = ((CategoryDataAccess.Category)EditProductCategoryId.SelectedItem)?.Id ?? 0;
            int brandId = ((BrandDataAccess.Brand)EditProductBrandId.SelectedItem)?.Id ?? 0;
            double price = double.Parse(EditProductPrice.Text);
            double priceSale = double.Parse(EditProductSale.Text);
            int quantity = int.Parse(EditProductQuantity.Text);

            if (int.TryParse(EditProductId.Text, out int productId))
            {

                if (string.IsNullOrWhiteSpace(productName))
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                    messageBox.ShowDialog();
                    return;
                }



                if (productBusinessLogic == null)
                {
                    // Khởi tạo đối tượng productBusinessLogic nếu cần thiết
                    productBusinessLogic = new ProductBusinessLogic();
                }

                if (productName != selectedProductName && productBusinessLogic.IsProductExists(productName))
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Sản phẩm này đã tồn tại trong hệ thống.");
                    messageBox.ShowDialog();
                    return;
                }

                bool success = productBusinessLogic.EditProduct(productName, productId, categoryId, brandId, price, priceSale, quantity);

                if (success)
                {
                    CustomMessageBox successMessageBox = new CustomMessageBox("Sửa thành công!");

                    successMessageBox.ShowDialog();

                    if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        Product product = new Product();
                        product.Show();
                        Close();
                    }
                    else
                    {
                        EditProductName.Text = string.Empty;
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
