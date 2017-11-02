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

            var type1 = new DataContext.Type {Name = "Köttätare"};
            var type2 = new DataContext.Type {Name = "Växtätare"};

            var enviroment1 = new Enviroment() {Name = "Mark"};
            var enviroment2 = new Enviroment() {Name = "Träd"};
            var enviroment3 = new Enviroment() {Name = "Vatten"};

            var animal1 = new DataContext.Animal()
            {
                Enviroment = enviroment1,
                Name = "Markus"
            };

            var animal3 = new DataContext.Animal()
            {
                Enviroment = enviroment3,
                Name = "Gustav"
            };

            var species1 = new Species()
            {
                Name = "Elefant",
                Type = type2,
                Animals = new List<DataContext.Animal>()
            };

            
            var species3 = new Species()
            {
                Name = "Haj",
                Type = type1,
                Animals = new List<DataContext.Animal>()
            };

            species1.Animals.Add(animal1);
            species3.Animals.Add(animal3);

            context.Species.AddOrUpdate(x => x.Name, species1, species3);
            context.Enviroments.AddOrUpdate(x => x.Name, enviroment2);


        }
    }
}
