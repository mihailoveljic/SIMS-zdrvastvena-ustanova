﻿using Model;
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
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    public partial class UpdateMedicineInTherapy : Window
    {
        public ObservableCollection<Medication> Medications { get; set; }
        public PrescribedMedicine PrescribedMedicineSelected { get; set; }
        public ObservableCollection<PrescribedMedicine> PrescribedMedicine { get; set; }
        public UpdateMedicineInTherapy(ObservableCollection<PrescribedMedicine> pms, PrescribedMedicine pm)
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Medications = new ObservableCollection<Medication>(app.MedicationController.GetAll());
            PrescribedMedicine = pms;
            PrescribedMedicineSelected = pm;
            medComboBox.ItemsSource = Medications;
            medComboBox.Text = pm.Medication.Name;
            medIdTextBox.Text = pm.Medication.Id.ToString();
            timesPerDay.Text = pm.TimesPerDay.ToString();
            quantity.Text = timesPerDay.Text;
            onHours.Text = pm.OnHours.ToString();
            endDate.Text = pm.EndDate.Date.ToString();
            description.Text = pm.Description;
            
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medication med = (Medication)medComboBox.SelectedItem;
            medIdTextBox.Text = med.Id.ToString();
        }
        private void updatePrescribedMedicine(object sender, RoutedEventArgs e)
        {
            Medication med = (Medication)medComboBox.SelectedItem;
            int tpd = int.Parse(timesPerDay.Text);
            int oh = int.Parse(onHours.Text);
            DateTime ed = (DateTime)endDate.SelectedDate;
            string desc = description.Text;
            foreach (PrescribedMedicine pmPomocni in PrescribedMedicine)
            {
                if (pmPomocni.Id == PrescribedMedicineSelected.Id)
                {
                    PrescribedMedicine.Remove(pmPomocni);
                    break;
                }
            }
            PrescribedMedicine.Add(new PrescribedMedicine(PrescribedMedicineSelected.Id, tpd, oh, ed, desc, med));
            this.Close();
        }
        private void Button_Click_Cancel_Updating_Medicine_In_Therapy(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni da zelite da ponistite izmene?", "Izmena terapije", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}