using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MVC5.Common
{
    public class DateUtil
    {
        public static String formatDate(string day, string month, string year)
        {
            StringBuilder str = new StringBuilder();
            str.Append(day);
            str.Append(MyConstant.slash);
            str.Append(month);
            str.Append(MyConstant.slash);
            str.Append(year);
            return str.ToString();
        }

        public static DateTime convertStringToDate(string dtVal)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo(MyConstant.localeFR, true);
            return DateTime.Parse(dtVal, culture, System.Globalization.DateTimeStyles.AssumeLocal); ;
        }
    }
}