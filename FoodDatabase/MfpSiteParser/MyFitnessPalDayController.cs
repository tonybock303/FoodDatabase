using HtmlAgilityPack;
using FoodDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FoodDatabase.Data;
namespace FoodDatabase.MfpSiteParser
{
    public class MyFitnessPalDayDataControl
    {
        private FoodDatabaseContext db;
        public MyFitnessPalDayDataControl()
        {            
        }
        public void Save(MyFitnessPalDay dayObject)
        {
            //db = dayObject.db;
            //MyFitnessPalDay dayDb = db.MyFitnessPalDays.FirstOrDefault(x => x.Id == dayObject.Id);
            //if (dayDb == null)
            //{
            //    //db.FoodItems.Add(foodItem);
            //    //db.SaveChanges();
            //    throw new NullReferenceException();
            //}
            //dayDb.DateOfPage = dayObject.DateOfPage;
            //dayDb.TimeStamp = dayObject.TimeStamp;
            //dayDb.Html = dayObject.Html;

            //db.Entry(dayDb).State = EntityState.Modified;
            //db.SaveChanges();
        }

        public MyFitnessPalDay Get(DateTime day)
        {
            return db.MyFitnessPalDays.FirstOrDefault(x => x.DateOfPage == day);
        }

        
    }
}