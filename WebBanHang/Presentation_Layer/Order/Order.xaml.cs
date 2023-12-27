using System.Collections.ObjectModel;
using System.Windows;
using static WebBanHang.Data_Access_Layer.OrderDataAccess;
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

namespace WebBanHang.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private readonly OrderBusinessLogic orderBusinessLogic;
     
        public ObservableCollection<OrderDataAccess.Order> Orders { get; set; }
        public Order()
        {
            InitializeComponent();
            orderBusinessLogic = new OrderBusinessLogic();
            Orders = new ObservableCollection<OrderDataAccess.Order>();
            PageNumbers = new ObservableCollection<PageNumberItem>(); // Khởi tạo PageNumbers
            LoadOrdersFromDatabase();
            membersDataGrid.ItemsSource = Orders;

            // Gán giá trị vào nameTextBlock
            nameTextBlock.Text = StorageClass.FirstName + " " + StorageClass.LastName;

            // Đặt DataContext cho Brand
            this.DataContext = this;
            UpdatePageNumbers(); // Cập nhật danh sách số trang

        }


        public string LastName { get; set; }
        public string FirstName { get; set; }


        //Phân trang + xuất
        private const int PageSize = 10; // Số lượng người dùng hiển thị trên mỗi trang
        private int currentPage = 1; // Trang hiện tại

        private int totalPages; // Tổng số trang
        private int totalOrders; // Tổng số người dùng

        private ObservableCollection<WebBanHang.Data_Access_Layer.OrderDataAccess.Order> displayedOrders; // Danh sách người dùng hiển thị trên trang hiện tại

        //Xuất bảng order
        private void LoadOrdersFromDatabase()
        {

            Orders.Clear();
            var allOrders = orderBusinessLogic.GetOrders();
            totalOrders = allOrders.Count;

            totalPages = (int)Math.Ceiling((double)totalOrders / PageSize);

            // Xác định chỉ mục bắt đầu và chỉ mục kết thúc của người dùng được hiển thị trên trang hiện tại
            int startIndex = (currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, totalOrders) - 1;

            displayedOrders = new ObservableCollection<OrderDataAccess.Order>(allOrders.Skip(startIndex).Take(PageSize));


            int stt = startIndex + 1;
            foreach (var order in displayedOrders)
            {
                order.STT = stt++;
                Orders.Add(order);
            }


            customerCountTextBlock.Text = $"Tổng có " + totalOrders + " đơn hàng";

            UpdatePageNumbers(); // Cập nhật danh sách số trang


        }

        // Duyệt số lượng trang

        private void UpdatePageNumbers()
        {
            paginationOrder.Items.Clear(); // Xóa các nút trang cũ

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

                paginationOrder.Items.Add(button);
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
                LoadOrdersFromDatabase();
            }
        }


        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadOrdersFromDatabase();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadOrdersFromDatabase();
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
            var selectedItem = membersDataGrid.SelectedItem as OrderDataAccess.Order;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        OrderDataAccess orderDataAccess = new OrderDataAccess();
                        orderDataAccess.DeleteItemFromDatabase(selectedItem);
                        Orders.Remove(selectedItem);

                        CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công!");
                        successMessageBox.ShowDialog();

                        // Cập nhật lại số lượng đơn hàng
                        totalOrders = Orders.Count;
                        customerCountTextBlock.Text = $"Tổng có {totalOrders} đơn hàng";
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

           
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Order thành true
            foreach (var order in Orders)
            {
                order.IsSelected = true;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in membersDataGrid.Items)
            {
                var row = membersDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            foreach (var order in Orders)
            {
                selectedItems.Add(order);
            }
            isItemSelected = true;
        }

        private void headerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Order thành false
            foreach (OrderDataAccess.Order order in Orders)
            {
                order.IsSelected = false;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in membersDataGrid.Items)
            {
                var row = membersDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
        private List<OrderDataAccess.Order> selectedItems = new List<OrderDataAccess.Order>();
        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection;
            if (membersDataGrid.SelectedItems.Count > 0)
            {
                hasSelection = true;
            }
            else
            {
                hasSelection = false;
            }

            selectedItems.Clear();
            foreach (var selectedItem in membersDataGrid.SelectedItems.OfType<OrderDataAccess.Order>())
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
                        OrderDataAccess orderDataAccess = new OrderDataAccess();
                        var itemsToRemove = new List<OrderDataAccess.Order>();

                        // Kiểm tra trạng thái của từng mục đã chọn trong selectedItems
                        foreach (var selectedItem in selectedItems)
                        {
                            itemsToRemove.Add(selectedItem);
                        }

                        if (itemsToRemove.Count > 0)
                        {
                            foreach (var selectedItem in itemsToRemove)
                            {
                                orderDataAccess.DeleteItemFromDatabase(selectedItem);
                                Orders.Remove(selectedItem);
                            }

                            CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công");
                            successMessageBox.ShowDialog();

                            // Cập nhật lại số lượng đơn hàng
                            totalOrders = Orders.Count;
                            customerCountTextBlock.Text = $"Tổng có {totalOrders} đơn hàng";
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
            ICollectionView view = CollectionViewSource.GetDefaultView(membersDataGrid.ItemsSource);
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
                        var order = item as OrderDataAccess.Order;
                        if (order != null)
                        {
                            string keyword = FilterKeyword.ToLower();
                            return order.NameUser.ToLower().Contains(keyword);
                        }
                        return false;
                    };
                }
            }
        }

        // Chuyển tab
        private void btn_Brand(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Brand.Brand brand = new WebBanHang.Presentation_Layer.Brand.Brand();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            brand.Show();

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

        private void btn_User(object sender, RoutedEventArgs e)
        {
            Panel panel = new Panel();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            panel.Show();

        }

        // Xác nhận giao hàng
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = membersDataGrid.SelectedItem as OrderDataAccess.Order;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn giao hàng không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        OrderDataAccess orderDataAccess = new OrderDataAccess();
                        orderDataAccess.EditItemFromDatabase(selectedItem);


                        CustomMessageBox successMessageBox = new CustomMessageBox("Giao hàng thành công!");
                        successMessageBox.ShowDialog();

                        // load lại
                        Order order = new Order();
                        Close();
                        order.Show();

                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox errorMessageBox = new CustomMessageBox("Giao hàng thất bại!");
                    errorMessageBox.ShowDialog();
                }
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
