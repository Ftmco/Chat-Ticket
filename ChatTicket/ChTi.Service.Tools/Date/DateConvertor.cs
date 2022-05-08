﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ChTi.Service.Tools.Date;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime date)
    {
        PersianCalendar pc = new();
        return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
    }
}