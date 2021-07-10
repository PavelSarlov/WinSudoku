using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sudoku
{
    /// <summary>
    /// Custom converter for the binding.
    /// </summary>
    public class ByteConverter : IValueConverter
    {
        /// <summary>
        /// Converts nullable byte to string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        /// <summary>
        /// Converts string to nullable byte.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (byte.TryParse((string)value, out byte num))
            {
                return num;
            }
            return null;
        }
    }
}
