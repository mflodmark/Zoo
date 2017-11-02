using Zoo.DataContext;

namespace Zoo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zoo.DataContext.ZooContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zoo.DataContext.ZooContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var type1 = new DataContext.Type {Name = EnumType.K�tt�tare};
            var type2 = new DataContext.Type {Name = EnumType.V�xt�tare};

            context.Types.AddOrUpdate(x => x.Name, type1, type2);
        }
    }
}
