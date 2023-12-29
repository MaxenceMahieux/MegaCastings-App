﻿using MegaCastings.View;
using MegaCastings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MegaCastings.DBLib.Class;

namespace MegaCastings.View
{
    public partial class AddCustomerView : Page
    {
        public ObservableCollection<User> AllUsers { get; set; }
        public ObservableCollection<BigCategory> BigCategories { get; set; }
        public User SelectedUser { get; set; }

        public AddCustomerView()
        {
            InitializeComponent();
            DataContext = this; // Définition du DataContext sur cette instance de la classe
            BigCategories = GetBigCategories();
        }

        private ObservableCollection<BigCategory> GetBigCategories()
        {
            // Implémentez la logique pour obtenir les catégories depuis la base de données
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = prenom.Text;
            string lastName = nom.Text;
            string eMail = email.Text;
            DateTime? selectedDate = birthdate.SelectedDate;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(eMail) && selectedDate.HasValue)
            {
                User newUser = new User
                {
                    Lastname = firstName,
                    Firstname = lastName,
                    Email = eMail,
                    Birthdate = selectedDate.Value
                };

                using (MegaProductionContext context = new MegaProductionContext())
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Utilisateur ajouté avec succès.");
                Main.Content = new CustomerView();
            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom, le nom et sélectionner une date de naissance pour l'utilisateur.");
            }
        }
    }
}
