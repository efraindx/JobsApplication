namespace JobsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class verifying : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.JobsOfferViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        JobType = c.String(),
                        Company = c.String(),
                        Logo = c.String(),
                        URL = c.String(),
                        Position = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);*/
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobsOfferViewModels");
        }
    }
}
