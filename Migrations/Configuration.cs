using System.Collections.Generic;
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

            var type1 = new DataContext.Type {Name = "K�tt�tare"};
            var type2 = new DataContext.Type {Name = "V�xt�tare"};

            //context.Types.AddOrUpdate(x => x.Name, type1, type2);

            var enviroment1 = new Enviroment() {Name = "Mark"};
            var enviroment2 = new Enviroment() {Name = "Tr�d"};
            var enviroment3 = new Enviroment() {Name = "Vatten"};

            //context.Enviroments.AddOrUpdate(x => x.Name, enviroment1, enviroment2, enviroment3);

            var animal1 = new DataContext.Animal()
            {
                Enviroment = enviroment1,
                Quantity = 1,
                
            };

            //context.Animals.AddOrUpdate(x => x.);

            var species1 = new Species()
            {
                Name = "Elefant",
                Type = type2,
                Animals = new List<DataContext.Animal>()
            };
            species1.Animals.Add(animal1);

            context.Species.AddOrUpdate(x => x.Name, species1);

            
        }
    }
}
