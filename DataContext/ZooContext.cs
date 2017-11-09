using System.ComponentModel.DataAnnotations.Schema;
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

            modelBuilder.Entity<Description>().HasKey(x => x.DescriptionId)
                .Property(x=>x.DescriptionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Description>().HasRequired(x => x.VetVisit).WithOptional(y => y.Description);

        }
    }

   
}