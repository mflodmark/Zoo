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
using Zoo.UI;

namespace Zoo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentId = 0;
        string parentName;
        string childName;
        int visitId = 0;
        private string isUsed;

        public MainWindow()
        {

            InitializeComponent();

            LoadInitialDataToDataGrid();

            AddValuesToComboBoxType();
            AddValuesToComboBoxEnviroment();
            AddValuesToComboBoxSpecies();

            ChangeButtonState();

        }


        #region AddValuesToComboBoxes

        private void AddValuesToComboBoxSpecies()
        {
            var dbContext = new ZooContext();

            SearchSpeciesBox.Items.Clear();

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

        #region Search

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            AnimalGrid.ItemsSource = dataAccess.Search(SearchTypeBox.Text, SearchEnviromentBox.Text, SearchSpeciesBox.Text);

            SetGridToNullAndRefresh();

        }

        private void SetGridToNullAndRefresh()
        {
            VetGrid.ItemsSource = null;
            ParentGrid.ItemsSource = null;
            ChildrenGrid.ItemsSource = null;

            VetGrid.Items.Refresh();
            ParentGrid.Items.Refresh();
            ChildrenGrid.Items.Refresh();
        }

        #endregion

        #region ButtonClicks

        private void LoadInitialDataToDataGrid()
        {
            var dataAccess = new DataAccess();

            AnimalGrid.ItemsSource = dataAccess.LoadAnimals();

            SetGridToNullAndRefresh();
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var openForm = new AddNewAnimal();

            openForm.ChangeToFromEditMode(false);

            openForm.ShowDialog();

            LoadInitialDataToDataGrid();
            AddValuesToComboBoxSpecies();

        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var openForm = new AddNewAnimal();


            openForm.ChangeToFromEditMode(true);
            openForm.ChangeCurrent(currentId);
            openForm.AddEditDetailsIfTrue();

            openForm.ShowDialog();

            LoadInitialDataToDataGrid();
        }

        private void AddVetVisitButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentId != 0)
            {
                var openForm = new AddVetVisit();

                openForm.UpdateCurrentId(currentId);

                openForm.ShowDialog();

                LoadInitialDataToDataGrid();
            }

        }

        private void GoToVetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isUsed == "False")
            {
                var openForm = new AddVetVisit();

                openForm.UpdateCurrentId(currentId);
                openForm.ChangeVisitId(visitId);
                openForm.ChangeToFromEditMode(true);
                openForm.AddEditDetailsIfTrue();

                openForm.ShowDialog();

                LoadInitialDataToDataGrid();
            }
            else
            {
                ResultText.Text = "Kan inte gå på samma besök igen";
            }

        }
        #endregion

        #region Delete

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            try
            {
                dataAccess.DeleteAnimal(currentId);

                AnimalGrid.ItemsSource = dataAccess.LoadAnimals();

                SetGridToNullAndRefresh();
            }
            catch (Exception)
            {

                ResultText.Text = "Välj ett djur i listan";
            }


        }

        private void DeleteParentsBtn_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            try
            {
                dataAccess.DeleteParent(currentId, parentName);

                ParentGrid.ItemsSource = dataAccess.LoadAnimalsParent(currentId);
            }
            catch (Exception)
            {

                ResultText.Text = "Välj en förälder i listan";
            }
        }

        private void DeleteChildrenBtn_Click(object sender, RoutedEventArgs e)
        {
            var dataAccess = new DataAccess();

            try
            {
                dataAccess.DeleteChild(currentId, childName);

                ChildrenGrid.ItemsSource = dataAccess.LoadAnimalsChildren(currentId);
            }
            catch (Exception)
            {

                ResultText.Text = "Välj ett barn i listan";
            }
        }

        private void DeleteVetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isUsed == "False")
            {
                var dataAccess = new DataAccess();

                try
                {
                    dataAccess.DeleteVetBooking(currentId, visitId);

                    VetGrid.ItemsSource = dataAccess.LoadAnimalsVet(currentId);
                }
                catch (Exception)
                {

                    ResultText.Text = "Välj ett besök i listan";
                }
            }
            else
            {
                ResultText.Text = "Kan inte avboka om redan besökt";
            }


        }



        #endregion

        #region CellsChanged

        private void ChangeButtonState()
        {
            //AddButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            EditButton.IsEnabled = false;
            DeleteChildrenBtn.IsEnabled = false;
            DeleteParentsBtn.IsEnabled = false;
            DeleteVetBtn.IsEnabled = false;
            AddVetVisitButton.IsEnabled = false;
            GoToVetBtn.IsEnabled = false;
        }

        private void ParentGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //var dataAccess = new DataAccess();

                object item = ParentGrid.SelectedItem;

                if (item != null)
                {
                    string id = (ParentGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    parentName = id;

                    ResultText.Text = $"Vald förälder = {parentName}";

                    ChangeButtonState();

                    DeleteParentsBtn.IsEnabled = true;
                    
                }
            }
            catch (Exception)
            {

                ResultText.Text = "Välj en förälder i listan";
            }

        }

        private void AnimalGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var dataAccess = new DataAccess();

                object item = AnimalGrid.SelectedItem;

                if (item != null)
                {
                    string id = (AnimalGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    currentId = int.Parse(id);

                    if (dataAccess.CheckAnimalsParent(currentId) > 0)
                    {
                        ParentGrid.ItemsSource = dataAccess.LoadAnimalsParent(currentId);
                    }

                    if (dataAccess.CheckAnimalsChildren(currentId) > 0)
                    {
                        ChildrenGrid.ItemsSource = dataAccess.LoadAnimalsChildren(currentId);
                    }

                    if (dataAccess.CheckAnimalsVet(currentId) > 0)
                    {
                        VetGrid.ItemsSource = dataAccess.LoadAnimalsVet(currentId);
                    }

                    ResultText.Text = $"Valt djurId = {currentId}";

                    ChangeButtonState();

                    AddButton.IsEnabled = true;
                    DeleteButton.IsEnabled = true;
                    EditButton.IsEnabled = true;
                    AddVetVisitButton.IsEnabled = true;
                    
                }
            }
            catch (Exception)
            {

                ResultText.Text = "Välj ett djur i listan";
            }

        }

        private void ChildrenGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //var dataAccess = new DataAccess();

                object item = ChildrenGrid.SelectedItem;

                if (item != null)
                {
                    string id = (ChildrenGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    childName = id;

                    ResultText.Text = $"Valt barn = {childName}";

                    ChangeButtonState();

                    DeleteChildrenBtn.IsEnabled = true;
                }
            }
            catch (Exception)
            {

                ResultText.Text = "Välj ett barn i listan";
            }

        }

        private void VetGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //var dataAccess = new DataAccess();

                object item = VetGrid.SelectedItem;

                if (item != null)
                {
                    string id = (VetGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    isUsed = (VetGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;

                    visitId = int.Parse(id);

                    ResultText.Text = $"Valt besöksId = {visitId}";

                    ChangeButtonState();

                    DeleteVetBtn.IsEnabled = true;
                    GoToVetBtn.IsEnabled = true;
                }
            }
            catch (Exception)
            {

                ResultText.Text = "Välj ett besök i listan";
            }
        }

        #endregion


    }
}
