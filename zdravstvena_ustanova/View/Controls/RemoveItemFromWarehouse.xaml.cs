﻿using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for RemoveItemFromWarehouse.xaml
    /// </summary>
    public partial class RemoveItemFromWarehouse : UserControl, INotifyPropertyChanged
    {
        public DataGrid WarehouseItemsDataGrid { get; set; }
        public ObservableCollection<StoredItem> StoredItems { get; set; }
        #region NotifyProperties
        private StoredItem _storedItem;
        public StoredItem StoredItem
        {
            get { return _storedItem; }
            set
            {
                if (_storedItem != value)
                {
                    _storedItem = value;
                    OnPropertyChanged("StoredItem");
                }
            }
        }
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get { return _warehouse; }
            set
            {
                if (_warehouse != value)
                {
                    _warehouse = value;
                    OnPropertyChanged("Warehouse");
                }
            }
        }
        private int _itemsForTransfer;
        public int ItemsForTransfer
        {
            get { return _itemsForTransfer; }
            set
            {
                if (_itemsForTransfer != value)
                {
                    if (value > ItemCount) _itemsForTransfer = ItemCount;
                    else if (value < 0) _itemsForTransfer = 0;
                    else _itemsForTransfer = value;
                    OnPropertyChanged("ItemsForTransfer");
                }
            }
        }
        private int _itemCount;
        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                if (_itemCount != value)
                {
                    _itemCount = value;
                    OnPropertyChanged("ItemCount");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        public RemoveItemFromWarehouse(DataGrid warehouseItemsDataGrid, ObservableCollection<StoredItem> storedItems, Warehouse warehouse)
        {
            InitializeComponent();
            DataContext = this;

            var app = App.Current as App;

            StoredItems = storedItems;
            Warehouse = warehouse;

            WarehouseItemsDataGrid = warehouseItemsDataGrid;
            StoredItem = (StoredItem)WarehouseItemsDataGrid.SelectedItem;
            ItemCount = StoredItem.Quantity;
            ItemsForTransfer = 0;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsForTransfer <= 0 || ItemsForTransfer > ItemCount)
            {
                MessageBox.Show("Popuni sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = App.Current as App;

            if(ItemsForTransfer == ItemCount)
            {
                app.StoredItemController.Delete(StoredItem.Id);
                StoredItems.Remove(StoredItem);
            }
            else
            {
                StoredItem.Quantity -= ItemsForTransfer;
                app.StoredItemController.Update(StoredItem);
            }

            CollectionViewSource.GetDefaultView(WarehouseItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
