using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CustomLeaderboardsWebsite
{
	public class Utils
	{
		private static CultureInfo us = CultureInfo.GetCultureInfo("en-US");

		public static string FormatString(int value, Formatting format) => FormatString((double) value, format);

		public static string FormatString(double value, Formatting format)
		{
			return format switch
			{
				Formatting.single => value.ToString("N0", us),
				Formatting.percent => value.ToString("P", us),
				_ => value.ToString("N2", us)
			};
		}
	}
}
