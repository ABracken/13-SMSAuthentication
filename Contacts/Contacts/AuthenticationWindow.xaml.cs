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
using System.Windows.Shapes;
using Twilio;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {

        public string randomNumber;

        public AuthenticationWindow()
        {
            InitializeComponent();
        }
        private void button_Phone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();

                decimal rnumber = random.Next(100000, 999999);

                randomNumber = (rnumber).ToString();


                string phoneNumber = textBoxPhoneNumber.Text;

                string sendNumber = "+1" + phoneNumber;

                if (phoneNumber.Length == 10)
                {

                    if (!phoneNumber.All(char.IsDigit))
                    {
                        textBoxPhoneNumber.Clear();
                        MessageBox.Show("Please make sure you enter a correct phone number.");
                        return;
                    }
                    else
                    {
                        textBoxPhoneNumber.Clear();
                        MessageBox.Show("Your code will be sent to " + phoneNumber);
                    }
                }
                string AccountSid = "AC0f346bbdb33ccc68096fb208e97008f0";

                string AuthToken = "e5b714d86cd39aaf9f56c17cb95d103d";

                var twilio = new TwilioRestClient(AccountSid, AuthToken);

                var message = twilio.SendMessage("+19519161591", phoneNumber, "Your random code is " + randomNumber);

                if (message.RestException != null)
                {
                    var error = message.RestException.Message;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void button_Response_Click(object sender, RoutedEventArgs e)
        {
            string code = textBoxResponse.Text;

            if (code != randomNumber)
            {
                MessageBox.Show("This is not a valid code please try again");
            }
            else
            {
                MainWindow newWindow = new MainWindow();

                newWindow.Show();

                Close();


            }
        }

        private void textBoxPhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            var tbox = sender as TextBox;
            tbox.Text = "";
        }

        private void textBoxPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            var tbox = sender as TextBox;
            if (tbox.Text == "")
            {
                tbox.Text = "I.e. 5152345678";
            }
        }
    }
}


