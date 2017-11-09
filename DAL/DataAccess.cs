using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Zoo.DataContext;
using Zoo.Model;
using Animal = Zoo.Model.Animal;
using Medication = Zoo.DataContext.Medication;
using VetVisit = Zoo.Model.VetVisit;

namespace Zoo.DAL
{
    public class DataAccess
    {
        #region Animal-Load,Delete,Add

        public Model.Animal LoadAnimal(int animalId)
        {
            var animal = new Model.Animal();

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


                var q = query.SingleOrDefault(y => y.AnimalId == animalId);

                animal = q;
            }

            return animal;
        }

        public Model.VetVisit LoadVetVisit(int visitId)
        {
            var animal = new Model.VetVisit();

            using (var db = new ZooContext())
            {
                var query = db.VetVisits.Select(x => new VetVisit()
                {

                    Date = x.DateAndTime.ToString(),
                    VetName = x.Vet.Name,
                    VetVisitId = x.VetVisitId.ToString()

                }).ToList();


                var q = query.SingleOrDefault(y => y.VetVisitId == visitId.ToString());

                animal = q;
            }

            return animal;
        }

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


        public void AddAnimal(string animalName, string enviromentName, string speciesName, string typeName, double weigth, string countryName,
            string genderName, List<Model.Family> parentList, List<Model.Family> childList)
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
                    Parents = new List<DataContext.Animal>()
                };

                //Link
                foreach (var item in parentList)
                {
                    var q = db.Animals.SingleOrDefault(x => x.Name == item.Name);
                    animal.Parents.Add(q);
                    q.Children.Add(animal);
                }

                db.Animals.Add(animal);

                db.SaveChanges();
            }
        }

        public void EditAnimal(string animalName, string enviromentName, string speciesName, string typeName, double weigth, string countryName,
            string genderName, List<Model.Family> parentList, List<Model.Family> childList, int animalId)
        {
            using (var db = new ZooContext())
            {
                var currentAnimal = db.Animals.Find(animalId);

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

                currentAnimal.Name = animalName;
                currentAnimal.SpeciesId = speciesId;
                currentAnimal.Weight = weigth;
                currentAnimal.CountryOfOriginId = countryId;
                currentAnimal.GenderId = gender.GenderId;
                currentAnimal.Parents = new List<DataContext.Animal>();

                //Link
                foreach (var item in parentList)
                {
                    var q = db.Animals.SingleOrDefault(x => x.Name == item.Name);
                    currentAnimal.Parents.Add(q);
                    q.Children.Add(currentAnimal);

                }

                db.SaveChanges();
            }
        }


        #endregion

        #region Delete

        public void DeleteAnimal(int animalId)
        {
            using (var db = new ZooContext())
            {
                var animal = db.Animals.Find(animalId);

                var count = animal.Parents.Count;

                for (int i = 0; i < count; i++)
                {
                    animal.Parents.Remove(animal.Parents[i]);
                }

                var countC = animal.Children.Count;

                for (int i = 0; i < countC; i++)
                {
                    animal.Children.Remove(animal.Children[i]);
                }

                db.Animals.Remove(animal);

                db.SaveChanges();
            }
        }

        public void DeleteParent(int animalId, string parentName)
        {
            using (var db = new ZooContext())
            {
                var animal = db.Animals.Find(animalId);

                var parent = animal.Parents.SingleOrDefault(y => y.Name == parentName);

                animal.Parents.Remove(parent);

                db.SaveChanges();
            }
        }

        public void DeleteChild(int animalId, string childName)
        {
            using (var db = new ZooContext())
            {
                var animal = db.Animals.Find(animalId);

                var child = animal.Children.SingleOrDefault(y => y.Name == childName);

                animal.Children.Remove(child);

                db.SaveChanges();
            }
        }

        public void DeleteVetBooking(int animalId, int vetBookingId)
        {
            using (var db = new ZooContext())
            {
                var animal = db.Animals.Find(animalId);

                var vetBooking = db.VetVisits.Find(vetBookingId);

                animal.VetVisits.Remove(vetBooking);

                db.SaveChanges();
            }
        }

        #endregion

        #region Parent

        public BindingList<Model.Family> LoadAnimalsParent(int animalId)
        {
            BindingList<Model.Family> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).SelectMany(x => x.Parents);

                var parentList = query.Select(x => new Family()
                {
                    Name = x.Name,
                    Gender = x.Gender.Name
                }).ToList();

                animal = new BindingList<Model.Family>(parentList);
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

        #region Checks

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

        #endregion
        
        #region Children


        public BindingList<Model.Family> LoadAnimalsChildren(int animalId)
        {
            BindingList<Model.Family> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).SelectMany(x => x.Children);

                var list = query.Select(x => new Family()
                {
                    Name = x.Name,
                    Gender = x.Gender.Name,

                }).ToList();

                animal = new BindingList<Model.Family>(list);
            }

            return animal;
        }

        public int CheckAnimalsChildren(int animalId)
        {
            int check = 0;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).Select(x => x.Children.Count);

                check = query.Count();
            }

            return check;
        }

        #endregion
        
        #region BookVetVisit

        public BindingList<VetVisit> LoadAnimalsVet(int animalId)
        {
            BindingList<VetVisit> vetVisit;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).SelectMany(x => x.VetVisits);

                var visitList = query.Select(x => new VetVisit()
                {
                    Diagnosis = x.Diagnosis.Name,
                    Description = x.Description.Name,
                    VetName = x.Vet.Name,
                    Date = x.DateAndTime.ToString(),
                    VetVisitId = x.VetVisitId.ToString(),
                    Medications = x.Medications.Count.ToString()

                }).ToList();


                vetVisit = new BindingList<VetVisit>(visitList);
            }

            return vetVisit;
        }

        public int CheckAnimalsVet(int animalId)
        {
            int check = 0;

            using (var db = new ZooContext())
            {
                var query = db.Animals.Where(y => y.AnimalId == animalId).Select(x => x.VetVisits.Count);

                check = query.Count();
            }

            return check;
        }

        public void AddAnimalVetDetails(int animalId, string diagnosisName, 
            List<Model.Medication> medicationsList, string descriptionText, bool isUsed, int vetVisitId)
        {
            using (var db = new ZooContext())
            {
                

                // Diagnosis
                var diagnosis = db.Diagnoses.SingleOrDefault(x => x.Name == diagnosisName);
                var diaId = 0;
                var newDiagnosis = new Diagnosis();

                if (diagnosis.Name == null)
                {
                    newDiagnosis.Name = diagnosis.Name;
                    db.Diagnoses.Add(newDiagnosis);

                    db.SaveChanges();

                    diaId = newDiagnosis.DiagnosisId;
                }
                else
                {
                    diaId = diagnosis.DiagnosisId;
                }

                // Description
                var newDiagnosisDescription = new Description()
                {
                    Name = descriptionText,
                };


                db.Descriptions.Add(newDiagnosisDescription);

                var descId = newDiagnosisDescription.DescriptionId;

                // Vetvisit
                var vetVisit = db.VetVisits.Find(vetVisitId);
                vetVisit.Medications = new List<Medication>();
                vetVisit.DiagnosisId = diaId;
                vetVisit.DescriptionId = descId;
                vetVisit.IsUsed = isUsed;
                

                // Medication
                foreach (var item in medicationsList)
                {
                    var q = db.Medications.SingleOrDefault(x => x.Name == item.Name);
                    vetVisit.Medications.Add(q);

                }

                db.SaveChanges();

            }
        }

        public void AddAnimalVetVisit(int animalId, DateTime date, string diagnosisName, string vetName, 
            List<Model.Medication> medicationsList, string descriptionText, bool isUsed)
        {
            using (var db = new ZooContext())
            {

                //// Diagnosis
                //var diagnosis = db.Diagnoses.SingleOrDefault(x => x.Name == diagnosisName);
                //var diaId = 0;
                //var newDiagnosis = new Diagnosis();
                
                //if (diagnosis.Name == null)
                //{
                //    newDiagnosis.Name = diagnosis.Name;
                //    db.Diagnoses.Add(newDiagnosis);

                //    db.SaveChanges();

                //    diaId = newDiagnosis.DiagnosisId;
                //}
                //else
                //{
                //    diaId = diagnosis.DiagnosisId;
                //}

                //// Description
                //var newDiagnosisDescription = new Description()
                //{
                //    Name = descriptionText,
                //};


                //db.Descriptions.Add(newDiagnosisDescription);

                //var descId = newDiagnosisDescription.DescriptionId;

                // Vet
                var vet = db.Vets.SingleOrDefault(x => x.Name == vetName);
                

                // Vetvisit
                var vetVisit = new DataContext.VetVisit()
                {
                    AnimalId = animalId,
                    DateAndTime = date,
                    VetId = vet.VetId,
                    Medications = new List<Medication>(),
                    DiagnosisId = null,
                    DescriptionId = null,
                    IsUsed = isUsed
                };

                //// Medication
                //foreach (var item in medicationsList)
                //{
                //    var q = db.Medications.SingleOrDefault(x => x.Name == item.Name);
                //    vetVisit.Medications.Add(q);

                //}

                db.VetVisits.Add(vetVisit);

                db.SaveChanges();

            }
        }

        #endregion

        #region Medications

        public BindingList<Model.Medication> GetMedications(int vetVisitId)
        {
            BindingList<Model.Medication> list;

            using (var db = new ZooContext())
            {
                var query = db.VetVisits.Where(x => x.VetVisitId == vetVisitId).SelectMany(y => y.Medications);

                var meds = query.Select(x => new Model.Medication()
                {
                    Name = x.Name

                }).ToList();

                list = new BindingList<Model.Medication>(meds);

            }

            return list;
        }

        #endregion

        #region Different Search Methods

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

        #endregion

    }
}
