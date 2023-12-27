
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

namespace WebBanHang.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        // Add a MessageText property
        public string MessageText { get; set; }

        // Add a DialogResult property
        public MessageBoxResult DialogResult { get; private set; }

        public CustomMessageBox(string messageText)
        {
            InitializeComponent();
            DataContext = this; // Set the DataContext to itself
            MessageText = messageText;
        }

        // Add the event handlers for the Yes and No buttons
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.Yes;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = MessageBoxResult.No;
            this.Close();
        }
    }
}