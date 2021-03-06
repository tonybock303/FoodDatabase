﻿using System;
using System.Collections.Generic;
using System.Linq;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Data;
namespace FoodDatabase.Models.FoodItemTypes
{
    public class FoodItemTypeDetailsViewModel
    {
        FoodDatabaseContext db = new FoodDatabaseContext();
        public FoodItemType FoodItemType { get; set; }
        public List<FoodItem> FoodItemsOfThisType { get; set; }
        public int CarbsAveragePercent { get { return GetAverageCarbsPercent(); } }

        public int FatsAveragePercent { get { return GetAverageFatsPercent(); } }

        public int ProteinAveragePercent { get { return GetAverageProteinPercent(); } }

        public FoodItemTypeDetailsViewModel( FoodItemType fit)
        {
            FoodItemType = fit;
            FoodItemsOfThisType = db.FoodItems.Where(x => x.FoodItemType_Id == fit.Id).ToList();
        }

        public int GetAverageCarbsPercent()
        {
            int val = 0;
            if (FoodItemsOfThisType != null && FoodItemsOfThisType.Count > 0)
            {
                val = (int)Math.Round(FoodItemsOfThisType.Select(x => x.GetCarbsPercent()).Average(), 0);
            }
            return val;
        }
        public int GetAverageFatsPercent()
        {
            int val = 0;
            if (FoodItemsOfThisType != null && FoodItemsOfThisType.Count > 0)
            {
                val = (int)Math.Round(FoodItemsOfThisType.Select(x => x.GetFatPercent()).Average(), 0);
            }
            return val;
        }
        public int GetAverageProteinPercent()
        {
            int val = 0;
            if (FoodItemsOfThisType != null && FoodItemsOfThisType.Count > 0)
            {
                val = (int)Math.Round(FoodItemsOfThisType.Select(x => x.GetProPercent()).Average(), 0);
            }
            return val;
        }
        
    }
}
