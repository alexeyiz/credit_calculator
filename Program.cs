
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string c = Console.ReadLine();
            string[] a = c.Split(' ');
            int b = a.Length;
            if (b != 5)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос");
                return;
            }
            foreach (int element in c)
            {
                if ((element < '0' || element > '9') && element != ' ')
                {
                    Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос");
                    return ;
                }
            }
            double sum = Convert.ToDouble(a[0]);
            double rate = Convert.ToDouble(a[1]);
            int term = Convert.ToInt32(a[2]);
            int selectedMonth = Convert.ToInt32(a[3]);
            double payment = Convert.ToDouble(a[4]);
            if (sum < 0 || rate < 0 || term < 0 || selectedMonth < 0 || payment < 0)
            {
                Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос");
                return;
            }
            double i = rate / 12 / 100;
            int o = 0;
            DateTime date1 = System.DateTime.Now;
            double bigproz = 0;
            double bigproz1 = 0;
            double sum1 = sum;
            int term1;
            double ann = (sum * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
            int term2 = (int)Math.Ceiling(Math.Log(ann / (ann - i * sum), 1 + i));
            while (o++ < term)
            {
                DateTime date2 = new DateTime(date1.Year, date1.Month, 1);
                date1 = date2.AddMonths(1);
                TimeSpan e = date1 - date2;
                int m = (int)e.TotalDays;
                int l = (int)date2.Year;
                date2 = date2.AddMonths(1);
                int v;
                if (DateTime.IsLeapYear(l))
                    v = 366;
                else
                    v = 365;
                double proz = (sum * rate * m) / (100 * v);
                double OD = ann - proz;
                if (o != selectedMonth)
                {
                    sum -= OD;
                }
                else
                {
                    sum = sum - OD;
                    sum -= payment;
                    term1 = term - selectedMonth;
                    ann = ((sum) * i * Math.Pow((1 + i), term1)) / (Math.Pow((1 + i), term1) - 1);
                }
                    bigproz += proz;
            }
            o = 0;
            date1 = System.DateTime.Now;
            ann = (sum1 * i * Math.Pow((1 + i), term)) / (Math.Pow((1 + i), term) - 1);
            while (o++ < term2 || sum1 >= 0)
            {
                DateTime date2 = new DateTime(date1.Year, date1.Month, 1);
                date1 = date2.AddMonths(1);
                TimeSpan e = date1 - date2;
                int m = (int)e.TotalDays;
                int l = (int)date2.Year;
                date2 = date2.AddMonths(1);
                int v;
                if (DateTime.IsLeapYear(l))
                    v = 366;
                else
                    v = 365;
                double proz = (sum1 * rate * m) / (100 * v);
                double OD = ann - proz;
                if (o != selectedMonth)
                    sum1 -= OD;
                else
                {
                    sum1 = sum1 - OD - payment;
                    term2 = (int)Math.Ceiling(Math.Log(ann / (ann - i * sum1), 1 + i)) + 1;
                }
                bigproz1 += proz;
                Console.WriteLine("{0}", term2);
            }
            Console.WriteLine("Переплата при уменьшении платежа: {0:N2}", bigproz);
            Console.WriteLine("Переплата при уменьшении срока: {0:N2}", bigproz1);
            if (bigproz > bigproz1)
            Console.WriteLine("Уменьшение срока выгоднее уменьшения платежа на {0:N2}", bigproz - bigproz1);
            else if (bigproz < bigproz1)
                Console.WriteLine("Уменьшение платежа выгоднее уменьшения срока на {0:N2}", bigproz1 - bigproz);
            else
                Console.WriteLine("Переплата одинакова в обоих вариантах.");
            Console.ReadLine();
        }
    }
}
