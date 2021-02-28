using HtmlAgilityPack;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Models.MyFitnessPalDay;
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
    public static class SiteParser
    {
        private static FoodDatabaseContext db = new FoodDatabaseContext();

        public static async Task<MfpDay> Parse(string website, MfpDay day)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(website);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(source);

            day.Htlm = source;
            

            List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
                    (x => (x.Name == "td" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("first alt"))).ToList();

            Meal meal = new Meal();


            List<HtmlNode> listOfRows = resultat.DocumentNode.Descendants().Where(x => (x.Name == "tr")).ToList();
            foreach (HtmlNode node in listOfRows)
            {
                string line = node.InnerText;
                bool isHeaderLine = false;
                if (line.Trim() == string.Empty)
                {
                    break;
                }
                if (line.Contains("AM - 12"))
                {
                    meal = Meal.Meal1;
                    isHeaderLine = true;
                }
                if (line.Contains("12 - 13"))
                {
                    meal = Meal.Meal2;
                    isHeaderLine = true;
                }
                if (line.Contains("13 - 14"))
                {
                    meal = Meal.Meal3;
                    isHeaderLine = true;
                }
                if (line.Contains("14 - 17"))
                {
                    meal = Meal.Meal4;
                    isHeaderLine = true;
                }
                if (line.Contains("17 - 20"))
                {
                    meal = Meal.Meal5;
                    isHeaderLine = true;
                }
                if (line.Contains("20 - PM"))
                {
                    meal = Meal.Meal6;
                    isHeaderLine = true;
                }

                if (!isHeaderLine)
                {
                    FoodItemViewModel fivm = ParseLine(line);
                    fivm.MealId = (int)meal;
                    day.All.Add(fivm);
                    switch (meal)
                    {
                        case Meal.Meal1:
                            day.meal1.Add(fivm);
                            break;
                        case Meal.Meal2:
                            day.meal2.Add(fivm);
                            break;
                        case Meal.Meal3:
                            day.meal3.Add(fivm);
                            break;
                        case Meal.Meal4:
                            day.meal4.Add(fivm);
                            break;
                        case Meal.Meal5:
                            day.meal5.Add(fivm);
                            break;
                        case Meal.Meal6:
                            day.meal6.Add(fivm);
                            break;
                        default:
                            break;
                    }

                }
            }
            return day;
        }

        private static FoodItemViewModel ParseLine(string line)
        {
            string workingLine = line.Trim();

            // Brand
            int index = workingLine.IndexOf('-');
            string brand = string.Empty;
            if (index != -1)
            {
                brand = workingLine.Substring(0, index);
                workingLine = workingLine.Substring(index + 2);
            }

            // Name
            index = workingLine.LastIndexOf(", ");
            string name = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index + 2);

            // Unit
            index = workingLine.IndexOf("\n");
            string unit = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            // Calories
            index = workingLine.IndexOf("\n");
            string calories = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            // Carbs
            index = workingLine.IndexOf("\n");
            string carbs = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            index = workingLine.IndexOf("\n");
            string carbsPercent = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            // Fats
            index = workingLine.IndexOf("\n");
            string fats = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            index = workingLine.IndexOf("\n");
            string fatsPercent = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            // Protein
            index = workingLine.IndexOf("\n");
            string protein = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            index = workingLine.IndexOf("\n");
            string proteinPercent = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            // Fibre
            index = workingLine.IndexOf("\n");
            string fibre = workingLine.Substring(0, index);
            workingLine = workingLine.Substring(index).Trim();

            FoodItemViewModel model = new FoodItemViewModel();

            model.FoodItem.Brand = brand;
            model.FoodItem.Name = name;
            model.FoodItem.Unit = unit;
            model.FoodItem.Calories = double.Parse(calories);
            model.FoodItem.Carbs = double.Parse(carbs);
            model.FoodItem.Fats = double.Parse(fats);
            model.FoodItem.Protein = double.Parse(protein);
            model.FoodItem.Fibre = double.Parse(fibre);
            model.FoodItem.Quantity = double.Parse(unit.Substring(0, unit.IndexOf(" ")));
            model.FoodItem.Unit = unit.Substring(unit.IndexOf(" ") + 1);

            model.FoodItemTypes = DetermineType(model.FoodItem);
            model.AllCategories = DetermineCategory(model.FoodItem, model.FoodItemTypes);
            return model;
        }

        public static List<FoodItemType> DetermineType(FoodItem item)
        {
            List<FoodItemType> typesScored = new List<FoodItemType>();
            foreach (FoodItemType t in db.FoodItemTypes)
            {
                FoodItemType newFit = new FoodItemType()
                {
                    TypeName = t.TypeName,
                    Id = t.Id,
                    Calories = t.Calories,
                    Carbs = t.Carbs,
                    CarbsDiff = t.CarbsDiff,
                    Fats = t.Fats,
                    FatDiff = t.FatDiff,
                    Protein = t.Protein,
                    ProteinDiff = t.ProteinDiff,
                    Fibre = t.Fibre,
                    FibreDiff = t.FibreDiff,
                    GlycemicIndex = t.GlycemicIndex,
                    Unit = t.Unit                    
                };

                newFit.Score = GetTypeScore(item, t);
                typesScored.Add(newFit);
            }

            return typesScored.OrderByDescending(x => x.Score).ToList();
        }

        public static int GetTypeScore(FoodItem item, FoodItemType foodItemType)
        {
            int score = 0;

            foreach (string str in foodItemType.TypeName.ToLower().Split(' '))
            {
                if (item.Name.ToLower().Contains(str))
                {
                    score++;
                }
                if (item.Brand.ToLower().Contains(foodItemType.TypeName))
                {
                    score++;
                }
            }
            double proDiff = (item.Protein * 4 / item.Calories) - (foodItemType.Protein * 4 / foodItemType.Calories);
            proDiff = proDiff < 0 ? proDiff * -1 : proDiff;
            double carbDiff = (item.Carbs * 4 / item.Calories) - (foodItemType.Carbs * 4 / foodItemType.Calories);
            carbDiff = carbDiff < 0 ? carbDiff * -1 : carbDiff;
            double fatDiff = (item.Fats * 9 / item.Calories) - (foodItemType.Fats * 9 / foodItemType.Calories);
            fatDiff = fatDiff < 0 ? fatDiff * -1 : fatDiff;
            double fibreDiff = (item.Fibre * 4 / item.Calories) - (foodItemType.Fibre * 4 / foodItemType.Calories);
            fibreDiff = fibreDiff < 0 ? fibreDiff * -1 : fibreDiff;

            foodItemType.CarbsDiff = 100 - (Math.Round(carbDiff, 2) * 100);
            foodItemType.FatDiff = 100 - (Math.Round(fatDiff, 2) * 100);
            foodItemType.ProteinDiff = 100 - (Math.Round(proDiff, 2) * 100);
            foodItemType.FibreDiff = 100 - (Math.Round(fibreDiff, 2) * 100);

            // Individually
            double threshold = 0.2;
            score += CompareSingleField(proDiff, threshold);
            score += CompareSingleField(carbDiff, threshold);
            score += CompareSingleField(fatDiff, threshold);
            score += CompareSingleField(fibreDiff, threshold);

            // As a whole
            threshold = 0.3;
            score += CompareAllFields(proDiff, carbDiff, fatDiff, fibreDiff, threshold);
            
            return score;
        }

        private static int CompareAllFields(
            double proDiff,
            double carbDiff,
            double fatDiff,
            double fibreDiff,
            double threshold)
        {
            int val = 0;

            int numberOfFields = 4;
            if (proDiff < threshold
                && carbDiff < threshold
                && fatDiff < threshold
                && fibreDiff < threshold)
            {
                val = numberOfFields;
            }
            return val;
        }

        private static int CompareSingleField(double diff, double threshold)
        {
            int val = 0;
            if (diff < threshold)
            {
                val = 1;
            }
            return val;
        }

        public static List<Category> DetermineCategory(FoodItem foodItem, List<FoodItemType> types)
        {
            List<Category> categoriesScored = new List<Category>();
            foreach (Category cat in db.Categories)
            {
                GetCategoryScore(foodItem, cat, types);
                var dacat = new Category(cat.Id, cat.FoodCategory, "");
                dacat.Score = cat.Score;
                categoriesScored.Add(dacat);
            }
            categoriesScored= categoriesScored.OrderByDescending(x => x.Score).ToList();
            return categoriesScored;
        }

        private static Category GetCategoryScore(FoodItem foodItem, Category category, List<FoodItemType> types)
        {
            var suggestedTypes = types.GetRange(0,1);
            int score = 0;

            foreach (FoodItemType suggestedType in suggestedTypes)
            {
                var prods = db.FoodItems.Where(x => x.FoodItemType_Id == suggestedType.Id);
                if (prods.Count() > 0)
                {
                    List<FoodItem> prodsInThisCategory = prods.Where(x => x.Category_Id == category.Id).ToList();
                    var count = prodsInThisCategory.Count();
                    score += count;
                    double multiplier = prodsInThisCategory.Count() / prods.Count();
                    foreach (FoodItem prod in prodsInThisCategory)
                    {
                        int suitableWordLengthMin = 2;
                        if (!string.IsNullOrEmpty(prod.Name))
                        {
                            List<string> suitableNameWords = prod.Name.ToLower().Split(' ').Where(x => x.Length > suitableWordLengthMin).ToList();
                            foreach (string str in suitableNameWords)
                            {
                                if (prod.Name.ToLower().Contains(str))
                                {
                                    score+=2;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(prod.Brand))
                        {
                            List<string> suitableBrandWords = prod.Brand.ToLower().Split(' ').Where(x => x.Length > suitableWordLengthMin).ToList();
                            foreach (string str in suitableBrandWords)
                            {

                                if (prod.Brand.ToLower().Contains(str))
                                {
                                    score++;
                                }
                            }
                        }
                    }
                    score *= (int)multiplier;
                }


                if (foodItem.Name.ToLower().Contains(category.FoodCategory.ToLower()))
                {
                    score+=2;
                }
                if (foodItem.Brand.ToLower().Contains(category.FoodCategory.ToLower()))
                {
                    score++;
                }
            }
            category.Score = score;
            return category;
        }
    }
    enum Meal
    {
        Meal1,
        Meal2,
        Meal3,
        Meal4,
        Meal5,
        Meal6
    }
}
