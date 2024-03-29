﻿using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.Controls;
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.ManagerMVVM.View;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for ManagerItemsPage.xaml
    /// </summary>
    public partial class ManagerItemsPage : Page
    {
        public ObservableCollection<ItemModel> ItemViewModels { get; set; }

        public ManagerItemsPage()
        {
            InitializeComponent();
            ItemViewModels = new ObservableCollection<ItemModel>();
            DataContext = this;

            var app = Application.Current as App;

            var items = app.ItemController.GetAll();

            foreach (var item in items)
            {
                var totalCount = app.StoredItemController.GetTotalItemCount(item);
                ItemModel itemViewModel = new ItemModel(item, totalCount);
                ItemViewModels.Add(itemViewModel);
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;
                // Use the brush to paint the button's background.
                searchTextBox.Background = textImageBrush;
            }
            else
            {

                searchTextBox.Background = null;
            }
        }

        private void AddIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddItemView(ItemViewModels); 
            MainWindow.Modal.IsOpen = true;
        }

        private void EditIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow.Modal.Content = new EditItemControl(ItemViewModels, ItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void DeleteIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new DeleteItemControl(ItemViewModels, ItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem == null)
            {
                DeleteIcon.IsEnabled = false;
                EditIcon.IsEnabled = false;
            }
            else
            {

                DeleteIcon.IsEnabled = true;
                EditIcon.IsEnabled = true;
            }
        }
    }
}
