namespace FeebackApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastV : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Category = c.String(),
                        Date = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        NewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewModel", t => t.NewModel_Id)
                .Index(t => t.NewModel_Id);
            
            CreateTable(
                "dbo.ProductReview",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedbackId = c.Int(nullable: false),
                        Value = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feedback", t => t.FeedbackId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FeedbackId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.NewModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Field", "Product_Id", c => c.Int());
            AddColumn("dbo.Field", "NewModel_Id", c => c.Int());
            CreateIndex("dbo.Field", "Product_Id");
            CreateIndex("dbo.Field", "NewModel_Id");
            AddForeignKey("dbo.Field", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.Field", "NewModel_Id", "dbo.NewModel", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "NewModel_Id", "dbo.NewModel");
            DropForeignKey("dbo.Field", "NewModel_Id", "dbo.NewModel");
            DropForeignKey("dbo.Field", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductReview", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductReview", "FeedbackId", "dbo.Feedback");
            DropIndex("dbo.ProductReview", new[] { "ProductId" });
            DropIndex("dbo.ProductReview", new[] { "FeedbackId" });
            DropIndex("dbo.Product", new[] { "NewModel_Id" });
            DropIndex("dbo.Field", new[] { "NewModel_Id" });
            DropIndex("dbo.Field", new[] { "Product_Id" });
            DropColumn("dbo.Field", "NewModel_Id");
            DropColumn("dbo.Field", "Product_Id");
            DropTable("dbo.NewModel");
            DropTable("dbo.ProductReview");
            DropTable("dbo.Product");
        }
    }
}
