using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodDatabase.Data
{
    public class FoodDatabaseContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public FoodDatabaseContext() : base("name=FoodDatabaseContext")
        {
        }

        public System.Data.Entity.DbSet<FoodDatabase.Models.FoodItems.FoodItem> FoodItems { get; set; }

        public System.Data.Entity.DbSet<FoodDatabase.Models.FoodItemTypes.FoodItemType> FoodItemTypes { get; set; }

        public System.Data.Entity.DbSet<FoodDatabase.Models.Categories.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<FoodDatabase.MfpSiteParser.MyFitnessPalDay> MyFitnessPalDays { get; set; }
        
        //public System.Data.Entity.DbSet<FoodDatabase.Models.MfpDay> MfpDays { get; set; }

        //public System.Data.Entity.DbSet<FoodDatabase.Models.FoodItemViewModel> FoodItemViewModels { get; set; }

    }
}
