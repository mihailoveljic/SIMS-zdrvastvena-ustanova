﻿using Model;
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
    public partial class ScheduleItemTransferFromWarehouse : UserControl, INotifyPropertyChanged
    {
        public DataGrid WarehouseItemsDataGrid { get; set; }
        public Item ItemForTransfer { get; set; }
        public Warehouse Warehouse { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        #region NotifyProperties
        private int _itemCount;
        public int ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                if (value != _itemCount)
                {
                    _itemCount = value;
                    OnPropertyChanged("ItemCount");
                }
            }
        }
        private int _itemsForTransfer;
        public int ItemsForTransfer
        {
            get
            {
                return _itemsForTransfer;
            }
            set
            {
                if (value != _itemsForTransfer)
                {
                    if (value > ItemCount) _itemsForTransfer = ItemCount;
                    else if (value < 0) _itemsForTransfer = 0;
                    else _itemsForTransfer = value;
                    OnPropertyChanged("ItemsForTransfer");
                }
            }
        }
        private Room _destinationRoom;
        public Room DestinationRoom
        {
            get { return _destinationRoom; }
            set
            {
                if (value != _destinationRoom)
                {
                    _destinationRoom = value;
                    OnPropertyChanged("DestinationRoom");
                }
            }
        }
        private DateTime? _transferDate;
        public DateTime? TransferDate
        {
            get { return _transferDate; }
            set
            {
                if (_transferDate != value)
                {
                    _transferDate = value;
                    OnPropertyChanged("TransferDate");
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


        public ScheduleItemTransferFromWarehouse(Warehouse warehouse, DataGrid warehouseItemsDataGrid)
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Rooms = new ObservableCollection<Room>();
            var rooms = app.RoomController.GetAll();
            foreach (var r in rooms)
            {
                Rooms.Add(r);
            }
            StoredItem storedItem = (StoredItem)warehouseItemsDataGrid.SelectedItem;
            Warehouse = warehouse;
            WarehouseItemsDataGrid = warehouseItemsDataGrid;
            ItemForTransfer = storedItem.Item;
            ItemCount = storedItem.Quantity;
            ItemsForTransfer = 0;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            ScheduledItemTransfer scheduledItemTransfer;

            if (DestinationRoom == null || ItemsForTransfer <= 0 || ItemsForTransfer > ItemCount || TransferDate == null)
            {
                MessageBox.Show("Popuni sva polja!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            scheduledItemTransfer =
                new ScheduledItemTransfer(ItemForTransfer, ItemsForTransfer, Warehouse, DestinationRoom, (DateTime)TransferDate);
            

            var app = Application.Current as App;

            // TODO validacija pre Create(scheduledItemTransfer)
            // da se proveri da li je za izabrani item vec zakazano premestanje
            app.ScheduledItemTransferController.ScheduleItemTransferFromRoomToWarehouse(scheduledItemTransfer);

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
