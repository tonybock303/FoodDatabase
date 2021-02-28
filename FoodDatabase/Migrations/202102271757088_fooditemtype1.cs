namespace FoodDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fooditemtype1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FoodItems", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
