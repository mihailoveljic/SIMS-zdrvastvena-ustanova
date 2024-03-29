﻿using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    public partial class UpdateAppointment : Window, INotifyPropertyChanged
    {
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public DoctorHomePageWindow DoctorHomePageWindow { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        #region NotifyProperties
        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                if (value != _selectedPatient)
                {
                    _selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                if (value != _selectedRoom)
                {
                    _selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                if (value != _selectedDate)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
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

        public UpdateAppointment(ScheduledAppointment sa, DoctorHomePageWindow dhpw)
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            Patients = new ObservableCollection<Patient>();
            var patients = app.PatientController.GetAll();
            foreach(Patient p in patients)
            {
                Patients.Add(p);
            }
            Rooms = new ObservableCollection<Room>();
            var rooms = app.RoomController.GetAll();
            foreach(Room r in rooms)
            {
                Rooms.Add(r);
            }

            typeOfAppointment.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            ScheduledAppointment = sa;
            DoctorHomePageWindow = dhpw;
            SelectedPatient = new Patient();
            SelectedPatient = sa.Patient;
            SelectedPatient.Name = sa.Patient.Name;
            SelectedPatient.Surname = sa.Patient.Surname;
            SelectedDate = sa.Start.Date;
            if(SelectedDate.Year < DateTime.Now.Year)
            {
                DateOfAppointmentDatePicker.IsEnabled = false;
                TimeComboBox.IsEnabled = false;
            }
            else if(SelectedDate.Month < DateTime.Now.Month)
            {
                DateOfAppointmentDatePicker.IsEnabled = false;
                TimeComboBox.IsEnabled = false;
            }
            else if(SelectedDate.Day < DateTime.Now.Day)
            {
                DateOfAppointmentDatePicker.IsEnabled = false;
                TimeComboBox.IsEnabled = false;
            }
            typeOfAppointment.SelectedItem = sa.AppointmentType;
            SelectedRoom = new Room();
            SelectedRoom = ((Doctor)app.LoggedInUser).Room;
            rComboBox.Text = SelectedRoom.Name;
            TimeComboBox.Text = sa.Start.Hour.ToString();

        }
        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            string selectedTime = ((ComboBoxItem)TimeComboBox.SelectedItem).Content.ToString();
            int startTime = int.Parse(selectedTime);
            int endTime = int.Parse(selectedTime) + 1;
            DateTime startDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, startTime, 0, 0);
            DateTime endDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, endTime, 0, 0);
            //if (SelectedPatient == null)
            //{
            //    if (typeOfAppointment.SelectedItem == null)
            //    {
            //        MessageBox.Show("Morate odabrati pacijenta i tip pregleda!");
            //        return;
            //    }
            //    MessageBox.Show("Morate odabrati pacijenta!");
            //    return;
            //}
            //else if (typeOfAppointment.SelectedItem == null)
            //{
            //    MessageBox.Show("Morate odabrati tip pregleda!");
            //    return;
            //}
            //else
            //{
            //    ScheduledAppointment sa = new ScheduledAppointment(startDate, endDate,
            //     (AppointmentType)typeOfAppointment.SelectedItem, ScheduledAppointment.Id, SelectedPatient.Id, app.LoggedInUser.Id, SelectedRoom.Id);
            //    app.ScheduledAppointmentController.Update(sa);
            //}
            if (app.ScheduledAppointmentController.ValidateForm(startDate, SelectedPatient, typeOfAppointment.SelectedItem.ToString(), startDate.Hour.ToString(), 1))
            {
                ScheduledAppointment sa = new ScheduledAppointment(startDate, endDate,
                (AppointmentType)typeOfAppointment.SelectedItem, ScheduledAppointment.Id, SelectedPatient.Id, app.LoggedInUser.Id, SelectedRoom.Id);
                app.ScheduledAppointmentController.Update(sa);
                DoctorHomePageWindow.UpdateCalendar();
                this.Close();
            }
            
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
