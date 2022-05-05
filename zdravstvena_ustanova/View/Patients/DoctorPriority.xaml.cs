﻿using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace zdravstvena_ustanova.View
{
    public partial class DoctorPriority : Window
    {
        public List<Doctor> doctors;
        public DoctorPriority()
        {
            InitializeComponent();
            var app = Application.Current as App;
            List<string> names = new List<string>();
            doctors = new List<Doctor>(app.DoctorController.GetAll());
            int i = 0;
            foreach (Doctor doctor in doctors)
            {
                names.Add(doctor.Name + " " + doctor.Surname);
                i++;
            }
            doctorComboBox.ItemsSource = names;
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void createAppointment(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            string dat = (string)list.SelectedItem;
            DateTime date = Convert.ToDateTime(dat);
            DateTime startDate = date;
            DateTime endDate = date.AddHours(1);
            long docId = 0;
            long docRoom = 0;
            foreach (Doctor d in doctors)
            {
                if (startDate.Hour < 14 && d.Shift == Shift.FIRST)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
                else if (startDate.Hour >= 14 && d.Shift == Shift.SECOND)
                {
                    docId = d.Id;
                    docRoom = d.Room.Id;
                    break;
                }
            }

            var scheduledAppointment = new ScheduledAppointment(startDate, endDate, AppointmentType.REGULAR_APPOINTMENT, app.LoggedInUser.Id, docId, docRoom);
            scheduledAppointment = app.ScheduledAppointmentController.Create(scheduledAppointment);
            scheduledAppointment = app.ScheduledAppointmentController.GetById(scheduledAppointment.Id);
            this.Close();
        }

        private void changeList(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string value = (string)doctorComboBox.SelectedItem;
            Shift docS = Shift.THIRD;
            foreach (Doctor d in doctors)
            {
                if (value.Contains(d.Name + " " + d.Surname))
                {
                    docS = d.Shift;
                    break;
                }
            }
            DateTime today = DateTime.Now;
            today = today.AddMinutes(-today.Minute);
            today = today.AddHours(1);
            if (today.Hour >= 20)
            {
                today = new DateTime(today.Year, today.Month, today.Day + 1, 7, 0, 0);
            }
            if (today.Hour <= 7)
            {
                today = new DateTime(today.Year, today.Month, today.Day, 7, 0, 0);
            }
            int days = DateTime.DaysInMonth(2022, DateTime.Now.Month);
            int to = today.Day + 4;
            if (to > days) { to -= days; }
            ObservableCollection<string> dates = new ObservableCollection<string>();
            while (true)
            {
                if (today.Hour == 21) { today = today.AddDays(1); today = today.AddHours(-14); }
                if (today.Day == to) break;
                if(today.Hour < 14 && docS == Shift.FIRST)
                {
                    dates.Add(today.ToString("dd.MM.yyyy. HH:mm"));
                }
                else if (today.Hour >= 14 && docS == Shift.SECOND)
                {
                    dates.Add(today.ToString("dd.MM.yyyy. HH:mm"));
                }
                today = today.AddHours(1);
            }
            var app = Application.Current as App;
            List<ScheduledAppointment> sa = new List<ScheduledAppointment>(app.ScheduledAppointmentController.GetAll());
            foreach (ScheduledAppointment sapp in sa)
            {
                dates.Remove(sapp.Start.ToString("dd.MM.yyyy. HH:mm"));
            }
            foreach (ScheduledAppointment sapp in sa)
            {
                if (sapp.Patient.Id == app.LoggedInUser.Id)
                {
                    for (int i = 7; i < 10; i++)
                    {
                        dates.Remove(sapp.Start.ToString("dd.MM.yyyy. 0" + i + ":mm"));
                    }
                    for (int i = 10; i < 21; i++)
                    {
                        dates.Remove(sapp.Start.ToString("dd.MM.yyyy. " + i + ":mm"));
                    }
                }
            }
            list.ItemsSource = dates;
            Ok1.IsEnabled = false;
        }

        private void chosen(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Ok1.IsEnabled = true;
        }
    }
}
