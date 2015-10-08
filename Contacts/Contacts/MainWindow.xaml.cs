using Contacts.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;
using Twilio;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {
        Contact selectedContact;

        public ObservableCollection<Contact> Entries { get; set; }

        private void dataGridContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedContact = (Contact)dataGridContacts.SelectedValue;
        }
        public MainWindow()
        {
            InitializeComponent();

            Entries = new ObservableCollection<Contact>();

            dataGridContacts.ItemsSource = Entries;
        }

        public void RefreshGrid()
        {
            dataGridContacts.Items.Refresh();
        }
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddContact addContact = new AddContact();

            addContact.Owner = this;

            Contact newContact = new Contact();

            addContact.LoadContact(newContact);

            addContact.Show();
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow { Owner = this };

            editWindow.LoadContact(selectedContact);

            editWindow.Show();

        }
        public void ReceiveContact(Contact currentContact)
        {
            Entries.Add(currentContact);
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Entries.Remove(selectedContact);
        }

        private void menuItem_New_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes before starting a new file?", "Warning", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveFileDialog saveFile = new SaveFileDialog();

                    saveFile.OverwritePrompt = true;

                    saveFile.Filter = "Text File|*.txt";

                    saveFile.FileName = "NewFile";

                    if (saveFile.ShowDialog() == true)
                    {
                        string path = "C:\\Documents";

                        string json = JsonConvert.SerializeObject(saveFile);

                        System.IO.File.WriteAllText(saveFile.FileName, json);
                    }
                    break;
                case MessageBoxResult.No:
                    Entries.Clear();
                    break;
            }
        }

        private void menuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Text|*.txt|All|*.*";

            bool? openFile = dialog.ShowDialog();

            if (openFile == true)
            {
                string filename = dialog.FileName;

                string json = File.ReadAllText(filename);

                ObservableCollection<Contact> contactsFromFile = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);

                dataGridContacts.ItemsSource = contactsFromFile;
            }
        }

        private void menutItem_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.OverwritePrompt = true;

            saveFile.Filter = "Text File|*.txt";

            saveFile.FileName = "NewFile";

            if (saveFile.ShowDialog() == true)
            {
                string path = "C:\\Documents";

                string json = JsonConvert.SerializeObject(saveFile);

                System.IO.File.WriteAllText(saveFile.FileName, json);
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save changes to this file before closing?", "Warning", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    SaveFileDialog saveFile = new SaveFileDialog();

                    saveFile.OverwritePrompt = true;

                    saveFile.Filter = "Text File|*.txt";

                    saveFile.FileName = "NewFile";

                    if (saveFile.ShowDialog() == true)
                    {
                        string path = "C:\\Documents";

                        string json = JsonConvert.SerializeObject(saveFile);

                        System.IO.File.WriteAllText(saveFile.FileName, json);
                    }
                    break;
                case MessageBoxResult.No:
                    Environment.Exit(0);
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;

                    
            }


        }
    }
}

