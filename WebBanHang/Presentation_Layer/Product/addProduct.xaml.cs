
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WebBanHang.Business_Layer;
using WebBanHang.Data_Access_Layer;

namespace WebBanHang.Presentation_Layer.Product
{
    /// <summary>
    /// Interaction logic for addProduct.xaml
    /// </summary>
    public partial class addProduct : Window
    {
        private ProductBusinessLogic productBusinessLogic;

        public ObservableCollection<CategoryDataAccess.Category> Categories { get; set; }
        public ObservableCollection<BrandDataAccess.Brand> Brands { get; set; }



        public addProduct()
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


        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductName.Text;
            int categoryId = ((CategoryDataAccess.Category)ProductCategoryComboBox.SelectedItem)?.Id ?? 0;
            int brandId = ((BrandDataAccess.Brand)ProductBrandComboBox.SelectedItem)?.Id ?? 0;
            double price = double.Parse(ProductPrice.Text);
            double priceSale = double.Parse(ProductSale.Text);
            int quantity = int.Parse(ProductQuantity.Text);


            if (string.IsNullOrWhiteSpace(productName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                messageBox.ShowDialog();
                return;
            }

            if (productBusinessLogic.IsProductExists(productName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Thương hiệu này đã tồn tại trong hệ thống.");
                messageBox.ShowDialog();
                return;
            }

            bool success = this.productBusinessLogic.AddProduct(productName, categoryId, brandId, price, priceSale, quantity);

            if (success)
            {
                CustomMessageBox successMessageBox = new CustomMessageBox("Thêm thành công!");
                successMessageBox.ShowDialog();

                if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                {
                    Product product = new Product();
                    product.Show();
                    Close();
                }
                else
                {
                    ProductName.Text = string.Empty;
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
