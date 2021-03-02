using FoodDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodDatabase.Models.FoodItemTypes
{
    public class FoodItemType : FoodItemBase
    {
        FoodDatabaseContext db = new FoodDatabaseContext();
        [Display(Name ="Type Name")]        
        public string TypeName { get; set; }

        [NotMapped]
        public int Score { get; set; }
        [NotMapped]
        public double ProteinDiff { get; set; }
        [NotMapped]
        public double CarbsDiff { get; set; }
        [NotMapped]
        public double FatDiff { get; set; }
        [NotMapped]
        public double FibreDiff { get; set; }
        [NotMapped]
        public int FoodItemCount { get
            {
                return GetFoodItemCount();
            }
        }
        public int GetFoodItemCount()
        {
            return db.FoodItems.Where(x => x.FoodItemType_Id == Id).Count();
        }
    }
}