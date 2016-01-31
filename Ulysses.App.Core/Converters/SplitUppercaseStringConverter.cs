using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ulysses.App.Core.Converters
{
    public class SplitUppercaseStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof (string))
            {
                return value;
            }

            var text = (string)value;
            return string.Concat(text.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}