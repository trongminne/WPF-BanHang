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
using static WebBanHang.Data_Access_Layer.CodeDataAccess;

namespace WebBanHang.Presentation_Layer.Code
{
    /// <summary>
    /// Interaction logic for editCode.xaml
    /// </summary>
    public partial class editCode : Window
    {
        private CodeBusinessLogic codeBusinessLogic = new CodeBusinessLogic();

        public editCode()
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
        public string CodeId { get; set; }
        // Lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string codeName = EditCodeName.Text;
            string codeMa = EditCodeMa.Text;
            int codePrice = int.Parse(EditCodePrice.Text);

            if (int.TryParse(EditCodeId.Text, out int codeId))
            {
                if (string.IsNullOrWhiteSpace(codeName))
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập gì!");
                    messageBox.ShowDialog();
                    return;
                }

                if (codeBusinessLogic == null)
                {
                    // Khởi tạo đối tượng codeBusinessLogic nếu cần thiết
                    codeBusinessLogic = new CodeBusinessLogic();
                }

                if (codeBusinessLogic.IsCodeExists(codeName) && codeName != Name)
                {
                    CustomMessageBox messageBox = new CustomMessageBox("Thẻ giảm giá này đã tồn tại trong hệ thống.");
                    messageBox.ShowDialog();
                    return;
                }

                bool success = codeBusinessLogic.EditCode(codeName, codeId, codeMa, codePrice);

                if (success)
                {
                    CustomMessageBox successMessageBox = new CustomMessageBox("Sửa thành công!");

                    successMessageBox.ShowDialog();

                    if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                    {
                        Code code = new Code();
                        code.Show();
                        Close();
                    }
                    else
                    {
                        EditCodeName.Text = string.Empty;
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
