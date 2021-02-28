namespace FoodDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fooditem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodCategory = c.String(),
                        FoodItemTypesIds = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FoodItemType_Id = c.Int(),
                        Category_Id = c.Int(),
                        Brand = c.String(),
                        Quantity = c.Double(nullable: false),
                        OriginalBrand = c.String(),
                        OriginalName = c.String(),
                        OriginalUnit = c.String(),
                        OriginalQuantity = c.Double(nullable: false),
                        OriginalCalories = c.Double(nullable: false),
                        OriginalCarbs = c.Double(nullable: false),
                        OriginalFats = c.Double(nullable: false),
                        OriginalProtein = c.Double(nullable: false),
                        OriginalFibre = c.Double(nullable: false),
                        OriginalGlycemicIndex = c.Double(nullable: false),
                        OriginalDateParsed = c.DateTime(nullable: false),
                        Unit = c.String(),
                        Calories = c.Double(nullable: false),
                        Carbs = c.Double(nullable: false),
                        Fats = c.Double(nullable: false),
                        Protein = c.Double(nullable: false),
                        Fibre = c.Double(nullable: false),
                        GlycemicIndex = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodItemTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        Unit = c.String(),
                        Calories = c.Double(nullable: false),
                        Carbs = c.Double(nullable: false),
                        Fats = c.Double(nullable: false),
                        Protein = c.Double(nullable: false),
                        Fibre = c.Double(nullable: false),
                        GlycemicIndex = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyFitnessPalDays",
                c => new
                    {
                        DateOfPage = c.DateTime(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Html = c.String(),
                    })
                .PrimaryKey(t => t.DateOfPage);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyFitnessPalDays");
            DropTable("dbo.FoodItemTypes");
            DropTable("dbo.FoodItems");
            DropTable("dbo.Categories");
        }
    }
}
