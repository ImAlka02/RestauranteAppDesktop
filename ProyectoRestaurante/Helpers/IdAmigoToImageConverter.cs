using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ProyectoRestaurante.Helpers
{
    public class IdAmigoToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rutaArchivo = "";
            if(value is int)
            {
                int idImg = (int)value;
                rutaArchivo = $"{AppDomain.CurrentDomain.BaseDirectory}imagenes\\{idImg}.jpg";
            }
            else
                rutaArchivo = (string)value;

            BitmapImage? b = null;
            if (File.Exists(rutaArchivo))
            {
                b = new BitmapImage();
                b.BeginInit();
                b.StreamSource = new FileStream(rutaArchivo, FileMode.Open);
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.EndInit();
                b.StreamSource.Dispose();
            }
            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
