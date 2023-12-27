using System.Collections.ObjectModel;
using System.Windows;
using static WebBanHang.Data_Access_Layer.CodeDataAccess;
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

namespace WebBanHang.Presentation_Layer.Code
{
    /// <summary>
    /// Interaction logic for Code.xaml
    /// </summary>
    public partial class Code : Window
    {
        private readonly CodeBusinessLogic codeBusinessLogic;
        public ObservableCollection<CodeDataAccess.Code> Codes { get; set; }

        public Code()
        {
            InitializeComponent();
            codeBusinessLogic = new CodeBusinessLogic();
            Codes = new ObservableCollection<CodeDataAccess.Code>();

            PageNumbers = new ObservableCollection<PageNumberItem>(); // Khởi tạo PageNumbers
            LoadCodesFromDatabase();
            codesDataGrid.ItemsSource = Codes;

            // Gán giá trị vào nameTextBlock
            nameTextBlock.Text = StorageClass.FirstName + " " + StorageClass.LastName;

            // Đặt DataContext cho Code
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
        private int totalCodes; // Tổng số người dùng

        private ObservableCollection<CodeDataAccess.Code> displayedCodes; // Danh sách người dùng hiển thị trên trang hiện tại


        //Xuất bảng code

        private void LoadCodesFromDatabase()
        {
            Codes.Clear();
            var allCodes = codeBusinessLogic.GetCodes();
            totalCodes = allCodes.Count;

            totalPages = (int)Math.Ceiling((double)totalCodes / PageSize);

            // Xác định chỉ mục bắt đầu và chỉ mục kết thúc của người dùng được hiển thị trên trang hiện tại
            int startIndex = (currentPage - 1) * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, totalCodes) - 1;

            displayedCodes = new ObservableCollection<CodeDataAccess.Code>(allCodes.Skip(startIndex).Take(PageSize));

            int stt = startIndex + 1;
            foreach (var code in displayedCodes)
            {
                code.STT = stt++;
                Codes.Add(code);
            }


            customerCountTextBlock.Text = $"Tổng có {totalCodes} thẻ giảm giá";

            UpdatePageNumbers(); // Cập nhật danh sách số trang
        }

        // Duyệt số lượng trang

        private void UpdatePageNumbers()
        {
            paginationCode.Items.Clear(); // Xóa các nút trang cũ

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

                paginationCode.Items.Add(button);
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
                LoadCodesFromDatabase();
            }
        }


        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCodesFromDatabase();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCodesFromDatabase();
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
            var selectedItem = codesDataGrid.SelectedItem as CodeDataAccess.Code;
            if (selectedItem != null)
            {
                try
                {
                    CustomMessageBox confirmMessageBox = new CustomMessageBox("Bạn có chắc muốn xoá không?");
                    confirmMessageBox.ShowDialog();

                    if (confirmMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        DatabaseConnection databaseConnection = new DatabaseConnection();
                        CodeDataAccess codeDataAccess = new CodeDataAccess(databaseConnection);
                        codeDataAccess.DeleteItemFromDatabase(selectedItem);
                        Codes.Remove(selectedItem);

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
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Code thành true
            foreach (CodeDataAccess.Code code in Codes)
            {
                code.IsSelected = true;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in codesDataGrid.Items)
            {
                var row = codesDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
            foreach (var code in Codes)
            {
                selectedItems.Add(code);
            }
            isItemSelected = true;
        }

        private void headerCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đặt thuộc tính IsSelected của tất cả các đối tượng Code thành false
            foreach (CodeDataAccess.Code code in Codes)
            {
                code.IsSelected = false;
            }

            // Cập nhật hiển thị của checkbox
            foreach (var item in codesDataGrid.Items)
            {
                var row = codesDataGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
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
        private List<CodeDataAccess.Code> selectedItems = new List<CodeDataAccess.Code>();
        private void codesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection;
            if (codesDataGrid.SelectedItems.Count > 0)
            {
                hasSelection = true;
            }
            else
            {
                hasSelection = false;
            }

            selectedItems.Clear();
            foreach (var selectedItem in codesDataGrid.SelectedItems.OfType<CodeDataAccess.Code>())
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
                        CodeDataAccess codeDataAccess = new CodeDataAccess(databaseConnection);
                        var itemsToRemove = new List<CodeDataAccess.Code>();

                        // Kiểm tra trạng thái của từng mục đã chọn trong selectedItems
                        foreach (var selectedItem in selectedItems)
                        {
                            itemsToRemove.Add(selectedItem);
                        }

                        if (itemsToRemove.Count > 0)
                        {
                            foreach (var selectedItem in itemsToRemove)
                            {
                                codeDataAccess.DeleteItemFromDatabase(selectedItem);
                                Codes.Remove(selectedItem);
                            }

                            CustomMessageBox successMessageBox = new CustomMessageBox("Xoá thành công");
                            successMessageBox.ShowDialog();

                            // Cập nhật lại số lượng khách hàng
                            totalCodes = Codes.Count;
                            customerCountTextBlock.Text = $"Tổng có {totalCodes} thẻ giảm giá";
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
            ICollectionView view = CollectionViewSource.GetDefaultView(codesDataGrid.ItemsSource);
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
                        var code = item as CodeDataAccess.Code;
                        if (code != null)
                        {
                            string keyword = FilterKeyword.ToLower();
                            return code.Name.ToLower().Contains(keyword);
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

        private void btn_Category(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Category.Category category = new WebBanHang.Presentation_Layer.Category.Category();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            category.Show();

        }

        private void btn_Brand(object sender, RoutedEventArgs e)
        {
            WebBanHang.Presentation_Layer.Brand.Brand Brand = new WebBanHang.Presentation_Layer.Brand.Brand();
            Close(); // Đóng cửa sổ hiện tại nếu cần
            Brand.Show();

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

        private void btn_addCode(object sender, RoutedEventArgs e)
        {
            addCode addcode = new addCode();
            addcode.Show();
            Close();
        }


        // Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (WebBanHang.Data_Access_Layer.CodeDataAccess.Code)codesDataGrid.SelectedItem;

            if (selectedItem != null && selectedItem.Name != null)
            {
                string selectedName = selectedItem.Name;
                int selectedId = selectedItem.Id;
                string selectedCode = selectedItem.MaCode;
                int selectedPrice = selectedItem.Price;

                editCode editCode = new editCode();
                editCode.EditCodeName.Text = selectedName; // Truyền sang xaml
                editCode.Name = selectedName; // Truyền sang cs
                editCode.EditCodeId.Text = selectedId.ToString();  // Truyền giá trị id vào thuộc tính CodeId
                editCode.EditCodeMa.Text = selectedCode;
                editCode.EditCodePrice.Text = selectedPrice.ToString();

                editCode.Show();
                Close();
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
