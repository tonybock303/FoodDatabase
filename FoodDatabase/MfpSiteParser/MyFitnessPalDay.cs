using System;
using System.ComponentModel.DataAnnotations;

namespace FoodDatabase.MfpSiteParser
{
    public class MyFitnessPalDay
    {
        [Key]
        public DateTime DateOfPage { get; set; } = new DateTime();
        public DateTime TimeStamp { get; set; } = new DateTime();

        
        public string Html { get; set; }

        public MyFitnessPalDay(DateTime timeStamp, DateTime dateOfPage, string html)
        {
            TimeStamp = timeStamp;
            DateOfPage = dateOfPage;
            Html = html;

        }
        

        public MyFitnessPalDay()
        {
            
        }
        public void Update(MyFitnessPalDay day)
        {
            TimeStamp = day.TimeStamp;
            DateOfPage = day.DateOfPage;
            Html = day.Html;
        }
    }
}