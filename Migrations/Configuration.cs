using Zoo.DataContext;
using Animal = Zoo.Model.Animal;

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

            var type1 = new DataContext.Type {Name = EnumType.Köttätare};
            var type2 = new DataContext.Type {Name = EnumType.Växtätare};

            context.Types.AddOrUpdate(x => x.Name, type1, type2);

            var enviroment1 = new Enviroment() {Name = EnumEnviroment.Mark};
            var enviroment2 = new Enviroment() {Name = EnumEnviroment.Träd};
            var enviroment3 = new Enviroment() {Name = EnumEnviroment.Vatten};

            context.Enviroments.AddOrUpdate(x => x.Name, enviroment1, enviroment2, enviroment3);

            var animal1 = new DataContext.Animal()
            {
                Enviroment = enviroment1,

            };
        }
    }
}
