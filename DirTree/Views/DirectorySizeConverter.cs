using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirTree.Views
{
    internal class DirectorySizeConverter : IValueConverter
    {
        const double GB_PER_BYTE = 1000000000;
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;

            else
            {
                double gb = System.Convert.ToDouble(value) / GB_PER_BYTE;
                return $"({gb:F4} GB)";
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
