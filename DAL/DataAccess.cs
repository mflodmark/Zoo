using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    Enviroment = x.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();
               
                animal = new BindingList<Animal>(query);
            }

            return animal;
        }

        public BindingList<Animal> Search(string type, string enviroment, string species)
        {
            BindingList<Animal> animal;
            var typeString = "Typ";
            var enviromentString = "Miljö";
            var speciesString = "Art";


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

        private BindingList<Animal> SearchType(string type)
        {
            BindingList<Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.Species.Type.Name == type).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Enviroment.Name,
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
                var query = db.Animals.Where(y => y.Enviroment.Name == enviroment).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Enviroment.Name,
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
                    Enviroment = x.Enviroment.Name,
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
                var query = db.Animals.Where(y => y.Species.Type.Name == type && y.Enviroment.Name == enviroment).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Enviroment.Name,
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
                    Enviroment = x.Enviroment.Name,
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
                var query = db.Animals.Where(y => y.Enviroment.Name == enviroment && y.Species.Name == species).Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Enviroment.Name,
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
                var query = db.Animals.Where(y => y.Species.Type.Name == type && y.Species.Name == species && y.Enviroment.Name == enviroment)
                    .Select(x => new Animal()
                {
                    Name = x.Name,
                    Enviroment = x.Enviroment.Name,
                    Species = x.Species.Name,
                    Type = x.Species.Type.Name

                }).ToList();

                animal = new BindingList<Animal>(query);
            }

            return animal;
        }
    }
}
