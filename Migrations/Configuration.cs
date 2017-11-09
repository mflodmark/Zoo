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
                GenderId = 3,
                CountryOfOriginId = 10,
                SpeciesId = 1,
            };

            var elephant2Animal = new DataContext.Animal()
            {

                Name = "Gustav",
                Weight = 260.0,
                GenderId = 3,
                CountryOfOriginId = 6,
                SpeciesId = 1
            };

            var elephant3Animal = new DataContext.Animal()
            {

                Name = "Sandra",
                Weight = 98.5,
                GenderId = 4,
                CountryOfOriginId = 8,
                SpeciesId = 1
            };

            var speciesElephant = new Species
            {
                Name = "Elefant",
                TypeId = 4,
                EnviromentId = 4
            };

            var shark1Animal = new DataContext.Animal()
            {
                Name = "Sharky",
                Weight = 300.1,
                GenderId = 3,
                CountryOfOriginId = 7,
                SpeciesId = 2
            };

            var shark2Animal = new DataContext.Animal()
            {
                Name = "Mr.Shark",
                Weight = 10.1,
                GenderId = 3,
                CountryOfOriginId = 6,
                SpeciesId = 2
            };

            var speciesShark = new Species()
            {
                Name = "Haj",
                TypeId = 3,
                EnviromentId = 6
            };


            context.Species.AddOrUpdate(x => x.Name, speciesElephant, speciesShark);

            context.Animals.AddOrUpdate(x => x.Name, elephant1Animal, elephant2Animal, elephant3Animal
                , shark1Animal, shark2Animal);

        }
    }
}
