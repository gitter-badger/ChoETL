﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChoETL
{
    [ChoTypeConverter(typeof(ChoCurrency))]
    public class ChoCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                string text = value as string;
                if (text.IsNullOrWhiteSpace())
                    text = "0";

                NumberStyles? format = parameter.GetValueAt<NumberStyles?>(0, ChoTypeConverterFormatSpec.Instance.Value.CurrencyNumberStyle);
                return format == null ? Double.Parse(text, culture) : Double.Parse(text, format.Value, culture);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double convValue = (double)value;
                string format = parameter.GetValueAt<string>(1, ChoTypeConverterFormatSpec.Instance.Value.CurrencyFormat);
                if (format.IsNullOrWhiteSpace())
                    format = "C";

                return convValue.ToString(format, culture); // String.Format(culture, "{0:" + format + "}", value);
            }

            return value;
        }
    }
}
