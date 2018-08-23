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
        private string toEmailAddress = "TO@GMAIL.COM";
        private string fromEmailAddress = "FROM@GMAIL.COM";
        private string fromEmailPassword = "FROM_PASSWORD";
        private string messageSubject = "Parking Pass";
        private string visitorName = "JOHN SMITH";

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
                popupMessage("Email Sent to: " + toEmailAddress + ". Thank you.");
            }
            catch (Exception e)
            {
                popupMessage("Error: " + e.Message);
            }
            
        }

        private MailMessage buildEmail()
        {
            MailMessage email = new MailMessage();
            email.To.Add(toEmailAddress);
            email.From = new MailAddress(fromEmailAddress);
            email.Subject = messageSubject;

            string emailOpening = "Hello,";
            string emailIntro = $"I am expecting {visitorName} to request a parking pass for the today.";
            string emailCarDetails = "Car details: COLOR YEAR MAKE MODEL, STATE Plates - LICENSE";
            string emailClose = "Thank you,";
            string emailSignature = "JANE DOE\nApartment 1-1";
            string emailMessage = emailOpening + "\n\n" + emailIntro + "\n\n" + emailCarDetails + "\n\n" + emailClose + "\n" + emailSignature;

            email.Body = emailMessage;
            return (email);
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

