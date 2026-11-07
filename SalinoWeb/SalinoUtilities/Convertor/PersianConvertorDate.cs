using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Salino.ToShamsi
{
    public static class PersianConvertorDate
    {
        public static string ToShamsi(this string value)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(Convert.ToDateTime(value)) + "/" + pc.GetMonth(Convert.ToDateTime(value)).ToString("00") + "/" +
                   pc.GetDayOfMonth(Convert.ToDateTime(value)).ToString("00");
        }
    }
}