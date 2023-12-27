using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WebBanHang.Presentation_Layer.Convert
{
    public class RandomColorConverter : IValueConverter
    {
        private static readonly Random random = new Random();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(RandomColor());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color RandomColor()
        {
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            return Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
        }
    }
}
