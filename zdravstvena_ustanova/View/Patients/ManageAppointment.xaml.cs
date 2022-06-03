﻿using zdravstvena_ustanova.Model;
using System;
using System.Windows;


namespace zdravstvena_ustanova.View
{
    public partial class ManageAppointment : Window
    {
        public ScheduledAppointment ScheduledAppointment { get; set; }
        public ManageAppointment(ScheduledAppointment sa)
        {
            InitializeComponent();
            ScheduledAppointment = sa;
            var app = Application.Current as App;
            Doctor doc = app.DoctorController.GetById(ScheduledAppointment.Doctor.Id);
            doctor.Text = doc.Name + " " + doc.Surname;
            time.Text = ScheduledAppointment.Start.Hour + ":00";
            if(!(DateTime.Compare(ScheduledAppointment.Start.Date,DateTime.Now.Date)<=0 || DateTime.Compare(ScheduledAppointment.Start.Date, DateTime.Now.AddDays(1).Date) == 0))
            {
                change.IsEnabled = true;
                delete.IsEnabled = true;
            }
            else
            {
                change.IsEnabled = false;
                delete.IsEnabled = false;
            }
        }

        private void goToChangeAppointment(object sender, RoutedEventArgs e)

        {
            this.Close();
            ChangeAppointment ca = new ChangeAppointment(ScheduledAppointment);
            ca.ShowDialog();
        }

        private void goToDeleteAppointment(object sender, RoutedEventArgs e)
        {
            DeleteAppointment da = new DeleteAppointment(this, ScheduledAppointment);
            da.ShowDialog();
        }

        private void goToAppointments(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
