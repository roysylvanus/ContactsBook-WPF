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
using System.Drawing;
using SQLite;
using ContactsBook.Models;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;



namespace ContactsBook
{
    //In this class we present data of each selected contact
    //User here is able to update data per specific field and is able to delete it
    public partial class ContactDetailsWindow : Window
    {
        Contact contact;
        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            this.contact = contact;
            txtUsernameBox.Text = contact.Username;
            if (contact.BirthDay != null) {
                dpDOB.SelectedDate = DateTime.Parse(contact.BirthDay);
            }
            txtEmailBox.Text = contact.Email;
            txtHomePhoneBox.Text = contact.HomePhone;
            txtLocationBox.Text = contact.Location;
            txtNotesBox.Text = contact.Notes;   
            txtWorkPhoneBox.Text = contact.WorkPhone;
            
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(contact);
            }

            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) {

            contact.Username = txtUsernameBox.Text;
            contact.Email = txtEmailBox.Text;
            contact.BirthDay = dpDOB.SelectedDate.Value.ToShortDateString();
            contact.Location = txtLocationBox.Text;
            contact.Notes = txtNotesBox.Text;
            contact.HomePhone = txtHomePhoneBox.Text;
            contact.WorkPhone = txtWorkPhoneBox.Text;



            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(contact);
            }

            this.Close();

        }

        private void btnImageUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                imProfile.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        

    }

}
