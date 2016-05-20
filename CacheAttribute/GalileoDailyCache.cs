using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheAttribute
{
    public sealed class GalileoDailyCache : GalileoCaching
    {
        /// <summary>
        /// Daily cache
        /// </summary>
        /// <param name="hour">Hour when cache expires on current day</param>
        /// <param name="minute">Minute when cache expires on current day</param>
        /// <param name="second">Second when cache expires on current day</param>
        public GalileoDailyCache(int hour = 23, int minute = 59, int second = 59)
        {
            if (hour > 23 || minute > 59 || second > 59 || hour < 0 || minute < 0 || second < 0)
                throw new ArgumentException("Invalid minutes, hours or seconds");

            var now = DateTime.Now;
            base._expirationDate = new DateTime(now.Year, now.Month, now.Day, hour, minute, second, DateTimeKind.Local);
        }


    }
}
