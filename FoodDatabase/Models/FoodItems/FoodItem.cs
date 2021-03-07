using System.ComponentModel.DataAnnotations;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using FoodDatabase.Data;
using System;
using System.Collections.Generic;

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
            NormalizeFromOriginal();
        }
        public void SetAsOriginal(FoodItem fi)
        {
            OriginalBrand = fi.Brand;
            OriginalName = fi.Name;
            OriginalUnit = fi.Unit;
            OriginalQuantity = fi.Quantity;
            OriginalCalories = fi.Calories;
            OriginalCarbs = fi.Carbs;
            OriginalFats = fi.Fats;
            OriginalProtein = fi.Protein;
            OriginalFibre = fi.Fibre;
            OriginalGlycemicIndex = fi.GlycemicIndex;
            OriginalDateParsed = DateTime.Now;
            NormalizeFromOriginal();
        }
        public FoodItem()
        {

        }
        private void NormalizeFromOriginal()
        {
            System.Console.Beep();
            List<string> gramUnit = new List<string> { "g", "gram" };

            foreach (string str in gramUnit)
            {
                if (Unit.StartsWith(str))
                {
                    Unit = "100g";
                    Quantity = Quantity / 100;
                }
            }
            Calories = Math.Round(Calories / Quantity, 2);
            Carbs = Math.Round(Carbs / Quantity, 2);
            Fats = Math.Round(Fats / Quantity, 2);
            Protein = Math.Round(Protein / Quantity, 2);
            Fibre = Math.Round(Fibre / Quantity, 2);

            Quantity = 1;
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

        public bool CompareMacroPercent(FoodItem foodItemToCompare, int carbsPlusOrMinus, int fatsPlusOrMinus, int proteinPlusOrMinus)
        {
            if (Calories < 70)
            {
                return true;
            }
            double carbs = foodItemToCompare.CarbsPercent - CarbsPercent;
            double fats = foodItemToCompare.FatsPercent - FatsPercent;
            double protein = foodItemToCompare.ProteinPercent - ProteinPercent;
            carbs = carbs < 0 ? carbs * -1 : carbs;
            fats = fats < 0 ? fats * -1 : fats;
            protein = protein < 0 ? protein * -1 : protein;
            bool success = (carbs < carbsPlusOrMinus && fats < fatsPlusOrMinus && protein < proteinPlusOrMinus);
            if (!success)
            {
                int i = 0;
                // Match macros do not match
            }
            return success;
        }
    }
}