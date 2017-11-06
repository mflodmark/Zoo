﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DataContext;
using Animal = Zoo.Model.Animal;

namespace Zoo.DAL
{
    public class DataAccess
    {
        public BindingList<Animal> LoadAnimals()
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name,
                    Gender = x.Gender.Name,
                    CountryOfOrigin = x.CountryOfOrigin.Name,
                    Weight = x.Weight

                }).ToList();
               
                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        public void AddAnimal(string animalName,string enviromentName, string speciesName, string typeName, double weigth, string country,
            string gender)
        {
            using (var db = new ZooContext())
            {
                // Species
                var typeId = db.Types.Where(x => x.Name == typeName).Select(y => y.TypeId).ToString();
                var enviromentId = db.Enviroments.Where(x => x.Name == enviromentName).Select(y => y.EnviromentId).ToString();

                var species = new Species()
                {
                    Name = speciesName,
                    TypeId = int.Parse(typeId),
                    EnviromentId = int.Parse(enviromentId)
                };

                db.Species.AddOrUpdate(x => x.Name, species);

                db.SaveChanges();

                var speciesId = species.SpeciesId;

                // Country
                var countryOfOrigin = new CountryOfOrigin()
                {
                    Name = country
                };

                db.Species.AddOrUpdate(x => x.Name, species);

                db.SaveChanges();

                var countryId = countryOfOrigin.CountryOfOriginId;

                // Animal
                var genderId = db.Genders.Where(x => x.Name == gender).Select(y => y.GenderId).ToString();

                var animal = new DataContext.Animal()
                {
                    Name = animalName,
                    SpeciesId = speciesId,
                    Weight = weigth,
                    CountryOfOriginId = countryId,
                    GenderId = int.Parse(genderId)
                };

                db.Animals.Add(animal);

                db.SaveChanges();


            }
        }

        
        public BindingList<Animal> Search(string type, string enviroment, string species)
        {
            BindingList<Animal> animal;
            var typeString = "";
            var enviromentString = "";
            var speciesString = "";


            if (type != typeString && enviroment == enviromentString && species == speciesString)
            {
                animal = SearchType(type);
            }
            else if (type == typeString && enviroment != enviromentString && species == speciesString)
            {
                animal = SearchEnviroment(enviroment);
            }
            else if (type == typeString && enviroment == enviromentString && species != speciesString)
            {
                animal = SearchSpecies(species);
            }
            else if (type != typeString && enviroment != enviromentString && species == speciesString)
            {
                animal = SearchTypeAndEnviroment(type, enviroment);
            }
            else if (type == typeString && enviroment != enviromentString && species != speciesString)
            {
                animal = SearchEnviromentAndSpecies(enviroment, species);
            }
            else if (type != typeString && enviroment == enviromentString && species != speciesString)
            {
                animal = SearchTypeAndSpecies(type, species);
            }
            else if (type != typeString && enviroment != enviromentString && species != speciesString)
            {
                animal = SearchTypeAndSpeciesAndEnviroment(type, species, enviroment);
            }
            else
            {
                animal = LoadAnimals();
            }
            
            return animal;
        }

        #region Different Search Methods

        private BindingList<Animal> SearchType(string type)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Type.Name == type).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchEnviroment(string enviroment)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Enviroment.Name == enviroment).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchSpecies(string species)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Name.Contains(species)).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchTypeAndEnviroment(string type, string enviroment)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Type.Name == type && y.Species.Enviroment.Name == enviroment).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchTypeAndSpecies(string type, string species)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Type.Name == type && y.Species.Name == species).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchEnviromentAndSpecies(string enviroment, string species)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Enviroment.Name == enviroment && y.Species.Name == species).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Species.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        private BindingList<Animal> SearchTypeAndSpeciesAndEnviroment(string type, string species, string enviroment)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Type.Name == type && y.Species.Name == species && y.Species.Enviroment.Name == enviroment)
                    .Select(x => new Animal()
                    {
                        Name = x.Name,
                        Enviroment = x.Species.Enviroment.Name,
                        Species = x.Species.Name,
                        Type = x.Species.Type.Name

                    }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        #endregion

    }
}
