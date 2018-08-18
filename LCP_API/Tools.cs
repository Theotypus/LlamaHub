using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCP_API
{
    static class Tools
    {
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
