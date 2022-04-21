using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using ContactsBook.Models;
using SQLite;

namespace ContactsBook
{
    /// In this class we present the list of contacts
  
    public partial class MainWindow : Window
    {
        List<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();

            ReadDatabase();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {

                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


       

        private void NwContactViewButton_Click(object sender, RoutedEventArgs e)
        {

            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();

        }

        private void ReadDatabase() {

          

            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                 contacts = (connection.Table<Contact>().ToList()).OrderBy(c => c.Username).ToList();

            }

            if (contacts != null) {

               contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;

            var filteredContacts = contacts.Where(c => c.Username.ToLower().Contains(searchBox.Text) ||  c.Location.ToLower().Contains(searchBox.Text) || c.BirthDay.Contains(searchBox.Text) ||
             c.Email.ToLower().Contains(searchBox.Text) || c.WorkPhone.ToLower().Contains(searchBox.Text) || c.HomePhone.Contains(searchBox.Text) || c.Notes.ToLower().Contains(searchBox.Text)
                ).ToList();

            contactsListView.ItemsSource = filteredContacts;    
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedtContact =  (Contact)contactsListView.SelectedItem;
            if (selectedtContact != null) { 
            ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedtContact);
                contactDetailsWindow.ShowDialog();
            }
            ReadDatabase();
        }
    }
}
