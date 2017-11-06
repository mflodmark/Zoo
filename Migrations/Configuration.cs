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

            var elephant1Animal = new DataContext.Animal()
            {
                Name = "Markus",
                Weight = 160.5,
                GenderId = 1,
                CountryOfOriginId = 1,
                SpeciesId = 16
            };

            var elephant2Animal = new DataContext.Animal()
            {

                Name = "Gustav",
                Weight = 260.0,
                GenderId = 1,
                CountryOfOriginId = 1,
                SpeciesId = 16
            };

            var elephant3Animal = new DataContext.Animal()
            {

                Name = "Sandra",
                Weight = 98.5,
                GenderId = 2,
                CountryOfOriginId = 2,
                SpeciesId = 16
            };

            var speciesElephant = new Species
            {
                Name = "Elefant",
                TypeId = 18,
                EnviromentId = 24
            };

            var shark1Animal = new DataContext.Animal()
            {
                Name = "Sharky",
                Weight = 300.1,
                GenderId = 1,
                CountryOfOriginId = 3
            };

            var shark2Animal = new DataContext.Animal()
            {
                Name = "Mr.Shark",
                Weight = 10.1,
                GenderId = 1,
                CountryOfOriginId = 4
            };

            var speciesShark = new Species()
            {
                Name = "Haj",
                TypeId = 17,
                EnviromentId = 26
            };

            context.Species.AddOrUpdate(x => x.Name, speciesElephant, speciesShark);
            context.Animals.AddOrUpdate(x => x.Name, elephant1Animal, elephant2Animal, elephant3Animal
                ,shark1Animal, shark2Animal);
            
        }
    }
}
