using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ContactsBook.Models;
using Microsoft.Win32;
using SQLite;
using System.Data;

namespace ContactsBook
{
    /// In this class we create a new contact by getting all data from the required fields
    
    public partial class NewContactWindow : Window
    {
        DataSet ds;
        string imageName;
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact()
            {
                Username = txtUsernameBox.Text,
                Email = txtEmailBox.Text,
                HomePhone = txtHomePhoneBox.Text,
                WorkPhone = txtWorkPhoneBox.Text,
                Location = txtLocationBox.Text,
                Notes = txtNotesBox.Text,
                BirthDay = dpDOB.SelectedDate.Value.ToShortDateString(),
                imageUrl = ""
            };

         

           using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Contact>();
                connection.Insert(contact);
            }

            this.Close();

        }


        private void txtInput_Keydown(object sender, KeyEventArgs e)
        {
            btnSave.IsEnabled = true;
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnImageUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex= 1;
            if (openFileDialog.ShowDialog() == true) { 
                    imProfile.Source =  new BitmapImage(new Uri(openFileDialog.FileName));
            }

          
        }

       
    }
}
