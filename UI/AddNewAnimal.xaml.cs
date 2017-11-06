using System;
using System.Collections.Generic;
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
        public AddNewAnimal()
        {
            InitializeComponent();

            var context = new ZooContext();
            
            AddValuesToComboBoxEnviroment(context);
            AddValuesToComboBoxSpecies(context);
            AddValuesToComboBoxType(context);
            AddValuesToComboBoxCountry(context);
            AddValuesToComboBoxGender(context);
            AddValuesToComboBoxParentOrChild(context);

        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            CheckComboBoxValuesForZeroValues();

            //dataAccess.AddAnimal();
        }

        private void CheckComboBoxValuesForZeroValues()
        {
            if (GenderBox.Text == "" || CountryBox.Text == "" || ChildrenBox.Text == "" || EnviromentBox.Text == ""
                || TypeBox.Text == "")
            {
                ResultText.Text = "Något/några fält saknar värde";
                ResultText.Foreground = Brushes.White;
                ResultText.Background = Brushes.DarkRed;
            }
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

        private void AddValuesToComboBoxParentOrChild(ZooContext dbContext)
        {
            foreach (var item in dbContext.Animals)
            {

                ParentBox.Items.Add(item.Name + " " + item.Gender.Name);
                ChildrenBox.Items.Add(item.Name + " " + item.Gender.Name);
                
            }
        }

        #endregion

        private void SpeciesBox_DropDownClosed(object sender, EventArgs e)
        {
            var zooContext = new ZooContext();
        
            CheckSpeciesInput(zooContext, SpeciesBox.Text);
        }

        private void CheckSpeciesInput(ZooContext zooContext, string input)
        {
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

                ResultText.Text = "Ändring av typ och miljö sker på alla";
            }

            ResultText.Background = Brushes.Chocolate;
            TypeBox.IsEnabled = true;
            EnviromentBox.IsEnabled = true;
        }

        private void TypeEnviromentBox_DropDownOpened(object sender, EventArgs e)
        {
            MessageBox.Show("Ändring av typ och miljö sker på alla");
        }
    }
}
