using FoodDatabase.MfpSiteParser;
using System.Data.Entity;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Models.MyFitnessPalDay;

namespace FoodDatabase.Data
{
    public interface IDataManagement
    {
        DbSet<Category> Categories { get; set; }
        DbSet<FoodItem> FoodItems { get; set; }
        DbSet<FoodItemType> FoodItemTypes { get; set; }
        DbSet<MyFitnessPalDay> MyFitnessPalDays { get; set; }
    }
}