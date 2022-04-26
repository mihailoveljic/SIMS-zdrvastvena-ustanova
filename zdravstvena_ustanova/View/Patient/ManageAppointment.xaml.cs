﻿using Model;
using System.Collections.Generic;
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
            List<Doctor> doctors = new List<Doctor>(app.DoctorController.GetAll());
            foreach (Doctor d in doctors)
            {
                if (d.Id == ScheduledAppointment.Doctor.Id)
                {
                    doctor.Text = d.Name + " " + d.Surname;
                }
            }
            time.Text = ScheduledAppointment.Start.Hour + ":00";
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
