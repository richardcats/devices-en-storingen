﻿using DevicesAndProblems.App.Services;
using DevicesAndProblems.App.Utility;
using DevicesAndProblems.DAL.SQLite;
using System.Windows;

namespace DevicesAndProblems.App.View
{
    public partial class LoginView : Window
    {

        public LoginView()
        {
            InitializeComponent();
        }

        private void btnInloggen_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDataService employeeDataService = new EmployeeDataService(new EmployeeRepository(), txtGebruikersnaam.Text); // The username of the employee will be saved throughout the application

            bool loginDetailsCorrect = employeeDataService.CheckLoginDetails(txtGebruikersnaam.Text, txtWachtwoord.Password); // checks whether the login details are correct

            if (loginDetailsCorrect)
            {
                OverviewView overzicht = new OverviewView();
                overzicht.Show();
                Messenger.Default.Send(employeeDataService);
                Close();
            }
            else
            {
                MessageBox.Show("Gebruikersnaam of wachtwoord is incorrect");
            }
        }
    }
}
