using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using LaserTag.Host.Models;
using System.Windows.Media;

namespace LaserTag.Host.Helper
{
    public class HitTypeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HitType hitType)
            {
                return hitType switch
                {
                    HitType.Normal => Color.FromRgb(128, 128, 128),    // Gray
                    HitType.SSketch => Color.FromRgb(70, 130, 169),    // Blue
                    HitType.Healing => Color.FromRgb(133, 159, 61),    // Green
                    _ => Colors.Gray
                };
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HitTypeToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HitType hitType)
            {
                return hitType switch
                {
                    HitType.Normal => Colors.White,
                    HitType.SSketch => Colors.Black,
                    HitType.Healing => Colors.Black,
                    _ => Colors.White
                };
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
