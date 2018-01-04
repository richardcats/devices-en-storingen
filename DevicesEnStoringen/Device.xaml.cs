﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
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

namespace DevicesEnStoringen
{
    /// <summary>
    /// Interaction logic for Storing.xaml
    /// </summary>
    public partial class Device : Window
    {
        DatabaseConnectie conn = new DatabaseConnectie();
        public static ObservableCollection<string> listDeviceTypes;


        public Device(int id)
        {
            InitializeComponent();

            Title = "Device bewerken";

            FillTextBoxes(id);
            lstDeviceType.ItemsSource = FillComboboxDeviceType();
            lstAfdeling.ItemsSource = FillComboboxAfdeling();
            grdOpenstaandeStoringen.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = conn.ShowDataInGridView("SELECT StoringID AS ID, Beschrijving, Date(DatumToegevoegd) AS Datum FROM Storing WHERE DeviceID = '" + id + "' AND Status = 'Open'") });

            cvsRegistreerKnoppen.Visibility = Visibility.Hidden;
            cvsBewerkKnoppen.Visibility = Visibility.Visible;
        }

        public Device()
        {
            InitializeComponent();
            Title = "Device registreren";
            lstDeviceType.ItemsSource = FillComboboxDeviceType();
            lstAfdeling.ItemsSource = FillComboboxAfdeling();

            cvsRegistreerKnoppen.Visibility = Visibility.Visible;
            cvsBewerkKnoppen.Visibility = Visibility.Hidden;
            cvsOpenstaandeStoringen.Visibility = Visibility.Hidden;
            Height = 270;
        }

        private void FillTextBoxes(int id)
        {
            conn.OpenConnection();
            SQLiteDataReader dr = conn.DataReader("SELECT DeviceType.Naam AS DeviceTypeNaam, Device.* FROM Device INNER JOIN DeviceType ON DeviceType.DeviceTypeID = Device.DeviceTypeID WHERE DeviceID='" + id + "'");
            dr.Read();

            txtNaam.Text = dr["Naam"].ToString();
            lstDeviceType.SelectedValue = dr["DeviceTypeNaam"].ToString();
            lstAfdeling.SelectedValue = dr["Afdeling"].ToString();
            txtSerienummer.Text = dr["Serienummer"].ToString();
            txtOpmerkingen.Text = dr["Opmerkingen"].ToString();
        }

        public static ObservableCollection<string> FillComboboxDeviceType()
        {
            listDeviceTypes = new ObservableCollection<string>();
            DatabaseConnectie conn = new DatabaseConnectie();
            conn.OpenConnection();
            SQLiteDataReader dr = conn.DataReader("SELECT Naam FROM DeviceType");

            while (dr.Read())
                listDeviceTypes.Add(dr["Naam"].ToString());

            return listDeviceTypes;
        }

        public static ObservableCollection<string> FillComboboxAfdeling()
        {
            ObservableCollection<string> listAfdelingen = new ObservableCollection<string>();
            DatabaseConnectie conn = new DatabaseConnectie();
            conn.OpenConnection();
            SQLiteDataReader dr = conn.DataReader("SELECT Afdeling FROM Device GROUP BY Afdeling");

            while (dr.Read())
                listAfdelingen.Add(dr["Afdeling"].ToString());

            return listAfdelingen;
        }

        private void FillDataGrid()
        {
            
        }

        private void AddStoring(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveDevice(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeGridButtonPositionToEnd(object sender, EventArgs e)
        {
            var dgrd = sender as DataGrid;
            {
                var c = dgrd.Columns[0];
                dgrd.Columns.RemoveAt(0);
                dgrd.Columns.Add(c);
            }
        }

        private void ChangeGridButtonPositionToEnd(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void RowButtonClick(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)grdOpenstaandeStoringen.SelectedItems[0];
            Storing storing = new Storing(Convert.ToInt32(row["ID"]));
            storing.Show();
        }
    }
}