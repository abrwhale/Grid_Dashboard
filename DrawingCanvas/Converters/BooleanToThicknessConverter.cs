using System;
using System.Globalization;
using System.Windows.Data;

namespace DrawingCanvas.Converters
{
    public class BooleanToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolVal)
            {
                return boolVal ? 4.0 : 2.0; // 선택 시 4, 미선택 시 2
            }
            return 2.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
