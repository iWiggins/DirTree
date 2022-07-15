using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace DirTree.Views
{
    public class DirectoryNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;

            else return $"{value}:";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
