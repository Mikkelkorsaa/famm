using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;
using System;

public class RadzenThreeDayView : RadzenWeekView, ISchedulerView
{

    public override DateTime StartDate
    {
        get
        {
            // Get the start of the current day
            var date = Scheduler.CurrentDate.Date;

            // If we're in the middle of a three-day period, adjust to the start
            while (date.DayOfWeek != GetStartDayOfWeek())
            {
                date = date.AddDays(-1);
            }

            return date;
        }
    }

    public override DateTime EndDate
    {
        get
        {
            // Return start date plus 2 days (3 days total)
            return StartDate.AddDays(3);
        }
    }

    private DayOfWeek GetStartDayOfWeek()
    {
        // You can customize this to start on any day
        return DayOfWeek.Monday;
    }

    public override string Title
    {
        get
        {
            var start = StartDate;
            var end = EndDate;

            if (start.Month == end.Month)
            {
                return $"{start.ToString("MMMM d")} - {end.Day}, {end.Year}";
            }
            else if (start.Year == end.Year)
            {
                return $"{start.ToString("MMMM d")} - {end.ToString("MMMM d")}, {end.Year}";
            }
            else
            {
                return $"{start.ToString("MMMM d, yyyy")} - {end.ToString("MMMM d, yyyy")}";
            }
        }
    }

    public string GetTimeText(DateTime date, bool isEnd = false)
    {
        return date.ToString("ddd, MMM d");
    }
}