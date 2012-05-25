using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Diagnostics;

namespace CodeBotApp {
    public partial class MainPage : PhoneApplicationPage {
        // Constructor
        public MainPage() {
            InitializeComponent();
        }

        CodeBotAdapter.CodeBotAdapter session = new CodeBotAdapter.CodeBotAdapter();

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {
            session.OnLoginResponse += new CodeBotAdapter.CodeBotAdapter.OnLoginResponseEventHandler(session_OnLoginRequestFinished);
            session.OnShoutBoxXmlRecieved += new CodeBotAdapter.CodeBotAdapter.OnShoutBoxXmlRecievedEventHandler(session_OnShoutBoxXmlRecieved);

            session.Login(new CodeBotAdapter.CodeBotUser() {
                Name = AccountInformation.Name,
                Password = AccountInformation.Password
            });
        }

        void session_OnShoutBoxXmlRecieved(object sender, CodeBotAdapter.OnShoutBoxXmlRecievedEventArgs eventArgs) {
            this.asdf.Text = eventArgs.XmlContent;
        }

        void session_OnLoginRequestFinished(object sender, CodeBotAdapter.OnLoginResponseEventArgs eventArgs) {
            System.Diagnostics.Debug.WriteLine("Login " + (eventArgs.Succes ? "succeded" : "failed"));

            session.GetShoutBoxXml();
        }
    }
}