namespace Wang_Xuejiao_HW7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "MemberID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MemberID", c => c.Int(nullable: false));
        }
    }
}
