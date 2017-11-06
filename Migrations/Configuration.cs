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

            //var animal1 = new DataContext.Animal()
            //{
            //    Name = "Markus",

            //};

            //var animal3 = new DataContext.Animal()
            //{
            //    Name = "Gustav"
            //};

            //var species1 = new Species()
            //{
            //    Name = "Elefant",
            //    Type = type2,
            //    Animals = new List<DataContext.Animal>()
            //};


            //var species3 = new Species()
            //{
            //    Name = "Haj",
            //    Type = type1,
            //    Animals = new List<DataContext.Animal>()
            //};

            //species1.Animals.Add(animal1);
            //species3.Animals.Add(animal3);


        }
    }
}
