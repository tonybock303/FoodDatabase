namespace FoodDatabase.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodDatabase.Data.FoodDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodDatabase.Data.FoodDatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //List<MfpSiteParser.MyFitnessPalDay> mfpList = new List<MfpSiteParser.MyFitnessPalDay>();
            //for (int i = 0; i < 7; i++)
            //{
            //    MfpSiteParser.MyFitnessPalDay mfpDay = new MfpSiteParser.MyFitnessPalDay()
            //    {
            //        DateOfPage = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-i),
            //        TimeStamp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            //        Html = ""
            //    };
            //    context.MyFitnessPalDays.AddOrUpdate(mfpDay);
            //}            
            //context.SaveChanges();



            /*Models.FoodItems.FoodItemType fit = new Models.FoodItems.FoodItemType()
            {
                TypeName = "Lettuce",
                Unit = "100g",
                Calories = 14,
                Carbs = 2.5,
                Fats = 0.1,
                Protein = 0.75,
                Fibre = 1.2,
                GlycemicIndex = 5
            };
            context.FoodItemTypes.AddOrUpdate(fit);

            Models.FoodItems.FoodItem fi = new Models.FoodItems.FoodItem()
            {
                Id = 0,
                Name = "Iceberg Lettuce",
                Unit = "100g",
                Quantity = 1,
                Calories = 14,
                Carbs = 2.5,
                Fats = 0.1,
                Protein = 0.75,
                Fibre = 1.2,
                GlycemicIndex = 5,
                Category_Id = 32,
                FoodItemType_Id = 28,
                Brand = "Tesco"
            };
            context.FoodItems.AddOrUpdate(fi);

            context.Categories.AddOrUpdate(
                c => c.FoodCategory,
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Vegetables",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Seafood",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Beef",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Chicken",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Bacon",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Lamb",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Pork",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Fish",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Fruit",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Dairy",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Nuts",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Grains",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Mushrooms",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Spices",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Beverages",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Soups",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Baked Products",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Fast Food",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Sauce",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Condiments",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Preserves",
                        FoodItemTypesIds = ""
                    },
                    new Models.Categories.Category()
                    {
                        FoodCategory = "Generic",
                        FoodItemTypesIds = ""
                    }
);
            //context.Categories.AddOrUpdate(cat);
            context.SaveChanges();*/
        }
    }
}
