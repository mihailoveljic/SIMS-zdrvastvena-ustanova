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
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows
{
    /// <summary>
    /// Interaction logic for HolidayRequestWindow.xaml
    /// </summary>
    public partial class HolidayRequestFormWindow : Window, INotifyPropertyChanged
    {
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

        public HolidayRequest HolidayRequest { get; set; }
        public int ConstructorCheck { get; set; }
        public HolidayRequestsReviewWindow HolidayRequestsReviewWindow { get; set; }
        public ProfileAndPersonalDataWindow ProfileAndPersonalDataWindow { get; set; }
        public HolidayRequestFormWindow(ProfileAndPersonalDataWindow profileAndPersonalDataWindow)
        {
            InitializeComponent();
            DataContext = this;
            ConstructorCheck = 0;
            ProfileAndPersonalDataWindow = profileAndPersonalDataWindow;
        }
        public HolidayRequestFormWindow(HolidayRequestsReviewWindow holidayRequestsReviewWindow, ProfileAndPersonalDataWindow profileAndPersonalDataWindow)
        {
            InitializeComponent();
            DataContext = this;
            ConstructorCheck = 1;
            HolidayRequestsReviewWindow = holidayRequestsReviewWindow;
            ProfileAndPersonalDataWindow = profileAndPersonalDataWindow;
        }

        private void Button_Click_Submit_HolidayRequest(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            //if (startDate_datePicker.SelectedDate == null || endDate_datePicker.SelectedDate == null)
            //{
            //    MessageBox.Show("Morate uneti vremenski interval!");
            //    return;
            //}
            DateTime? startDate = startDate_datePicker.SelectedDate;
            DateTime? endDate = endDate_datePicker.SelectedDate;
            string cause = causeForHoliday_textBox.Text;

            //if (endDate<=startDate)
            //{
            //    MessageBox.Show("Morate uneti validan vremenski period!");
            //    return;
            //}
            //if(startDate.Year==DateTime.Now.Year)
            //{
            //    if (startDate.Month == DateTime.Now.Month)
            //    {
            //        if(startDate.Day - 2 <= DateTime.Now.Day)
            //        {
            //            MessageBox.Show("Morate zakazati odmor minimum 3 dana ranije!");
            //            return;
            //        }
            //    }
            //}
            bool ValidationDateReturnValue = app.HolidayRequestController.ValidateDateFromHolidayRequestForm(startDate, endDate);
            if (!ValidationDateReturnValue)
            {
                startDate_datePicker.BorderBrush = Brushes.Red;
                endDate_datePicker.BorderBrush = Brushes.Red;
                return;
            }
            else
            {
                startDate_datePicker.BorderBrush = Brushes.Gray;
                endDate_datePicker.BorderBrush = Brushes.Gray;
            }
            bool isUrgent = (bool)isUrgent_comboBox.IsChecked;
            Doctor myDoctor = (Doctor)app.LoggedInUser;
            if(!app.HolidayRequestController.CheckIfSomeOtherSpecialistHasRequestAtTheTime(isUrgent, myDoctor, startDate, endDate))
            {
                return;
            }
            //if (!isUrgent)
            //{
            //    List<HolidayRequest> holidayRequestsFromBase = app.HolidayRequestController.GetAll().ToList();
            //    foreach (HolidayRequest hr in holidayRequestsFromBase)
            //    {
            //        if (hr.Doctor.Specialty.Id == myDoctor.Specialty.Id)
            //        {
            //            if ((hr.StartDate <= startDate && startDate <= hr.EndDate) || (hr.StartDate <= endDate && endDate <= hr.EndDate) || (hr.StartDate <= startDate && endDate <= hr.EndDate) || (startDate<=hr.StartDate && hr.EndDate<=endDate))
            //            {
            //                if (hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ONHOLD || hr.HolidayRequestStatus == zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ACCEPTED)
            //                {
            //                    MessageBox.Show("Nazalost trenutno ne mozete zakazati odmor. " +
            //                   "Kolega vase specijalnosti je vec zatrazio odmor u tom vremenskom periodu.");
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
            HolidayRequest = new HolidayRequest(cause, (DateTime)startDate, (DateTime)endDate, zdravstvena_ustanova.Model.Enums.HolidayRequestStatus.ONHOLD, isUrgent, myDoctor, "");
            app = Application.Current as App;
            app.HolidayRequestController.Create(HolidayRequest);
            if(ConstructorCheck==0)
            {
                this.Close();
                return;
            }
            var refreshedHolidayRequests = ProfileAndPersonalDataWindow.RefreshPropertyHolidayRequests(HolidayRequestsReviewWindow);
            var holidayRequestsReviewWindowNew = new HolidayRequestsReviewWindow(refreshedHolidayRequests, ProfileAndPersonalDataWindow);
            holidayRequestsReviewWindowNew.ShowDialog();
            HolidayRequestsReviewWindow.Close();
            this.Close();
        }

        private void Button_Click_Cancel_HolidayRequest(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Da li ste sigurni?", "Checkout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                if (HolidayRequestsReviewWindow == null)
                {
                    this.Close();
                    return;
                }
                //var refreshedHolidayRequests = ProfileAndPersonalDataWindow.RefreshPropertyHolidayRequests(HolidayRequestsReviewWindow);
                //var holidayRequestsReviewWindowNew = new HolidayRequestsReviewWindow(refreshedHolidayRequests, ProfileAndPersonalDataWindow);
                //HolidayRequestsReviewWindow.Close();
                //holidayRequestsReviewWindowNew.ShowDialog();
                this.Close();
            }
            
        }

        private void startDate_datePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if(startDate_datePicker.SelectedDate < DateTime.Now)
            {
                startDate_datePicker.BorderBrush = Brushes.Red;
                startDate_datePicker.ToolTip = "Ne mozete zakazivati termin u proslost!";
                startDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else if(startDate_datePicker.SelectedDate < DateTime.Now.AddDays(3))
            {
                startDate_datePicker.BorderBrush = Brushes.Red;
                startDate_datePicker.ToolTip = "Morate zatraziti odmor miniumum 3 dana ranije!";
                startDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else if (endDate_datePicker.SelectedDate < startDate_datePicker.SelectedDate)
            {
                startDate_datePicker.BorderBrush = Brushes.Red;
                startDate_datePicker.ToolTip = "Da l sam pos'o il sam dos'o... :|";
                startDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                startDate_datePicker.BorderBrush = Brushes.Gray;
                startDate_datePicker.ToolTip = "This field is required!";
                startDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }

        private void endDate_datePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (endDate_datePicker.SelectedDate < DateTime.Now)
            {
                endDate_datePicker.BorderBrush = Brushes.Red;
                endDate_datePicker.ToolTip = "Ne mozete zakazivati termin u proslost!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else if (endDate_datePicker.SelectedDate < startDate_datePicker.SelectedDate)
            {
                endDate_datePicker.BorderBrush = Brushes.Red;
                endDate_datePicker.ToolTip = "Da l sam pos'o il sam dos'o... :|";
                endDatePreventErrorTextBlock.Visibility = Visibility.Visible;
                submitButton.IsEnabled = false;
            }
            else
            {
                endDate_datePicker.BorderBrush = Brushes.Gray;
                endDate_datePicker.ToolTip = "This field is required!";
                endDatePreventErrorTextBlock.Visibility = Visibility.Hidden;
                CheckIfCanEnableSubmitButton();
            }
        }
        public void CheckIfCanEnableSubmitButton()
        {
            if (startDate_datePicker.SelectedDate < DateTime.Now || startDate_datePicker.SelectedDate < DateTime.Now.AddDays(3) ||
                startDate_datePicker.SelectedDate > endDate_datePicker.SelectedDate || endDate_datePicker.SelectedDate<DateTime.Now)
            {
                submitButton.IsEnabled = false;
            }
            else
            {
                submitButton.IsEnabled = true;
            }
        }
    }
}
