using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zoo.DataContext;
using Zoo.DAL;

namespace Zoo.UI
{
    /// <summary>
    /// Interaction logic for AddNewAnimal.xaml
    /// </summary>
    public partial class AddNewAnimal : Window
    {
         
        List<Animal> parentsList = new List<Animal>();
        List<Animal> childrenList = new List<Animal>();

        public AddNewAnimal()
        {
            InitializeComponent();

            var context = new ZooContext();
            
            AddValuesToComboBoxEnviroment(context);
            AddValuesToComboBoxSpecies(context);
            AddValuesToComboBoxType(context);
            AddValuesToComboBoxCountry(context);
            AddValuesToComboBoxGender(context);

        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            if (CheckComboBoxValuesForZeroValues()) return;
            if (CheckName(AnimalName.Text))
            {
                ResultText.Text = "Namn är ej unikt";
                return;
            }

            if (double.TryParse(WeightBox.Text,out var weigth))
            {
                dataAccess.AddAnimal(AnimalName.Text, EnviromentBox.Text, SpeciesBox.Text,
                    TypeBox.Text, weigth, CountryBox.Text, GenderBox.Text, parentsList, childrenList);

                Close();
            }
            else
            {
                ResultText.Text = "Inmatning av vikt fungerade ej";
            }
        }

        private bool CheckName(string input)
        {
            var dataAccess = new DataAccess();

            return dataAccess.CheckName(input);
        }

        private bool CheckComboBoxValuesForZeroValues()
        {
            if (GenderBox.Text == "" || CountryBox.Text == "" || WeightBox.Text == "" || EnviromentBox.Text == ""
                || TypeBox.Text == "" || SpeciesBox.Text == "")
            {
                ResultText.Text = "Något/några fält saknar värde";
                ResultText.Foreground = Brushes.White;
                ResultText.Background = Brushes.DarkRed;
                return true;
            }

            return false;
        }

        #region AddValuesToComboBoxes

        private void AddValuesToComboBoxSpecies(ZooContext dbContext)
        {
            foreach (var item in dbContext.Species)
            {
                SpeciesBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxEnviroment(ZooContext dbContext)
        {
            foreach (var item in dbContext.Enviroments)
            {
                EnviromentBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxType(ZooContext dbContext)
        {
            foreach (var item in dbContext.Types)
            {
                TypeBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxCountry(ZooContext dbContext)
        {
            foreach (var item in dbContext.CountryOfOrigins)
            {
                CountryBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxGender(ZooContext dbContext)
        {
            foreach (var item in dbContext.Genders)
            {
                GenderBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxParentOrChild(string species)
        {
            var dbContext = new ZooContext();

            var animals = dbContext.Animals.Where(x => x.Species.Name == species).ToList();

            foreach (var item in animals)
            {

                ParentBox.Items.Add(item.Name + "-" + item.Gender.Name);
                ChildrenBox.Items.Add(item.Name + "-" + item.Gender.Name);
                
            }
        }

        #endregion

        private void SpeciesBox_DropDownClosed(object sender, EventArgs e)
        {   
            CheckSpeciesInput(SpeciesBox.Text);
            AddValuesToComboBoxParentOrChild(SpeciesBox.Text);

        }

        private void CheckSpeciesInput(string input)
        {
            var zooContext = new ZooContext();

            var species = zooContext.Species.SingleOrDefault(x => x.Name == input);

            if (species == null)
            {
                ResultText.Text = "Välj även typ och miljö";
            }
            else
            {
                TypeBox.Text = species.Type.Name;
                EnviromentBox.Text = species.Enviroment.Name;

                TypeBox.IsEditable = true;
                EnviromentBox.IsEditable = true;
                ChildrenBox.IsEditable = true;
                ParentBox.IsEditable = true;

                ResultText.Text = "Ändring av typ och miljö sker på alla";
            }

            ResultText.Background = Brushes.Chocolate;
            TypeBox.IsEnabled = true;
            EnviromentBox.IsEnabled = true;
            ParentBox.IsEnabled = true;
            ChildrenBox.IsEnabled = true;
        }


        private void AddParents_Click(object sender, RoutedEventArgs e)
        {
            var dbContext = new ZooContext();

            if (ParentGrid.Items.Count >= 2)
            {
                AddParents.IsEnabled = false;
            }

            var subString = ParentBox.Text.Split('-');
            var firstSubString = subString[0];

            var animal = dbContext.Animals.SingleOrDefault(x => x.Name == firstSubString);

            parentsList.Add(animal);

            var list = new BindingList<Model.Animal>(parentsList.Select(x => new Model.Animal()
            {
                Name = x.Name,
                Enviroment = x.Species.Enviroment.Name,
                Species = x.Species.Name,
                Type = x.Species.Type.Name,
                Gender = x.Gender.Name,
                CountryOfOrigin = x.CountryOfOrigin.Name,
                Weight = x.Weight,
                AnimalId = x.AnimalId

            }).ToList());

            ParentGrid.ItemsSource = list;
        }

        private void AddChildren_Click(object sender, RoutedEventArgs e)
        {
            var dbContext = new ZooContext();

            var subString = ChildrenBox.Text.Split('-');
            var firstSubString = subString[0];

            var animal = dbContext.Animals.SingleOrDefault(x => x.Name == firstSubString);

            childrenList.Add(animal);

            var list = new BindingList<Model.Animal>(childrenList.Select(x => new Model.Animal()
            {
                Name = x.Name,
                Enviroment = x.Species.Enviroment.Name,
                Species = x.Species.Name,
                Type = x.Species.Type.Name,
                Gender = x.Gender.Name,
                CountryOfOrigin = x.CountryOfOrigin.Name,
                Weight = x.Weight,
                AnimalId = x.AnimalId


            }).ToList());

            ChildrenGrid.ItemsSource = list;
        }
    }
}
