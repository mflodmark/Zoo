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

namespace Zoo.UI
{
    /// <summary>
    /// Interaction logic for AddVetVisit.xaml
    /// </summary>
    public partial class AddVetVisit : Window
    {
        public AddVetVisit()
        {
            InitializeComponent();

            AddValuesToComboBoxYear();
            AddValuesToComboBoxTime();
            AddValuesToComboBoxDiagnosis();
            AddValuesToComboBoxVet();
        }

        #region AddToComboboxes

        private void AddValuesToComboBoxTime()
        {
            for (int i = 0; i <= 23; i++)
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
                DiagnosisBox.Items.Add(item.Beskrivning);
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



        private void AddNewVetVisit_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        
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
    }
}
