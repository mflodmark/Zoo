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
        }

        private void AddValuesToComboBoxTime()
        {

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

        private void AddNewVetVisit_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }



        private void AddValuesToComboBoxSpecies()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Species)
            {
                DiagnosisBox.Items.Add(item.Name);
            }
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
