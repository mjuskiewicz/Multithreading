using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfWaitingIndicatorOnTasks.Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class BooleanConverter : IValueConverter
    {
        public object True { get; set; }
        public object False { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valueAsBoolaen;
            if (Boolean.TryParse(value.ToString(), out valueAsBoolaen) && valueAsBoolaen)
            {
                return True;
            }
            return False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
