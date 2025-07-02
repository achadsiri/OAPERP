using System;
using System.Collections.Generic;

namespace EGPConnect.Utility
{
    public static class ParserData
    {
        public static DateTime? ParseThaiDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date)) return null;

            var thaiMonths = new Dictionary<string, int>
            {
                ["ม.ค."] = 1, ["ก.พ."] = 2, ["มี.ค."] = 3, ["เม.ย."] = 4,
                ["พ.ค."] = 5, ["มิ.ย."] = 6, ["ก.ค."] = 7, ["ส.ค."] = 8,
                ["ก.ย."] = 9, ["ต.ค."] = 10, ["พ.ย."] = 11, ["ธ.ค."] = 12
            };

            var parts = date.Split(' ');
            if (parts.Length != 3) return null;

            if (!int.TryParse(parts[0], out int day)) return null;
            if (!thaiMonths.TryGetValue(parts[1], out int month)) return null;
            if (!int.TryParse(parts[2], out int yearShort)) return null;

            int year = (yearShort < 2500) ? 2500 + yearShort : yearShort;

            try
            {
                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ParseDecimal(string val)
        {
            return decimal.TryParse(val.Replace(",", ""), out var result) ? result : null;
        }
    }
}
