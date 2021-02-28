using System.ComponentModel.DataAnnotations;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Data;
using System;

namespace FoodDatabase.Models.FoodItems
{
    public class FoodItem : FoodItemBase
    {
        FoodDatabaseContext db = new FoodDatabaseContext();
        [Required]
        public string Name { get; set; }
        [Display(Name = "Food Type")]
        public int? FoodItemType_Id { get; set; }

        public int? Category_Id { get; set; }
        public string Brand { get; set; }
        [Required]
        public double Quantity { get; set; }
        public string OriginalBrand { get; set; }
        public string OriginalName { get; set; }
        public string OriginalUnit { get; set; }
        public double OriginalQuantity { get; set; }
        public double OriginalCalories { get; set; }
        public double OriginalCarbs { get; set; }
        public double OriginalFats { get; set; }
        public double OriginalProtein { get; set; }
        public double OriginalFibre { get; set; }
        public double OriginalGlycemicIndex { get; set; }
        public DateTime OriginalDateParsed { get; set; }
        public void SetAsOriginal()
        {
            OriginalBrand = Brand;
            OriginalName = Name;
            OriginalUnit = Unit;
            OriginalQuantity = Quantity;
            OriginalCalories = Calories;
            OriginalCarbs = Carbs;
            OriginalFats = Fats;
            OriginalProtein = Protein;
            OriginalFibre = Fibre;
            OriginalGlycemicIndex = GlycemicIndex;
            OriginalDateParsed = DateTime.Now;
        }
        public FoodItem()
        {

        }
        public Category GetCategory()
        {
            return db.Categories.Find(Category_Id);
        }
        public FoodItemType GetFoodItemType()
        {
            return db.FoodItemTypes.Find(FoodItemType_Id);
        }
        public void SetFoodItemType(FoodItemType fit)
        {
            if (fit != null)
            {
                FoodItemType_Id = fit.Id;
            }
        }
        public void SetFoodItemType(int id)
        {
            FoodItemType_Id = id;
        }
        public void SetCategory(Category cat)
        {
            if (cat != null)
            {
                Category_Id = cat.Id;
            }
        }
        public void SetCategory(int id)
        {
            Category_Id = id;
        }
    }
}