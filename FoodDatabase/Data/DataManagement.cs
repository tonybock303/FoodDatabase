using FoodDatabase.MfpSiteParser;
using FoodDatabase.Models.FoodItems;
using FoodDatabase.Models.Categories;
using FoodDatabase.Models.FoodItemTypes;
using System;
using System.Data.Entity;

namespace FoodDatabase.Data
{
    public class DataManagement : IDataManagement
    {
        public DbSet<Category> Categories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<FoodItem> FoodItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<FoodItemType> FoodItemTypes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<MyFitnessPalDay> MyFitnessPalDays { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}