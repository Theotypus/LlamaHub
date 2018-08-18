using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hub
{
    class Tools
    {

        /* Returns the distance between 2 strings as an int (changes needed to go from one to the other)
        * using Levenshtein algorithm */
        public static int CompareStrings(string str1, string str2)
        {
            int n = str1.Length;
            int m = str2.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (str2[j - 1] == str1[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        // Returns the current time as a unix timestamp (precise to the milliseconds)
        public static double GetUnixTime()
        {
            DateTime dt = DateTime.Now;
            var dateTimeOffset = new DateTimeOffset(dt);
            double unixDateTime = dateTimeOffset.ToUnixTimeMilliseconds();
            return unixDateTime * Math.Pow(10, -3);
        }
    }
}
