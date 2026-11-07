using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salino.Convertor
{
    public static class ConvertDate
    {
        //تبدیل حروف عدد فارسی به عدد انگلیسی
        public static string ConvertToEnglish (this string value)
        {
            string Date = value.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");

            string year = (Date.Substring(0, 4));
            string Month = (Date.Substring(5, 2));
            string Day = (Date.Substring(8, 2));
            string datem = year + "/" + Month + "/" + Day;
            return datem;
        }
        public static string ConvertToNumberPersioan(this string value)
        {
            string Date = value.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳")
                .Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷")
                .Replace("8", "۸").Replace("9", "۹");

            return Date;
        }
        public static string ConvertToNumberEnglish(this string value)
        {
            string Date = value.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");

            return Date;
        }
        //تبدیل تاریخ شمسی به تاریخ میلادی
        public static DateTime PersianDateStringToDateTime(this string persianDate)
        {
            PersianCalendar pc = new PersianCalendar();

            var persianDateSplitedParts = persianDate.Split('/');
            DateTime dateTime = new DateTime(int.Parse(persianDateSplitedParts[0]), int.Parse(persianDateSplitedParts[1]), int.Parse(persianDateSplitedParts[2]), pc);
            return DateTime.Parse(dateTime.ToString(CultureInfo.InvariantCulture));
        }
        public static string ConvertToPersian(this string value)
        {
            string Date = value.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");

            string year = (Date.Substring(0, 4));
            string Month = (Date.Substring(5, 2));
            string Day = (Date.Substring(8, 2));
            string datem = year + "/" + Month + "/" + Day;
            return datem;
        }
        public static int ConvertIntYear(this string value)
        {
            int year = int.Parse(value.Substring(0, 4));
            return year;
        }
        public static int ConvertIntMonth(this string value)
        {
            int Month = int.Parse(value.Substring(5, 2));
            return Month;
        }
        public static int ConvertIntDay(this string value)
        {
            int Day = int.Parse(value.Substring(8, 2));
            return Day;
        }
    }
}
