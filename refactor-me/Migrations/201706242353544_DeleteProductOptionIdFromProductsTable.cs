using System;
using System.Data.Entity.Migrations;

namespace refactor_me.Migrations
{
    public partial class DeleteProductOptionIdFromProductsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductOptionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductOptionId", c => c.Guid(nullable: false));
        }
    }
}
