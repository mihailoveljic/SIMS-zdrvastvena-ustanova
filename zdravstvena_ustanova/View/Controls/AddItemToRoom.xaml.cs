﻿using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    public partial class AddItemToRoom : UserControl
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<ItemViewModel> ItemViewModels { get; set; }
        public Room Room { get; set; }
        public Warehouse Warehouse { get; set; }
        public DataGrid RoomItemsDataGrid { get; set; }
        public AddItemsNewInputControl AddItemsNewInputControl { get; set; }
        public AddItemFromWarehouseControl AddItemFromWarehouseControl { get; set; }

        public AddItemToRoom(Room room, DataGrid roomItemsDataGrid)
        {
            ItemViewModels = new ObservableCollection<ItemViewModel>();
            Items = new ObservableCollection<Item>();
            InitializeComponent();
            DataContext = this;

            AddItemsNewInputControl = new AddItemsNewInputControl();
            TransferCountPanel.Children.Add(AddItemsNewInputControl);
            var app = App.Current as App;

            Room = room;
            Warehouse = app.WarehouseController.GetAll().SingleOrDefault();
            RoomItemsDataGrid = roomItemsDataGrid;


            Items = new ObservableCollection<Item>(app.ItemController.GetAll());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewInput.IsChecked == true)
            {
                if (ItemsSearchBox.SelectedItem == null || AddItemsNewInputControl.ItemsForTransfer <= 0)
                {
                    MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                if (ItemsSearchBox.SelectedItem == null || AddItemFromWarehouseControl.ItemsForTransfer <= 0)
                {
                    MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            var app = Application.Current as App;

            Item item = (Item)ItemsSearchBox.SelectedItem;
            int quantity;
            if (NewInput.IsChecked == true)
            {
                quantity = AddItemsNewInputControl.ItemsForTransfer;
            }
            else
            {
                quantity = AddItemFromWarehouseControl.ItemsForTransfer;
            }

            if (NewInput.IsChecked == true)
            {
                StoredItem storedItem = new StoredItem(item, quantity, StorageType.ROOM, Room);
                storedItem = app.StoredItemController.Create(storedItem);
                foreach (StoredItem si in Room.StoredItems)
                {
                    if (si.Id == storedItem.Id)
                    {
                        storedItem.Item = si.Item;
                        storedItem.Room = si.Room;
                        Room.StoredItems.Remove(si);
                        break;
                    }
                }
                Room.StoredItems.Add(storedItem);
            }
            else
            {
                app.StoredItemController.MoveItemFromTo(Warehouse, Room, item, quantity);

            }
            CollectionViewSource.GetDefaultView(RoomItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void WarehouseInput_Checked(object sender, RoutedEventArgs e)
        {
            ItemViewModels.Clear();
            Items.Clear();
            if (TransferCountPanel is not null)
            {
                TransferCountPanel.Children.Clear();
                AddItemFromWarehouseControl = new AddItemFromWarehouseControl();
                TransferCountPanel.Children.Add(AddItemFromWarehouseControl);
            }
            foreach (var storedItem in Warehouse.StoredItems)
            {
                ItemViewModels.Add(new ItemViewModel(storedItem.Item, storedItem.Quantity));
                Items.Add(storedItem.Item);
            }
        }

        private void NewInput_Checked(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            ItemViewModels.Clear();
            Items.Clear();
            if (TransferCountPanel is not null)
            {
                TransferCountPanel.Children.Clear();
                AddItemsNewInputControl = new AddItemsNewInputControl();
                TransferCountPanel.Children.Add(AddItemsNewInputControl);
            }
            foreach (var item in app.ItemController.GetAll())
            {
                Items.Add((Item)item);
            }
        }

        private void ItemsSearchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WarehouseInput.IsChecked == true && ItemsSearchBox.SelectedItem is not null)
            {
                AddItemFromWarehouseControl.ItemCount = Warehouse.StoredItems.SingleOrDefault(storedItem =>
                            storedItem.Item.Id == ((Item)ItemsSearchBox.SelectedItem).Id).Quantity;
                AddItemFromWarehouseControl.ItemsForTransfer = 0;
            }
            
        }
    }
}
