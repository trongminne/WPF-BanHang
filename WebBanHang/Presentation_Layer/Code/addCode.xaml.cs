
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WebBanHang.Business_Layer;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.CodeDataAccess;

namespace WebBanHang.Presentation_Layer.Code
{
    /// <summary>
    /// Interaction logic for addCode.xaml
    /// </summary>
    public partial class addCode : Window
    {
        private CodeBusinessLogic codeBusinessLogic;

        public addCode()
        {
            InitializeComponent();
            DatabaseConnection databaseConnection = new DatabaseConnection();
            codeBusinessLogic = new CodeBusinessLogic();
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


        private void AddCodeButton_Click(object sender, RoutedEventArgs e)
        {
            string codeName = CodeName.Text;
            string codeMa = CodeMa.Text;
            int codePrice = int.Parse(CodePrice.Text);

            if (string.IsNullOrWhiteSpace(codeName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Bạn chưa nhập đủ!");
                messageBox.ShowDialog();
                return;
            }

            if (codeBusinessLogic.IsCodeExists(codeName))
            {
                CustomMessageBox messageBox = new CustomMessageBox("Thẻ giảm giá này đã tồn tại trong hệ thống.");
                messageBox.ShowDialog();
                return;
            }

            bool success = codeBusinessLogic.AddCode(codeName, codeMa, codePrice);

            if (success)
            {
                CustomMessageBox successMessageBox = new CustomMessageBox("Thêm thành công!");

                successMessageBox.ShowDialog();

                if (successMessageBox.DialogResult == MessageBoxResult.Yes)
                {
                    Code code = new Code();
                    code.Show();
                    Close();
                }
                else
                {
                    CodeName.Text = string.Empty;
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
