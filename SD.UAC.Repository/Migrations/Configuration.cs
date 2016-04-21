using System.Data.Entity.Migrations;
using SD.UAC.Repository.Base;
using ShSoft.Framework2016.Infrastructure.IRepository;

namespace SD.UAC.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbSession>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DbSession context)
        {
            //��ʼ������
            IDataInitializer initializer = new DataInitializer(context);
            initializer.Initialize();
        }
    }
}