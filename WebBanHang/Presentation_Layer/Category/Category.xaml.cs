using System.Collections.ObjectModel;
using System.Windows;
using static WebBanHang.Data_Access_Layer.CategoryDataAccess;
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

namespace WebBanHang.Presentation_Layer.Category
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : Window
    {
        private readonly CategoryBusinessLogic categoryBusinessLogic;
        public ObservableCollection<CategoryDataAccess.Category> Categorys { get; set; }

        public Category()
        {
            InitializeComponent();
            categoryBusinessLogic = new CategoryBusinessLogic();
            Categorys = new ObservableCollection<CategoryDataAccess.Category>();

            PageNumbers = new ObservableCollection<PageNumberItem>(); // Khởi tạo PageNumbers
            LoadCategorysFromDatabase();
            categorysDataGrid.ItemsSource = Categorys;

            // Gán giá trị vào nameTextBlock
            nameTextBlock.Text = StorageClass.FirstName + " " + StorageClass.LastName;

            // Đặt DataContext cho Category
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
        private int totalCategorys; // Tổng số người dùng

        private ObservableCollection<CategoryDataAccess.Category> displayedCategorys; // Danh sách người dùng hiển thị trên trang hiện tại


        //Xuất bảng category

        private void LoadCategorysFromDatabase()
        {
            Categorys.Clear();
            var allCategorys = categoryBusinessLogic.GetCategorys();
            totalCategorys = allCategorys.Count;

            totalPages = (int)Math.Ceiling((double)totalCategorys / PageSize);

            // Xác định chỉ mục bắt đầu và chỉ mục kết thúc của người dùng được hiển thị trên trang hiện tại
            int startIndex = (currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, totalCategorys) - 1;

            displayedCategorys = new ObservableCollection<CategoryDataAccess.Category>(allCategorys.Skip(startIndex).Take(PageSize));

            int stt = startIndex + 1;
            foreach (var category in displayedCategorys)
            {
                category.STT = stt++;
                Categorys.Add(category);
            }


            customerCountTextBlock.Text = $"Tổng có {totalCategorys} thương hiệu";

            UpdatePageNumbers(); // Cập nhật danh sách số trang
        }

        // Duyệt số lượng trang

        private void UpdatePageNumbers()
        {
            paginationCategory.Items.Clear(); // Xóa các nút trang cũ

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

                paginationCategory.Items.Add(button);
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
                LoadCategorysFromDatabase();
            }
        }


        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCategorysFromDatabase();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCategorysFromDatabase();
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
            var selectedItem = categorysDataGrid.SelectedItem as CategoryDataAccess.Category;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        DatabaseConnection databaseConnection = new DatabaseConnection();
                        CategoryDataAccess categoryDataAccess = new CategoryDataAccess(databaseConnection);

                        categoryDataAccess.DeleteItemFromDatabase(selectedItem);
                        Categorys.Remove(selectedItem);

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
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Category thành true
            foreach (CategoryDataAccess.Category category in Categorys)
            {
                category.IsSelected = true;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in categorysDataGrid.Items)
            {
                var row = categorysDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            foreach (var category in Categorys)
            {
                selectedItems.Add(category);
            }
            isItemSelected = true;
        }

        private void headerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Category thành false
            foreach (CategoryDataAccess.Category category in Categorys)
            {
                category.IsSelected = false;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in categorysDataGrid.Items)
            {
                var row = categorysDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
        private List<CategoryDataAccess.Category> selectedItems = new List<CategoryDataAccess.Category>();
        private void categorysDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection;
            if (categorysDataGrid.SelectedItems.Count > 0)
            {
                hasSelection = true;
            }
            else
            {
                hasSelection = false;
            }

            selectedItems.Clear();
            foreach (var selectedItem in categorysDataGrid.SelectedItems.OfType<CategoryDataAccess.Category>())
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
                        CategoryDataAccess categoryDataAccess = new CategoryDataAccess(databaseConnection);
                        var itemsToRemove = new List<CategoryDataAccess.Category>();

                        // Kiểm tra trạng thái của từng mục đã chọn trong selectedItems
                        foreach (var selectedItem in selectedItems)
                        {
                            itemsToRemove.Add(selectedItem);
                        }

                        if (itemsToRemove.Count > 0)
                        {
                            foreach (var selectedItem in itemsToRemove)
                            {
                                categoryDataAccess.DeleteItemFromDatabase(selectedItem);
                                Categorys.Remove(selectedItem);
                            }

                            CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công");
                            successMessageBox.ShowDialog();

                            // Cập nhật lại số lượng khách hàng
                            totalCategorys = Categorys.Count;
                            customerCountTextBlock.Text = $"Tổng có {totalCategorys} thương hiệu";
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
            ICollectionView view = CollectionViewSource.GetDefaultView(categorysDataGrid.ItemsSource);
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
                        var category = item as CategoryDataAccess.Category;
                        if (category != null)
                        {
                            string keyword = FilterKeyword.ToLower();
                            return category.Name.ToLower().Contains(keyword);
                        }
                        return false;
                    };
                }
            }
        }

        // Chuyển tab

        private void btn_User(object sender, RoutedEventArgs e)
        {
            Panel panel = new Panel();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            panel.Show();

        }

        private void btn_Brand(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Brand.Brand Brand = new WebBanHang.Presentation_Layer.Brand.Brand();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Brand.Show();

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

        // Thêm

        private void btn_addCategory(object sender, RoutedEventArgs e)
        {
            addCategory addcategory = new addCategory();
            addcategory.Show();
            Close();
        }

        // Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (WebBanHang.Data_Access_Layer.CategoryDataAccess.Category)categorysDataGrid.SelectedItem;

            if (selectedItem != null && selectedItem.Name != null)
            {
                string selectedName = selectedItem.Name;
                int selectedId = selectedItem.Id;

                editCategory editCategory = new editCategory();
                editCategory.EditCategoryName.Text = selectedName; // truyền xaml
                editCategory.Name = selectedName; // Truyền sang cs
                editCategory.EditCategoryId.Text = selectedId.ToString(); // Truyền giá trị id vào thuộc tính CategoryId
                Close();

                editCategory.Show();
            }
        }

        private void btn_logout(object sender, RoutedEventArgs e)
        {
            LoginView logout = new LoginView();
            Close();
            logout.Show();

        }
    }
}
