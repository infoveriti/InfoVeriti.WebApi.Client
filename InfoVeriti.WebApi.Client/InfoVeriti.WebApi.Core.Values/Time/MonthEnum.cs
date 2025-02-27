namespace InfoVeriti.WebApi.Core.Values.Time;

public enum MonthEnum
{
	January = 1,
	February = 2,
	March = 3,
	April = 4,
	May = 5,
	June = 6,
	July = 7,
	August = 8,
	September = 9,
	October = 10,
	November = 11,
	December = 12
}

public static class MonthsEnumExtensions
{
	public static string ToShortString( this MonthEnum monthEnum )
	{
		return monthEnum switch
		{
			MonthEnum.January => "Jan",
			MonthEnum.February => "Feb",
			MonthEnum.March => "Mar",
			MonthEnum.April => "Apr",
			MonthEnum.May => "May",
			MonthEnum.June => "Jun",
			MonthEnum.July => "Jul",
			MonthEnum.August => "Aug",
			MonthEnum.September => "Sep",
			MonthEnum.October => "Oct",
			MonthEnum.November => "Nov",
			MonthEnum.December => "Dec",
			_ => throw new ArgumentOutOfRangeException( nameof( monthEnum ), monthEnum, null )
		};
	}
	
	public static byte GetDaysInMonth(this MonthEnum monthEnum, bool isLeapYear)
	{
		return monthEnum switch
		{
			MonthEnum.January => 31,
			MonthEnum.February => (byte)(isLeapYear ? 29 : 28),
			MonthEnum.March => 31,
			MonthEnum.April => 30,
			MonthEnum.May => 31,
			MonthEnum.June => 30,
			MonthEnum.July => 31,
			MonthEnum.August => 31,
			MonthEnum.September => 30,
			MonthEnum.October => 31,
			MonthEnum.November => 30,
			MonthEnum.December => 31,
			_ => throw new ArgumentOutOfRangeException( nameof( monthEnum ), monthEnum, null )
		};
	}
}