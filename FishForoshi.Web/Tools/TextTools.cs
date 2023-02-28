namespace FishForoshi.Web.Tools
{
    using DocumentFormat.OpenXml.Drawing.Diagrams;
    using FishForoshi.Entities;
    using System;
    using System.Globalization;

    public static class TextTools
    {
        public static string ToPersianDate(this DateTime dateTime, string format = "yyyy/MM/dd")
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                return format
                        .Replace("yyyy", pc.GetYear(dateTime).ToString())
                        .Replace("MM", pc.GetMonth(dateTime).ToString())
                        .Replace("dd", pc.GetDayOfMonth(dateTime).ToString())
                        .Replace("hh", pc.GetHour(dateTime).ToString())
                        .Replace("mm", pc.GetMinute(dateTime).ToString())
                        .Replace("ss", pc.GetSecond(dateTime).ToString());
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ToPrice(this double price)
        {
            return price.ToString("#,##0");
        }

        public static string GetPersianNumber(string englishNumber)
        {
            string str = "";
            foreach (char c in englishNumber)
            {
                str += ((char)(1776.0 + char.GetNumericValue(c))).ToString();
            }
            return str;
        }

        public static string GetEnglishNumber(string number)
        {
            string englishNumber = "";

            if (string.IsNullOrEmpty(number))
                return string.Empty;

            foreach (char ch in number)
            {
                englishNumber += char.GetNumericValue(ch);
            }
            return englishNumber;
        }

        #region Numbers
        private static string[] yekan = new string[10]
        {
              "صفر",
              "یک",
              "دو",
              "سه",
              "چهار",
              "پنج",
              "شش",
              "هفت",
              "هشت",
              "نه"
        };

        private static string[] dahgan = new string[10]
        {
              "",
              "",
              "بیست",
              "سی",
              "چهل",
              "پنجاه",
              "شصت",
              "هفتاد",
              "هشتاد",
              "نود"
        };

        private static string[] nemidunam = new string[10]
        {
              "ده",
              "یازده",
              "دوازده",
              "سیزده",
              "چهارده",
              "پانزده",
              "شانزده",
              "هفده",
              "هجده",
              "نوزده"
        };
        private static string[] sadgan = new string[10]
        {
              "",
              "یکصد",
              "دویست",
              "سیصد",
              "چهارصد",
              "پانصد",
              "ششصد",
              "هفتصد",
              "هشتصد",
              "نهصد"
        };
        private static string[] prefiexes = new string[5]
        {
            "",
            "هزار",
            "میلیون",
            "میلیارد",
            "تریلیون"
        };
        #endregion

        private static string X(int A_0)
        {
            string str1 = "";
            int num = A_0 % 100;
            int index1 = A_0 / 100;
            if ((uint)index1 > 0U)
                str1 = sadgan[index1] + " و ";
            string str2;
            if (num >= 10 && num <= 19)
            {
                str2 = str1 + nemidunam[num - 10];
            }
            else
            {
                int index2 = num / 10;
                if ((uint)index2 > 0U)
                    str1 = str1 + dahgan[index2] + " و ";
                int index3 = num % 10;
                if ((uint)index3 > 0U)
                    str1 = str1 + yekan[index3] + " و ";
                str2 = str1.Substring(0, str1.Length - 3);
            }
            return str2;
        }

        public static string ToLetters(string snum)
        {
            string str = "";
            if (snum == "")
                return "صفر";
            if (snum == "0")
                return yekan[0];
            snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
            int num = snum.Length / 3 - 1;
            for (int index = 0; index <= num; ++index)
            {
                int A_0 = int.Parse(snum.Substring(index * 3, 3));
                if ((uint)A_0 > 0U)
                    str = str + X(A_0) + " " + prefiexes[num - index] + " و ";
            }
            return str.Substring(0, str.Length - 3);
        }

        public static string ToLetters(int num)
        {
            return ToLetters(num.ToString());
        }
    }
}
