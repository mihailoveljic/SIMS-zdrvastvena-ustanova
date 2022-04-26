﻿using System;
using System.Collections.Generic;
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
using Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for ScheduledAppointmentWindow.xaml
    /// </summary>
    public partial class ScheduledAppointmentWindow : Window, INotifyPropertyChanged
    {
        #region NotifyProperties
        private string _patientName;
        public string PatientName
        {
            get
            {
                return _patientName;
            }
            set
            {
                if (value != _patientName)
                {
                    _patientName = value;
                    OnPropertyChanged("PatientName");
                }
            }
        }

            private string _patientSurname;
        public string PatientSurname
        {
            get
            {
                return _patientSurname;
            }
            set
            {
                if (value != _patientSurname)
                {
                    _patientSurname = value;
                    OnPropertyChanged("PatientSurname");
                }
            }
        }

            private string _patientBirthday;
        public string PatientBirthday
        {
            get
            {
                return _patientBirthday;
            }
            set
            {
                if (value != _patientBirthday)
                {
                    _patientBirthday = value;
                    OnPropertyChanged("PatientBirthday");
                }
            }
        }

        private Anamnesis _anamnesis;
        public Anamnesis Anamnesis
        {
            get
            {
                return _anamnesis;
            }
            set
            {
                if (value != _anamnesis)
                {
                    _anamnesis = value;
                    OnPropertyChanged("Anamnesis");
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
        public ScheduledAppointmentWindow(ScheduledAppointment selectedAppointment)
        {
            InitializeComponent();
            DataContext = this;
            PatientName = selectedAppointment.Patient.Name;
            PatientSurname = selectedAppointment.Patient.Surname;
            PatientBirthday = selectedAppointment.Patient.DateOfBirth.ToString();
            Anamnesis = new Anamnesis(-1);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMedicineToTherapy addMedicineToTherapy = new AddMedicineToTherapy();
            addMedicineToTherapy.Show();
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddMedicineToTherapy addMedicineToTherapy = new AddMedicineToTherapy();
            addMedicineToTherapy.Show();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AnamnesisTextBoxInput ad = new AnamnesisTextBoxInput(this, "Anamnesis Diagnosis");
            ad.Owner = this;
            ad.Show();
            
        }

        private void Button_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            AnamnesisTextBoxInput ad = new AnamnesisTextBoxInput(this, "Anamnesis Conclusion");
            ad.Owner = this;
            ad.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            Anamnesis = app.AnamnesisController.Create(Anamnesis);
            Close();

            // TODO Sve iz medical examination
        }
    }
}
