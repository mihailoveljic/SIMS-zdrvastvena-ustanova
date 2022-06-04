﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows.ViewModel
{
    public class OrderMaterialFormViewModel : INotifyPropertyChanged
    {
        public OrderMaterialFormView OrderMaterialFormView { get; set;}
        public RelayCommand CloseCommand { get; set; }
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
        public OrderMaterialFormViewModel(OrderMaterialFormView orderMaterialFormView)
        {
            OrderMaterialFormView = orderMaterialFormView;
            var app = Application.Current as App;
            CloseCommand = new RelayCommand(param => ExecuteClose());
        }
        private void ExecuteClose()
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                OrderMaterialFormView.Close();
            }        
        }
    }
}
