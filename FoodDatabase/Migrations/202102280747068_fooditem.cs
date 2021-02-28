namespace FoodDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fooditem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "OriginalBrand", c => c.String());
            AddColumn("dbo.FoodItems", "OriginalName", c => c.String());
            AddColumn("dbo.FoodItems", "OriginalUnit", c => c.String());
            AddColumn("dbo.FoodItems", "OriginalQuantity", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalCalories", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalCarbs", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalFats", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalProtein", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalFibre", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalGlycemicIndex", c => c.Double(nullable: false));
            AddColumn("dbo.FoodItems", "OriginalDateParsed", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems", "OriginalDateParsed");
            DropColumn("dbo.FoodItems", "OriginalGlycemicIndex");
            DropColumn("dbo.FoodItems", "OriginalFibre");
            DropColumn("dbo.FoodItems", "OriginalProtein");
            DropColumn("dbo.FoodItems", "OriginalFats");
            DropColumn("dbo.FoodItems", "OriginalCarbs");
            DropColumn("dbo.FoodItems", "OriginalCalories");
            DropColumn("dbo.FoodItems", "OriginalQuantity");
            DropColumn("dbo.FoodItems", "OriginalUnit");
            DropColumn("dbo.FoodItems", "OriginalName");
            DropColumn("dbo.FoodItems", "OriginalBrand");
        }
    }
}
