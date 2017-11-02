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
        
    }
}
