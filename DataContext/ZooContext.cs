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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().Property(x => x.Quantity).IsRequired();

            modelBuilder.Entity<Enviroment>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Species>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Type>().Property(x => x.Name).IsRequired();
        }
    }

   
}