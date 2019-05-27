using System;
using Windows.UI.Xaml.Data;

namespace Graighle.Triping.UWPClient.Utilities.Converters
{
    /// <summary>
    /// Booleanを反転する変換。
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }
    }
}
