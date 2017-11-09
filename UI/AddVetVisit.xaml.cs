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
    /// Interaction logic for AddVetVisit.xaml
    /// </summary>
    public partial class AddVetVisit : Window
    {
        private int currentId = 0;
        private int vetVisitId = 0;
        private List<Model.Medication> medicationsList = new List<Model.Medication>();
        private bool inEditMode;


        public AddVetVisit()
        {
            InitializeComponent();

            AddValuesToComboBoxYear();
            AddValuesToComboBoxTime();
            AddValuesToComboBoxDiagnosis();
            AddValuesToComboBoxVet();
            AddValuesToComboBoxMedication();

            //DiagnosisBox.IsEnabled = false;
            //MedicationBox.IsEnabled = false;
            //DescriptionText.IsEnabled = false;
            //AddMedicationButton.IsEnabled = false;

        }

        public void UpdateCurrentId(int id)
        {
            currentId = id;
        }

        #region EditMode

        public void AddEditDetailsIfTrue()
        {
            if (inEditMode)
            {
                DiagnosisBox.IsEnabled = true;
                MedicationBox.IsEnabled = true;
                DescriptionText.IsEnabled = true;
                AddMedicationButton.IsEnabled = true;

                YearBox.IsEnabled = false;
                MonthBox.IsEnabled = false;
                DayBox.IsEnabled = false;
                TimeBox.IsEnabled = false;
                VetBox.IsEnabled = false;
                
                AddNewVetVisit.Content = "Avsluta besök!";

                AddValuesFromVetVisit(vetVisitId);
            }
        }

        public void ChangeToFromEditMode(bool value)
        {
            inEditMode = value;
        }

        public void ChangeVisitId(int visitId)
        {
            vetVisitId = visitId;
        }

        private void AddValuesFromVetVisit(int id)
        {
            var dataAccess = new DataAccess();

            var vetVisit = dataAccess.LoadVetVisit(id);

            VetBox.Text = vetVisit.VetName;

        }


        #endregion

        #region AddToComboboxes

        private void AddValuesToComboBoxTime()
        {
            for (int i = 7; i <= 19; i++)
            {
                for (int j = 0; j <= 59; j++)
                {
                    if (i <= 9 && j <= 9)
                    {
                        TimeBox.Items.Add("0" + i + ":" + "0" + j);

                    } else if (i <= 9)
                    {
                        TimeBox.Items.Add("0" + i + ":" + j);

                    }
                    else if (j <= 9)
                    {
                        TimeBox.Items.Add(i + ":" + "0" + j);
                    }
                    else
                    {
                        TimeBox.Items.Add(i + ":" + j);
                    }
                }
            }
        }

        private void AddValuesToComboBoxDay()
        {
            DayBox.Items.Clear();

            int daysInMonth = 0;

            switch (int.Parse(MonthBox.Text))
            {
                case 1:
                    daysInMonth = 31;
                        break;
                case 2:
                    daysInMonth = DateTime.IsLeapYear(int.Parse(YearBox.Text)) ? 29 : 28;
                    break;
                case 3:
                    daysInMonth = 31;
                    break;
                case 4:
                    daysInMonth = 30;
                    break;
                case 5:
                    daysInMonth = 31;
                    break;
                case 6:
                    daysInMonth = 30;
                    break;
                case 7:
                    daysInMonth = 31;
                    break;
                case 8:
                    daysInMonth = 31;
                    break;
                case 9:
                    daysInMonth = 30;
                    break;
                case 10:
                    daysInMonth = 31;
                    break;
                case 11:
                    daysInMonth = 30;
                    break;
                case 12:
                    daysInMonth = 31;
                    break;

            }

            for (int i = 1; i <= daysInMonth; i++)
            {
                DayBox.Items.Add(i);
            }
        }

        private void AddValuesToComboBoxMonth()
        {
            MonthBox.Items.Clear();

            for (int i = 1; i <= 12; i++)
            {
                MonthBox.Items.Add(i);
            }
        }

        private void AddValuesToComboBoxYear()
        {
            for (int i = 2017; i <= 2020; i++)
            {
                YearBox.Items.Add(i);
            }

        }

        private void AddValuesToComboBoxDiagnosis()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Diagnoses)
            {
                DiagnosisBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxMedication()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Medications)
            {
                MedicationBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxVet()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Vets)
            {
                VetBox.Items.Add(item.Name);
            }
        }

        #endregion
        
        #region DropDown

        private void YearBox_DropDownClosed(object sender, EventArgs e)
        {
            if (YearBox.Text == "") return;

            AddValuesToComboBoxMonth();
            MonthBox.IsEditable = true;
        }

        private void MonthBox_DropDownClosed(object sender, EventArgs e)
        {
            if (MonthBox.Text == "") return;

            AddValuesToComboBoxDay();
            DayBox.IsEditable = true;

        }

        #endregion

        #region Clicks

        private void AddNewVetVisit_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            if (inEditMode)
            {

            }
            else
            {
                var dateConverted = Convert.ToDateTime($"{YearBox.Text}-{MonthBox.Text}-{DayBox.Text} {TimeBox.Text}");

                dataAccess.AddAnimalVetVisit(currentId, dateConverted, DiagnosisBox.Text, VetBox.Text, 
                    medicationsList, DescriptionText.Text, false);
            }


            Close();
        }

        private void AddMedicationButton_Click(object sender, RoutedEventArgs e)
        {
            var med = new Model.Medication()
            {
                Name = MedicationBox.Text
            };

            medicationsList.Add(med);

            MedicationGrid.ItemsSource = null;
            MedicationGrid.ItemsSource = medicationsList;
        }
        #endregion


    }
}
