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
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for AddItemControl.xaml
    /// </summary>
    public partial class AddItemControl : UserControl
    {
        public ObservableCollection<ItemModel> ItemViewModels;

        public AddItemControl(ObservableCollection<ItemModel> itemViewModel)
        {
            InitializeComponent();
            DataContext = this;
            ItemViewModels = itemViewModel;
            var app = Application.Current as App;

            ItemTypeComboBox.ItemsSource = app.ItemTypeController.GetAll();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemNameTextBox.Text == "" || ItemDescriptionTextBox.Text == "" || ItemTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            string itemName = ItemNameTextBox.Text;
            string itemDescription = ItemDescriptionTextBox.Text;

            var item = new Item(itemName, itemDescription, (ItemType)ItemTypeComboBox.SelectedItem);

            item = app.ItemController.Create(item);

            ItemViewModels.Add(new ItemModel(item, 0));
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
