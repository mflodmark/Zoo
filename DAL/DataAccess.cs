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
        public BindingList<DataContext.Animal> LoadAnimals()
        {
            BindingList<DataContext.Animal> animal;

            using (var db = new ZooContext())
            {
                var query = db.Animals.ToList();

                animal = new BindingList<DataContext.Animal>(query);
            }

            return animal;

        }

        //public BindingList<Book> CreateAndReturnLibrary(string category)
        //{
        //    BindingList<Book> library;

        //    using (var db = new BibliotekDB())
        //    {
        //        var q2 = db.Books.Where(x => x.Category.Name == category).ToList();

        //        //var book = new Book() { Name = name };
        //        //db.Blogs.Add(blog);
        //        //db.SaveChanges();

        //        //var query = from b in db.Blogs
        //        //            orderby b.Name
        //        //            select new Model.Blog
        //        //            {
        //        //                Id = b.BlogId,
        //        //                Name = b.Name
        //        //            };

        //        library = new BindingList<Book>(q2);
        //    }

        //    return library;
        //}
    }
}
