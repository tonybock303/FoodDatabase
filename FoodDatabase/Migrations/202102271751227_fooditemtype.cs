namespace FoodDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fooditemtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems", "Discriminator");
        }
    }
}
