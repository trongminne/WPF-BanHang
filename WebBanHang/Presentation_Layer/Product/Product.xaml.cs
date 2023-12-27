using System.Collections.ObjectModel;
using System.Windows;
using static WebBanHang.Data_Access_Layer.ProductDataAccess;
using WebBanHang.Business_Layer;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Reflection;
using WebBanHang.Data_Access_Layer;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Generic;
using static WebBanHang.Presentation_Layer.LoginView;

namespace WebBanHang.Presentation_Layer.Product
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private readonly ProductBusinessLogic productBusinessLogic;
        public ObservableCollection<ProductDataAccess.Product> Products { get; set; }

        public Product()
        {
            InitializeComponent();
            productBusinessLogic = new ProductBusinessLogic();
            Products = new ObservableCollection<ProductDataAccess.Product>();

            PageNumbers = new ObservableCollection<PageNumberItem>(); // Khởi tạo PageNumbers
            LoadProductsFromDatabase();
            productsDataGrid.ItemsSource = Products;

            // Gán giá trị vào nameTextBlock
            nameTextBlock.Text = StorageClass.FirstName + " " + StorageClass.LastName;

            // Đặt DataContext cho Product
            this.DataContext = this;
            UpdatePageNumbers(); // Cập nhật danh sách số trang
                                 // Kiểm tra giá trị của DataContext


        }


        public string LastName { get; set; }
        public string FirstName { get; set; }

        //Phân trang + xuất
        private const int PageSize = 10; // Số lượng người dùng hiển thị trên mỗi trang
        private int currentPage = 1; // Trang hiện tại

        private int totalPages; // Tổng số trang
        private int totalProducts; // Tổng số người dùng

        private ObservableCollection<ProductDataAccess.Product> displayedProducts; // Danh sách người dùng hiển thị trên trang hiện tại


        //Xuất bảng product

        private void LoadProductsFromDatabase()
        {
            Products.Clear();
            var allProducts = productBusinessLogic.GetProducts();
            totalProducts = allProducts.Count;

            totalPages = (int)Math.Ceiling((double)totalProducts / PageSize);

            // Xác định chỉ mục bắt đầu và chỉ mục kết thúc của người dùng được hiển thị trên trang hiện tại
            int startIndex = (currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, totalProducts) - 1;

            displayedProducts = new ObservableCollection<ProductDataAccess.Product>(allProducts.Skip(startIndex).Take(PageSize));

            int stt = startIndex + 1;
            foreach (var product in displayedProducts)
            {
                product.STT = stt++;
                Products.Add(product);
            }


            customerCountTextBlock.Text = $"Tổng có {totalProducts} sản phẩm";

            UpdatePageNumbers(); // Cập nhật danh sách số trang
        }

        // Duyệt số lượng trang

        private void UpdatePageNumbers()
        {
            paginationProduct.Items.Clear(); // Xóa các nút trang cũ

            int startPage = Math.Max(currentPage - 1, 1); // Trang bắt đầu hiển thị
            int endPage = Math.Min(startPage + 2, totalPages); // Trang kết thúc hiển thị

            for (int i = startPage; i <= endPage; i++)
            {
                Button button = new Button();
                button.Style = (Style)FindResource("pagingButton");
                TextBlock textBlock = new TextBlock();
                textBlock.Text = i.ToString();
                button.Content = textBlock;
                button.Click += PaginationButton_Click; // Thêm xử lý sự kiện click cho nút trang

                if (i == currentPage)
                {
                    button.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x79, 0x50, 0xF2)); // Đặt background cho trang hiện tại
                    textBlock.Foreground = Brushes.White; // Đặt màu chữ trắng cho trang hiện tại

                }

                paginationProduct.Items.Add(button);
            }
        }

        public class PageNumberItem
        {
            public int Number { get; set; }

            public bool IsSelected { get; }

            public PageNumberItem(int number, bool isSelected)
            {
                Number = number;
                IsSelected = isSelected;
            }
        }
        private ObservableCollection<PageNumberItem> PageNumbers { get; set; } = new ObservableCollection<PageNumberItem>();


        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var textBlock = (TextBlock)button.Content; // Lấy đối tượng TextBlock từ nút trang
            int newPage = int.Parse(textBlock.Text); // Lấy giá trị số trang từ thuộc tính Text của TextBlock

            if (newPage != currentPage)
            {
                currentPage = newPage;
                LoadProductsFromDatabase();
            }
        }


        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadProductsFromDatabase();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadProductsFromDatabase();
            }
        }

        // end Phân trang + xuất


        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        // Xoá tài khoản
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = productsDataGrid.SelectedItem as ProductDataAccess.Product;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        DatabaseConnection databaseConnection = new DatabaseConnection();
                        ProductDataAccess productDataAccess = new ProductDataAccess(databaseConnection);
                        productDataAccess.DeleteItemFromDatabase(selectedItem);
                        Products.Remove(selectedItem);

                        CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công!");
                        successMessageBox.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox errorMessageBox = new CustomMessageBox("Xoá thất bại!");
                    errorMessageBox.ShowDialog();
                }
            }
        }

        // Xoá nhiều
        private void headerCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Product thành true
            foreach (ProductDataAccess.Product product in Products)
            {
                product.IsSelected = true;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in productsDataGrid.Items)
            {
                var row = productsDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null && checkBox != sender)
                    {
                        checkBox.IsChecked = true;
                    }
                }
            }

            // Cập nhật danh sách các mục đã chọn và trạng thái isItemSelected
            selectedItems.Clear();
            foreach (var product in Products)
            {
                selectedItems.Add(product);
            }
            isItemSelected = true;
        }

        private void headerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Product thành false
            foreach (ProductDataAccess.Product product in Products)
            {
                product.IsSelected = false;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in productsDataGrid.Items)
            {
                var row = productsDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = FindVisualChild<CheckBox>(row);
                    if (checkBox != null && checkBox != sender)
                    {
                        checkBox.IsChecked = false;
                    }
                }
            }

            // Cập nhật danh sách các mục đã chọn và trạng thái isItemSelected
            selectedItems.Clear();
            isItemSelected = false;
        }


        // Hàm hỗ trợ tìm kiếm điều khiển con trong một điều khiển cha
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T found)
                {
                    return found;
                }
                else
                {
                    var result = FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        private bool isItemSelected = false;
        private List<ProductDataAccess.Product> selectedItems = new List<ProductDataAccess.Product>();
        private void productsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection;
            if (productsDataGrid.SelectedItems.Count > 0)
            {
                hasSelection = true;
            }
            else
            {
                hasSelection = false;
            }

            selectedItems.Clear();
            foreach (var selectedItem in productsDataGrid.SelectedItems.OfType<ProductDataAccess.Product>())
            {
                selectedItems.Add(selectedItem);
            }

            isItemSelected = hasSelection;
        }

        private void RemoveButtonMulti_Click(object sender, RoutedEventArgs e)
        {
            if (isItemSelected == true)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        DatabaseConnection databaseConnection = new DatabaseConnection();
                        ProductDataAccess productDataAccess = new ProductDataAccess(databaseConnection);
                        var itemsToRemove = new List<ProductDataAccess.Product>();

                        // Kiểm tra trạng thái của từng mục đã chọn trong selectedItems
                        foreach (var selectedItem in selectedItems)
                        {
                            itemsToRemove.Add(selectedItem);
                        }

                        if (itemsToRemove.Count > 0)
                        {
                            foreach (var selectedItem in itemsToRemove)
                            {
                                productDataAccess.DeleteItemFromDatabase(selectedItem);
                                Products.Remove(selectedItem);
                            }

                            CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công");
                            successMessageBox.ShowDialog();

                            // Cập nhật lại số lượng khách hàng
                            totalProducts = Products.Count;
                            customerCountTextBlock.Text = $"Tổng có {totalProducts} sản phẩm";
                        }
                        else
                        {
                            CustomMessageBox infoMessageBox = new CustomMessageBox("Chưa có tài khoản nào được chọn");
                            infoMessageBox.ShowDialog();
                        }
                    }

                }
                catch (Exception ex)
                {
                    CustomMessageBox errorMessageBox = new CustomMessageBox("Xoá thất bại");
                    errorMessageBox.ShowDialog();
                }
            }
            else
            {
                CustomMessageBox infoMessageBox = new CustomMessageBox("Chưa có tài khoản nào được chọn");
                infoMessageBox.ShowDialog();
            }
        }


        // end xoá nhiều

        // Lọc tài khoản
        private string filterKeyword;
        public string FilterKeyword
        {
            get { return filterKeyword; }
            set
            {
                filterKeyword = value;
                ApplyFilter();
            }
        }
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterKeyword = txtFilter.Text;
        }
        private void ApplyFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(productsDataGrid.ItemsSource);
            if (view != null)
            {
                if (string.IsNullOrEmpty(FilterKeyword))
                {
                    view.Filter = null; // Bỏ qua bộ lọc nếu từ khóa trống
                }
                else
                {
                    view.Filter = item =>
                    {
                        // Thay thế property "TenTruong" bằng tên trường cần lọc
                        var product = item as ProductDataAccess.Product;
                        if (product != null)
                        {
                            string keyword = FilterKeyword.ToLower();
                            return product.Name.ToLower().Contains(keyword);
                        }
                        return false;
                    };
                }
            }
        }


        // Thêm

        private void btn_addProduct(object sender, RoutedEventArgs e)
        {
            addProduct addproduct = new addProduct();
            addproduct.Show();
            Close();
        }

        // Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (WebBanHang.Data_Access_Layer.ProductDataAccess.Product)productsDataGrid.SelectedItem;

            if (selectedItem != null && selectedItem.Name != null)
            {
                string selectedName = selectedItem.Name;
                int selectedId = selectedItem.Id;
                int selectedBrandId = selectedItem.BrandId;
                int selectedCategoryId = selectedItem.CategoryId;
                double selectedPrice = selectedItem.Price;
                double selectedPriceSale = selectedItem.PriceSale;
                int selectedQuantity = selectedItem.Quantity;

                editProduct editProduct = new editProduct();
                editProduct.EditProductName.Text = selectedName;
                editProduct.EditProductId.Text = selectedId.ToString(); // Truyền giá trị id vào thuộc tính ProductId
                editProduct.EditProductBrandId.Text = selectedBrandId.ToString();
                editProduct.EditProductCategoryId.Text = selectedCategoryId.ToString(); 
                editProduct.EditProductPrice.Text = selectedPrice.ToString();
                editProduct.EditProductSale.Text = selectedPriceSale.ToString();
                editProduct.EditProductQuantity.Text = selectedQuantity.ToString();

                // Truyền giá trị id đê checked
                editProduct.selectedProductName = selectedName; 
                editProduct.SelectedBrandId = selectedBrandId;
                editProduct.SelectedCategoryId = selectedCategoryId;
                Close();

                editProduct.Show();

            }
        }

        // Chuyển tab

        private void btn_User(object sender, RoutedEventArgs e)
        {
            Panel panel = new Panel();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            panel.Show();

        }

        private void btn_Category(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Category.Category category = new WebBanHang.Presentation_Layer.Category.Category();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            category.Show();

        }

        private void btn_Code(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Code.Code Code = new WebBanHang.Presentation_Layer.Code.Code();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Code.Show();

        }
        private void btn_Brand(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Brand.Brand Brand = new WebBanHang.Presentation_Layer.Brand.Brand();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Brand.Show();

        }

        private void btn_Home(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Order Order = new WebBanHang.Presentation_Layer.Order();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Order.Show();

        }

        private void btn_logout(object sender, RoutedEventArgs e)
        {
            LoginView logout = new LoginView();
            Close();
            logout.Show();

        }
    }
}
