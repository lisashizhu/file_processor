using System;
using System.Globalization;
using FileProcessor.Common.Exceptions;

namespace FileProcessor.Common.Extensions
{
	public static class StringExtension
	{
		public static DateTime ToDateTime(this string datetime,string format)
        {
            if (string.IsNullOrEmpty(datetime))
            {
                throw new ArgumentNullException(nameof(datetime));
            }
            if (DateTime.TryParseExact(datetime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var validDate))
            {
                return validDate;
            }
            else throw new BusinessException("Date is in wrong format");
        }
	}
}

