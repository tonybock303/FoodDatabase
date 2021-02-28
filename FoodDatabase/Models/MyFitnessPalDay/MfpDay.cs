using System;
using System.Collections.Generic;
using FoodDatabase.Models.FoodItems;

namespace FoodDatabase.Models.MyFitnessPalDay
{
    public class MfpDay
    {
        public int Id { get; set; }
        public List<FoodItemViewModel> All = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal1 = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal2 = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal3 = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal4 = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal5 = new List<FoodItemViewModel>();
        public List<FoodItemViewModel> meal6 = new List<FoodItemViewModel>();
        public DateTime SelectedDate { get; set; }
        public string Htlm { get; set; }
        
    }
}