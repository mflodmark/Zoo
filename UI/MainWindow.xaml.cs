﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zoo.DAL;
using Zoo.DataContext;

namespace Zoo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            LoadInitialDataToDataGrid();

            AddValuesToComboBoxType();
            AddValuesToComboBoxEnviroment();
            AddValuesToComboBoxSpecies();
        }

        
        #region AddValuesToComboBoxes

        private void AddValuesToComboBoxSpecies()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Species)
            {
                SearchSpeciesBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxEnviroment()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Enviroments)
            {
                SearchEnviromentBox.Items.Add(item.Name);
            }
        }

        private void AddValuesToComboBoxType()
        {
            var dbContext = new ZooContext();

            foreach (var item in dbContext.Types)
            {
                SearchTypeBox.Items.Add(item.Name);
            }
        }


        #endregion

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadInitialDataToDataGrid()
        {
            var dataAccess = new DataAccess();

            AnimalGrid.DataContext = dataAccess.LoadAnimals();
            
        }
    }
}
