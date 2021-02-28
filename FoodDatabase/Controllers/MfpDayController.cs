using FoodDatabase.MfpSiteParser;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Models.MyFitnessPalDay;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FoodDatabase.Data;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FoodDatabase.Controllers
{
    public class MfpDayController : Controller
    {
        private static FoodDatabaseContext db = new FoodDatabaseContext();
        public ActionResult DayDisplay(string SelectedDate)
        {
            if (string.IsNullOrEmpty(SelectedDate))
            {
                return Redirect("/");
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public static async void Worker(object state)
        {
            DateTime d = (DateTime)state;
            List<MfpDay> fj = new List<MfpDay>();
            for (int i = 0; i < 7; i++)
            {
                MfpDay day = new MfpDay();
                await GetDate(d, day);
                d = d.AddDays(-1);
                fj.Add(day);
            }
            foreach (MfpDay day in fj)
            {
                MyFitnessPalDay dayDb = db.MyFitnessPalDays.FirstOrDefault(x => x.DateOfPage == day.SelectedDate);
                if (dayDb == null)
                {
                    MyFitnessPalDay dayObject = new MyFitnessPalDay(
                        DateTime.Now,
                        day.SelectedDate,
                        day.Htlm
                    );
                    db.MyFitnessPalDays.Add(dayObject);
                    db.SaveChanges();
                }
                else
                {
                    //dayDb.DateOfPage = day.SelectedDate;
                    dayDb.TimeStamp = DateTime.Now;
                    dayDb.Html = day.Htlm;

                    db.Entry(dayDb).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        
        public async System.Threading.Tasks.Task<ActionResult> Index(string selectedDate)
        {
            DateTime selectedDateIsGood = InterogateDate(selectedDate);
            MfpDay mfpDay = new MfpDay();
            await GetDate(selectedDateIsGood, mfpDay);
            //ThreadPool.QueueUserWorkItem(Worker, selectedDateIsGood );
            
            TempData["MfpDay"] = mfpDay;
            TempData.Keep();

            var notMatchingList = mfpDay.All.Where(x => x.FoodItemMatch.Name == null).ToList();
            bool allMatching = notMatchingList.Count() == 0;
            
            if (allMatching)
            {
                return View("DayDisplay", mfpDay);
            }
            else
            {

            }

            return View(mfpDay);
        }

        private DateTime InterogateDate(string selectedDate)
        {
            DateTime selectedDateTime;
            string session = Session["SelectedDate"]?.ToString();
            if (string.IsNullOrEmpty(selectedDate) && !string.IsNullOrEmpty(session)) selectedDate = session;
            if (string.IsNullOrEmpty(selectedDate)) { selectedDate = TodaysDate().ToString("yyyy-MM-dd"); }
            if (string.IsNullOrEmpty(selectedDate)) { throw new ArgumentNullException(); }
            selectedDateTime = DateTime.Parse(selectedDate);
            Session["SelectedDate"] = selectedDateTime.ToString("yyyy-MM-dd");
            return selectedDateTime;
        }

        public async static Task<MfpDay> GetDate(DateTime date, MfpDay mfpDay)
        {            
            mfpDay.SelectedDate = date;
            await GetDay(mfpDay);
            mfpDay.SelectedDate = date;
            MatchItemsInDatabase(mfpDay);
            for (int i = 0; i < mfpDay.All.Count(); i++)
            {
                mfpDay.All[i].Id = i;
            }

            mfpDay.All = mfpDay.All.OrderBy(x => x.FoodItemMatch.Name != null).ToList();
            
            //Console.Beep();
            return mfpDay;
        }

        private static DateTime TodaysDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }
        private static void MatchItemsInDatabase(MfpDay temp)
        {
            foreach (FoodItemViewModel model in temp.All)
            {
                List<FoodItem> matches = db.FoodItems.Where(x => x.Name == model.FoodItem.Name && (string.IsNullOrEmpty(x.Brand) == string.IsNullOrEmpty(model.FoodItem.Brand) || x.Brand == model.FoodItem.Brand)).ToList();

                if (matches.Count == 1)
                {
                    model.FoodItemMatch = matches.First();
                }
                if (matches.Count > 1)
                {
                    throw new InvalidOperationException();
                    // duplicate food item
                }
            }
        }
        [HttpPost]
        public ActionResult GetValues(int? typeId)
        {
            if (typeId != null)
            {
                var model = TempData["Model"] as FoodItemViewModel;
                TempData.Keep();
                var foodType = db.FoodItemTypes.Find(typeId);
                foodType.Score = SiteParser.GetTypeScore(model.FoodItem, foodType);

                return Json(new
                {
                    Success = "true",
                    Data = new
                    {
                        Id = foodType.Id,
                        TypeName = foodType.TypeName,
                        Unit = foodType.Unit,
                        Calories = foodType.Calories,
                        Carbs = foodType.Carbs,
                        Fats = foodType.Fats,
                        Protein = foodType.Protein,
                        Fibre = foodType.Fibre,
                        GlycemicIndex = foodType.GlycemicIndex,
                        CarbsDiff = foodType.CarbsDiff,
                        FatDiff = foodType.FatDiff,
                        ProteinDiff = foodType.ProteinDiff,
                        FibreDiff = foodType.FibreDiff
                    }
                });
            }
            return Json(new { Success = "false" });
        }
        [HttpPost]
        public ActionResult GetMealOutput(int? id, double qty)
        {
            if (id != null)
            {
                var model = TempData["MfpDay"] as MfpDay;
                TempData.Keep();

                var foodItem = model.All.FirstOrDefault(x => x.Id == id).FoodItem;
                foodItem.Quantity = qty;

                return Json(new
                {
                    Success = "true",
                    Data = new
                    {
                        Calories = foodItem.Calories,
                        Carbs = foodItem.Carbs,
                        Fats = foodItem.Fats,
                        Protein = foodItem.Protein,
                        Fibre = foodItem.Fibre,
                        GlycemicIndex = foodItem.GlycemicIndex
                    }
                });
            }
            return Json(new { Success = "false" });
        }

        public ActionResult Update(int id)
        {
            MfpDay day = TempData["MfpDay"] as MfpDay;
            List<FoodItemViewModel> modelList = day.All;
            var model = modelList.FirstOrDefault(x => x.Id == id);

            TempData["Model"] = model;
            TempData.Keep();
            return View("Update", model);
        }
        [HttpPost]
        public ActionResult Update(FoodItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.FoodItem.SetAsOriginal();
                List<string> gramUnit = new List<string> { "g", "gram" };

                foreach (string str in gramUnit)
                {
                    if (model.FoodItem.Unit.StartsWith(str))
                    {
                        model.FoodItem.Unit = "100g";
                        model.FoodItem.Quantity = model.FoodItem.Quantity / 100;
                    }
                }
                model.FoodItem.Calories = Math.Round(model.FoodItem.Calories / model.FoodItem.Quantity, 2);
                model.FoodItem.Carbs = Math.Round(model.FoodItem.Carbs / model.FoodItem.Quantity, 2);
                model.FoodItem.Fats = Math.Round(model.FoodItem.Fats / model.FoodItem.Quantity, 2);
                model.FoodItem.Protein = Math.Round(model.FoodItem.Protein / model.FoodItem.Quantity, 2);
                model.FoodItem.Fibre = Math.Round(model.FoodItem.Fibre / model.FoodItem.Quantity, 2);

                model.FoodItem.Quantity = 1;

                if (string.IsNullOrEmpty(model.NewFoodItemType))
                {
                    model.FoodItem.SetFoodItemType(model.SelectedFoodItemType);
                    model.FoodItem.GlycemicIndex = model.FoodItem.GetFoodItemType().GlycemicIndex;
                }
                else
                {
                    FoodItemType fit = new FoodItemType()
                    {
                        TypeName = model.NewFoodItemType.Trim(),
                        Unit = model.FoodItem.Unit,
                        Calories = model.FoodItem.Calories,
                        Carbs = model.FoodItem.Carbs,
                        Fats = model.FoodItem.Fats,
                        Protein = model.FoodItem.Protein,
                        Fibre = model.FoodItem.Fibre,
                        GlycemicIndex = model.FoodItem.GlycemicIndex
                    };
                    db.FoodItemTypes.Add(fit);
                    db.SaveChanges();
                    model.FoodItem.FoodItemType_Id = fit.Id;
                }
                if (string.IsNullOrEmpty(model.NewCategory))
                {
                    model.FoodItem.SetCategory(model.SelectedCategory);
                }
                else
                {
                    Category cat = new Category()
                    {
                        FoodCategory = model.NewCategory,
                        FoodItemTypesIds = model.FoodItem.GetFoodItemType().Id.ToString()
                    };

                    db.Categories.Add(cat);
                    db.SaveChanges();

                    model.FoodItem.SetCategory(cat);
                }
                db.FoodItems.Add(model.FoodItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["Model"] = model;
            TempData.Keep();
            return View("Update", model);
        }
        private static async Task<MfpDay> GetDay(MfpDay mfpDay)
        {
            string searchLink = "https://www.myfitnesspal.com/food/diary/thephoenix25?date=";  // date=2020-02-14
            string date = mfpDay.SelectedDate.ToString("yyyy-MM-dd");
            string website = searchLink + date;
            return (await SiteParser.Parse(website, mfpDay));
        }
    }
}