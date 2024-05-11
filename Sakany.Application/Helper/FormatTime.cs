namespace Sakany.Application.Helper
{
    public class FormatTime
    {

        public static string FormatingTime(DateTime postDate)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeDifference = currentDate - postDate;

            int daysDifference = (int)timeDifference.TotalDays;
            int hoursDifference = (int)timeDifference.TotalHours;
            int minutesDifference = (int)timeDifference.TotalMinutes;

            if (daysDifference < 1)
            {
                if (hoursDifference < 1)
                {
                    if (minutesDifference < 1)
                    {
                        return "Just now";
                    }
                    else if (minutesDifference == 1)
                    {
                        return "1m";
                    }
                    else
                    {
                        return $"{minutesDifference}m";
                    }
                }
                else if (hoursDifference == 1)
                {
                    return "1h";
                }
                else
                {
                    return $"{hoursDifference}h";
                }
            }
            else if (daysDifference == 1)
            {
                return "1d";
            }
            else if (daysDifference < 7)
            {
                return $"{daysDifference}d";
            }
            else
            {
                return postDate.ToString("MMM dd, yyyy");
            }
        }
    }

}
