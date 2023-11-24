using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverters
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRanning = (bool)value;
            
            if (isRanning)
            {
                return "Currently ranning";
            }

            return "Currently not rainning";
        }

        /// <summary>
        /// This is not going to be called
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
