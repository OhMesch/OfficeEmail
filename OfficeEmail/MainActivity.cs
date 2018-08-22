using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net.Mail;
using Android.Graphics.Drawables;

namespace OfficeEmail
{
    [Activity(Label = "OfficeEmail", MainLauncher = true, Icon = "@drawable/officeEmailMan")]
    public class MainActivity : Activity
    {
        private string toEmailAddress = "TO_EMAIL@gmail.com";
        private string fromEmailAddress = "FROM_EMAIL@gmail.com";
        private string fromEmailPassword = "FROM_PASSWORD";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            initalizeButtons();
        }

        private void initalizeButtons()
        {
            Button sendOfficeEmailButton = FindViewById<Button>(Resource.Id.sendEmailB);
            sendOfficeEmailButton.Click += sendParkingEmail;
        }

        private void sendParkingEmail(object sender, EventArgs args)
        {
            MailMessage email = buildEmail();
            SmtpClient gmailClient = buildSmptClient();
            try
            {
                gmailClient.Send(email);
                popupMessage("Email Sent. Thank you.");
            }
            catch(Exception e)
            {
                popupMessage("Error: " + e.Message);
            }
            
        }

        private MailMessage buildEmail()
        {
            Random rand = new Random();
            MailMessage email = new MailMessage();
            email.To.Add(toEmailAddress);
            email.From = new MailAddress(fromEmailAddress);
            email.Subject = "Sent from an app " + rand.Next(1001).ToString();
            email.Body = "Testing";
            return email;
        }

        private SmtpClient buildSmptClient()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, fromEmailPassword);
            return (client);
        }

        private void popupMessage(string message)
        {
            Toast.MakeText(ApplicationContext, message, ToastLength.Long).Show();
        }
    }
}

