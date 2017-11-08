using System.Security.Cryptography.X509Certificates;

namespace Zoo.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class ZooContext : DbContext
    {
        
        public ZooContext(): base("name=ZooContext")
        {

        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Enviroment> Enviroments { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<CountryOfOrigin> CountryOfOrigins { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<Vet> Vets { get; set; }
        public virtual DbSet<VetVisit> VetVisits { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Enviroment>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Species>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Type>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<CountryOfOrigin>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Gender>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Vet>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Diagnosis>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Description>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Description>().HasRequired(v => v.VetVisit).WithRequiredDependent(d => d.Description);

            //modelBuilder.Entity<AnimalAnimals>()
            //    .HasRequired(t => t.)
            //    .WithMany(t => t.Parents)
            //    .HasForeignKey(d => d.Animal_AnimalId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<AnimalAnimals>()
            //    .HasKey(c => c.Animal_AnimalId)
            //    .HasRequired(c => c.Animal_AnimalId1)
            //    .WithRequiredDependent(c => c.).wi
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<AnimalAnimals>().HasKey(x=>x.Animal_AnimalId)

        }
    }

   
}