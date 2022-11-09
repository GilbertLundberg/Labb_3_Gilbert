using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics.Metrics;
using Path = System.IO.Path;

namespace Labb3_Gilbert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Bokning> bokningsLista = new List<Bokning>();

        public MainWindow()
        {
            InitializeComponent();
            AddExampleBookings();
            SaveToFile();
        }

        private void OpenBookingsFromFile_Click(object sender, RoutedEventArgs e)
        {
            DisplayBookingList();
        }


        public void ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtboxInputName.Text) || string.IsNullOrEmpty(datePicker.Text) || comboboxInputTable.SelectedIndex == -1 || boxInputTime.SelectedIndex == -1)
                {
                    MessageBox.Show("Skriv in alla värden innan du skapar en bokning", "Fel", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var newBokning = new Bokning(txtboxInputName.Text, datePicker.Text, boxInputTime.Text, comboboxInputTable.Text);

                    if (bokningsLista.Any(n => n.Date == newBokning.Date && n.Time == newBokning.Time && n.Tablenumber == newBokning.Tablenumber))
                    {
                        MessageBox.Show("En bokning för detta datum, tid och bord finns redan. ", "Fel", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (bokningsLista.Count(x => x.Date == newBokning.Date && x.Time == newBokning.Time) >= 5)
                    {
                        MessageBox.Show("Fem stycken bord är redan bokade med samma datum och tid. ", "För många bokningar på samma tid och datum", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        bokningsLista.Add(newBokning);
                        SaveToFile();
                        DisplayBookingList();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade. " + ex.Message, "Exception:", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        async void DisplayBookingList()
        {
            try
            {

                showBookings.Items.Clear();
                using (StreamReader reader = new StreamReader(@"Bordsbokningar.txt"))
                {

                    for (string line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
                    {
                        showBookings.Items.Add(line);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade. " + ex.Message, "Exception:", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }


        public void CancelBooking_Click(object sender, RoutedEventArgs e)
        {
            if (showBookings.SelectedItem == null)
            {
                MessageBox.Show("Välj en bokning i listan för att avboka den", "Fel", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Är du säker på att du vill avboka bokningen: \"{showBookings.SelectedItem}\" ?", "Avbokning", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    bokningsLista.RemoveAt(showBookings.SelectedIndex);
                    SaveToFile();
                    DisplayBookingList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ett oväntat fel inträffade. " + ex.Message, "Exception:", MessageBoxButton.OK, MessageBoxImage.Warning);
                }





            }
        }

       void AddExampleBookings()
        {
            try
            {
                bokningsLista.Add(new Bokning("Nils", "2022-12-09", "22:30", "4"));
                bokningsLista.Add(new Bokning("Gilbert", "2022-12-24", "18:00", "8"));
                bokningsLista.Add(new Bokning("Viktoria", "2023-01-09", "20:30", "1"));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade. " + ex.Message, "Exception:", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
          

        }

        async void SaveToFile()
        {
            try
            {
                bokningsLista.Sort((element1, element2) => element1.Date.CompareTo(element2.Date));
                using StreamWriter sw = new StreamWriter(@"Bordsbokningar.txt");
                foreach (var bokning in bokningsLista)
                {
                  await sw.WriteLineAsync(bokning.Name + " " + bokning.Date + " " + bokning.Time + " " + bokning.Tablenumber);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oväntat fel", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


    }
}
