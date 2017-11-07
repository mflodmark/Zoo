using System;
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
        #region Animal-Load,Delete,Add

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
                    Weight = x.Weight,
                    AnimalId = x.AnimalId

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        public void DeleteAnimal(int animalId)
        {
            using (var db = new ZooContext())
            {
                db.Animals.Remove(db.Animals.Find(animalId));

                db.SaveChanges();
            }
        }

        public void AddAnimal(string animalName, string enviromentName, string speciesName, string typeName, double weigth, string countryName,
            string genderName, List<DataContext.Animal> parentList, List<DataContext.Animal> childList)
        {
            using (var db = new ZooContext())
            {
                // Species
                var type = db.Types.SingleOrDefault(x => x.Name == typeName);
                var enviroment = db.Enviroments.SingleOrDefault(x => x.Name == enviromentName);

                var species = new Species()
                {
                    Name = speciesName,
                    TypeId = type.TypeId,
                    EnviromentId = enviroment.EnviromentId
                };

                db.Species.AddOrUpdate(x => x.Name, species);

                db.SaveChanges();

                var speciesId = species.SpeciesId;

                // Country
                var country = db.CountryOfOrigins.SingleOrDefault(x => x.Name == countryName);

                var countryId = country.CountryOfOriginId;

                // Animal
                var gender = db.Genders.SingleOrDefault(x => x.Name == genderName);

                var animal = new DataContext.Animal()
                {
                    Name = animalName,
                    SpeciesId = speciesId,
                    Weight = weigth,
                    CountryOfOriginId = countryId,
                    GenderId = gender.GenderId,

                    Parents = new List<DataContext.Animal>(),
                    Children = new List<DataContext.Animal>(),
                };

                //animal.Parents = parentList;
                //animal.Children = childList;

                db.Animals.Add(animal);

                db.SaveChanges();


            }
        }


        #endregion

        #region Parent

        public BindingList<Animal> LoadAnimalsParent(int animalId)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).SelectMany(x => x.Parents);

                var parentList = query.Select(x => new Animal()
                {
                    Name = x.Name,
                    Gender = x.Gender.Name
                }).ToList();

                animal = new BindingList<Animal>(parentList);
            }

            return animal;
        }

        public int CheckAnimalsParent(int animalId)
        {
            int check = 0;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).Select(x => x.Parents.Count);

                check = query.Count();
            }

            return check;
        }

        #endregion


        public bool CheckName(string input)
        {
            bool check;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Any(x => x.Name == input);
                check = query;
            }

            return check;
        }

        //public BindingList<Animal> LoadAnimalsChildren(int animalId)
        //{
        //    BindingList<Animal> animal;

        //    using (var db = new ZooContext())
        //    {
        //        var query = db.Animals.Where(y => y.AnimalId == animalId).Select(x => new Animal()
        //        {
        //            Name = x.Name,
        //            Gender = x.Gender.Name,
        //        }).ToList();

        //        animal = new BindingList<Animal>(query);
        //    }

        //    return animal;
        //}

        public BindingList<Animal> LoadAnimalsVet(int animalId)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).SelectMany(x => x.VetVisits);

                var visitList = query.Select(x => new Animal()
                {
                    Name = x.Name,
                    Gender = x.Gender.Name
                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
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
