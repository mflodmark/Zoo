namespace Zoo.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class ZooContext : DbContext
    {
        // Your context has been configured to use a 'ZooContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Zoo.DataContext.ZooContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ZooContext' 
        // connection string in the application configuration file.
        public ZooContext(): base("name=ZooContext")
        {

        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Enviroment> Enviroments { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}