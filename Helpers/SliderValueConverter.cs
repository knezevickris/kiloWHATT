using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Windows.Storage.Streams;

namespace efficiencyCalculator.Helpers
{
    public class SliderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sliderValue = System.Convert.ToDouble(value);
            double sliderWidth = System.Convert.ToDouble(parameter);

            double minimum = 0;
            double maximum = 24;

            double relative = (sliderValue - minimum) / (maximum - minimum);
            double thumbWidth = 18;

            //returns X coordinate of slider value at the moment
            return relative * (sliderWidth - thumbWidth) + thumbWidth / 4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
