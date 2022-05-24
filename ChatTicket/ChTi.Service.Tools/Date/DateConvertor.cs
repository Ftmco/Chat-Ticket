using System.Globalization;

namespace ChTi.Service.Tools.Date;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
    }

    public static string ToShamsiTime(this DateTime date)
    {
        PersianCalendar pc = new();
        return $"{pc.GetHour(date)}:{pc.GetMinute(date)}";
    }
}
