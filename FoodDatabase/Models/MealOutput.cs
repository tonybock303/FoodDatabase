using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoodDatabase.Models.FoodItems;
using System.Linq;
using System.Web;

namespace FoodDatabase.Models
{
    public class MealOutput
    {
        public int Id { get; set; }
        public double Calories { get; set; }

        public double Carbs { get; set; }

        public double Fats { get; set; }

        public double Protein { get; set; }

        public double Fibre { get; set; }

        [Display(Name = "Glycemic Load")]
        public double GlycemicLoad { get; set; }

        public int CarbsPercent { get { return GetCarbsPercent(); } }

        public int FatsPercent { get { return GetFatPercent(); } }

        public int ProteinPercent { get { return GetProPercent(); } }

        public MealOutput(int id, double calories, double carbs, double fats, double protein, double fibre, double glycemicLoad) :base()
        {
            Id = id;
            Calories = calories;
            Carbs = carbs;
            Fats = fats;
            Protein = protein;
            Fibre = fibre;
            GlycemicLoad = glycemicLoad;

        }
        public MealOutput(int id, IEnumerable<FoodItemViewModel> models) : base()
        {
            models.Select(x => x.FoodItem.Calories).Sum();
            Id = id;
            Calories = models.Select(x => x.FoodItem.Calories).Sum();
            Carbs = models.Select(x => x.FoodItem.Carbs).Sum();
            Fats = models.Select(x => x.FoodItem.Fats).Sum();
            Protein = models.Select(x => x.FoodItem.Protein).Sum();
            Fibre = models.Select(x => x.FoodItem.Fibre).Sum();
            double glynum = 0;
            foreach(FoodItemViewModel model in models)
            {
                glynum += model.FoodItem.Carbs / Carbs * model.FoodItemMatch.GlycemicIndex;
            }

            GlycemicLoad = Math.Round((Carbs - Fibre) * glynum / 100, 0);
            double v = double.IsNaN(GlycemicLoad) ? 0 : GlycemicLoad;
            GlycemicLoad = v;
        }

        public MealOutput()
        {

        }
        public int GetCarbsPercent()
        {
            return (int)Math.Round((Carbs * 4) / Calories * 100, 0);
        }
        public int GetFatPercent()
        {
            return (int)Math.Round((Fats * 9) / Calories * 100, 0);
        }
        public int GetProPercent()
        {
            return (int)Math.Round((Protein * 4) / Calories * 100, 0);
        }
    }
}