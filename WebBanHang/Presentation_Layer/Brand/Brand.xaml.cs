using System.Collections.ObjectModel;
using System.Windows;
using static WebBanHang.Data_Access_Layer.BrandDataAccess;
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

namespace WebBanHang.Presentation_Layer.Brand
{
    /// <summary>
    /// Interaction logic for Brand.xaml
    /// </summary>
    public partial class Brand : Window
    {
        private readonly BrandBusinessLogic brandBusinessLogic;
        public ObservableCollection<BrandDataAccess.Brand> Brands { get; set; }

        public Brand()
        {
            InitializeComponent();
            brandBusinessLogic = new BrandBusinessLogic();
            Brands = new ObservableCollection<BrandDataAccess.Brand>();

            PageNumbers = new ObservableCollection<PageNumberItem>(); // Khởi tạo PageNumbers
            LoadBrandsFromDatabase();
            brandsDataGrid.ItemsSource = Brands;

            // Gán giá trị vào nameTextBlock
            nameTextBlock.Text = StorageClass.FirstName + " " + StorageClass.LastName;

            // Đặt DataContext cho Brand
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
        private int totalBrands; // Tổng số người dùng

        private ObservableCollection<BrandDataAccess.Brand> displayedBrands; // Danh sách người dùng hiển thị trên trang hiện tại


        //Xuất bảng brand

        private void LoadBrandsFromDatabase()
        {
            Brands.Clear();
            var allBrands = brandBusinessLogic.GetBrands();
            totalBrands = allBrands.Count;

            totalPages = (int)Math.Ceiling((double)totalBrands / PageSize);

            // Xác định chỉ mục bắt đầu và chỉ mục kết thúc của người dùng được hiển thị trên trang hiện tại
            int startIndex = (currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, totalBrands) - 1;

            displayedBrands = new ObservableCollection<BrandDataAccess.Brand>(allBrands.Skip(startIndex).Take(PageSize));

            int stt = startIndex + 1;
            foreach (var brand in displayedBrands)
            {
                brand.STT = stt++;
                Brands.Add(brand);
            }


            customerCountTextBlock.Text = $"Tổng có {totalBrands} thương hiệu";

            UpdatePageNumbers(); // Cập nhật danh sách số trang
        }

        // Duyệt số lượng trang

        private void UpdatePageNumbers()
        {
            paginationBrand.Items.Clear(); // Xóa các nút trang cũ

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

                paginationBrand.Items.Add(button);
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
                LoadBrandsFromDatabase();
            }
        }


        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadBrandsFromDatabase();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadBrandsFromDatabase();
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
            var selectedItem = brandsDataGrid.SelectedItem as BrandDataAccess.Brand;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        DatabaseConnection databaseConnection = new DatabaseConnection();
                        BrandDataAccess brandDataAccess = new BrandDataAccess(databaseConnection);

                        brandDataAccess.DeleteItemFromDatabase(selectedItem);
                        Brands.Remove(selectedItem);

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
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Brand thành true
            foreach (BrandDataAccess.Brand brand in Brands)
            {
                brand.IsSelected = true;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in brandsDataGrid.Items)
            {
                var row = brandsDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            foreach (var brand in Brands)
            {
                selectedItems.Add(brand);
            }
            isItemSelected = true;
        }

        private void headerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Brand thành false
            foreach (BrandDataAccess.Brand brand in Brands)
            {
                brand.IsSelected = false;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in brandsDataGrid.Items)
            {
                var row = brandsDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
        private List<BrandDataAccess.Brand> selectedItems = new List<BrandDataAccess.Brand>();
        private void brandsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection;
            if (brandsDataGrid.SelectedItems.Count > 0)
            {
                hasSelection = true;
            }
            else
            {
                hasSelection = false;
            }

            selectedItems.Clear();
            foreach (var selectedItem in brandsDataGrid.SelectedItems.OfType<BrandDataAccess.Brand>())
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
                        BrandDataAccess brandDataAccess = new BrandDataAccess(databaseConnection);

                        var itemsToRemove = new List<BrandDataAccess.Brand>();

                        // Kiểm tra trạng thái của từng mục đã chọn trong selectedItems
                        foreach (var selectedItem in selectedItems)
                        {
                            itemsToRemove.Add(selectedItem);
                        }

                        if (itemsToRemove.Count > 0)
                        {
                            foreach (var selectedItem in itemsToRemove)
                            {
                                brandDataAccess.DeleteItemFromDatabase(selectedItem);
                                Brands.Remove(selectedItem);
                            }

                            CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công");
                            successMessageBox.ShowDialog();

                            // Cập nhật lại số lượng khách hàng
                            totalBrands = Brands.Count;
                            customerCountTextBlock.Text = $"Tổng có {totalBrands} thương hiệu";
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
            ICollectionView view = CollectionViewSource.GetDefaultView(brandsDataGrid.ItemsSource);
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
                        var brand = item as BrandDataAccess.Brand;
                        if (brand != null)
                        {
                            string keyword = FilterKeyword.ToLower();
                            return brand.Name.ToLower().Contains(keyword);
                        }
                        return false;
                    };
                }
            }
        }

       
        // Thêm

        private void btn_addBrand(object sender, RoutedEventArgs e)
        {
            addBrand addbrand = new addBrand();
            addbrand.Show();
            Close();
        }

        // Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (WebBanHang.Data_Access_Layer.BrandDataAccess.Brand)brandsDataGrid.SelectedItem;

            if (selectedItem != null && selectedItem.Name != null)
            {
                string selectedName = selectedItem.Name;
                int selectedId = selectedItem.Id;

                editBrand editBrand = new editBrand();

                // truyền vào xaml
                editBrand.EditBrandName.Text = selectedName;
                editBrand.EditBrandId.Text = selectedId.ToString(); // Truyền giá trị id vào thuộc tính BrandId

                editBrand.Name = selectedName; // Truyền name cs sang để kiểm tra

                Close();

                editBrand.Show();

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

        private void btn_Product(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Product.Product Product = new WebBanHang.Presentation_Layer.Product.Product();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Product.Show();

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
