using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheImageComparer.UI.Converters;
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (parameter is not null)
            boolValue = !boolValue;
        if (boolValue)
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility && visibility == Visibility.Visible && parameter is null)
            return true;
        return false;
    }
}
