namespace Wang_Xuejiao_HW7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OkToText", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "text", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "OkToText");
        }
    }
}
