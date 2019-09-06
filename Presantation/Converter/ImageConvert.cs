using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Presantation.Converter
{
    public class ImageConvert : IValueConverter
    {
      
            public readonly string defaultImage =Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Presantation\\image\\profile.png";
        /// <summary>
        /// Gelen Datayı Convert eder
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                
                var byteBuffer = File.ReadAllBytes(defaultImage);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(byteBuffer);
                bitmapImage.EndInit();
                return bitmapImage;
            }
            else
            {
                string base64Image = value.ToString();
                var byteBuffer = System.Convert.FromBase64String(base64Image);
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(byteBuffer);
                bitmapImage.EndInit();
                return bitmapImage;

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
