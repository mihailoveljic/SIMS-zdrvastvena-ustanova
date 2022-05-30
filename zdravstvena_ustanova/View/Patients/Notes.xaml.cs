﻿using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zdravstvena_ustanova.View
{
    public partial class Notes : UserControl
    {
        public ObservableCollection<Note> AllNotes { get; set; }
        public Notes()
        {
            InitializeComponent();
            this.refresh();
        }

        private void entered(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NoteDetails nd = new NoteDetails((Note)notesList.SelectedItem);
                nd.ShowDialog();
                this.refresh();
            }
        }

        private void goToCreateNote(object sender, RoutedEventArgs e)
        {
            CreateNote cn = new CreateNote();
            cn.ShowDialog();
            this.refresh();
        }
        private void refresh()
        {
            AllNotes = new ObservableCollection<Note>();
            var app = Application.Current as App;
            List<Note> ns = new List<Note>(app.NoteController.GetAll());
            foreach (Note no in ns)
            {
                if (app.LoggedInUser.Id == no.Patient.Id)
                {
                    AllNotes.Add(no);
                }
            }
            notesList.ItemsSource = AllNotes;
            delete.IsEnabled = false;
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            delete.IsEnabled = true;
        }

        private void goToDeleteNote(object sender, RoutedEventArgs e)
        {
            DeleteNote dn = new DeleteNote((Note)notesList.SelectedItem);
            dn.ShowDialog();
            this.refresh();
        }
    }
}