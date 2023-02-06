using ForPeople.Extension;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ForPeople.Converter
{
    /// <summary>
    /// Конвертер перечисления в строку.
    /// </summary>
    public class EnumToStringConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return enumValue.GetDisplayName();
            }

            return string.Empty;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
