using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeBotAdapter {
    public class CodeBotAdapter {
        #region Events

        public delegate void OnLoginResponseEventHandler(object sender, OnLoginResponseEventArgs eventArgs);
        public delegate void OnShoutBoxXmlRecievedEventHandler(object sender, OnShoutBoxXmlRecievedEventArgs eventArgs);

        /// <summary>
        /// Triggerd when logging in finished
        /// </summary>
        public event OnLoginResponseEventHandler OnLoginResponse;

        /// <summary>
        /// Triggered when the Shoutbox Xml recieves
        /// </summary>
        public event OnShoutBoxXmlRecievedEventHandler OnShoutBoxXmlRecieved;

        #endregion

        #region Members

        HtmlAgilityPack.HtmlDocument _document;
        RestClient _restClient;

        #endregion

        #region Properties

        public List<CodeBotShoutBoxEntry> ShoutBoxEntries;

        #endregion

        #region Constants

        /// <summary>
        /// Useragent provided with the Http Stuff
        /// </summary>
        const string _USER_AGENT = "CodeBotAdapter for Windows Phone - Development Build 1";

        //Codebot Page Urls
        public const String _PAGE_BASE    = "http://www.codebot.de/";
        public const String _PAGE_LOGIN = "index.php?form=UserLogin";
        public const String _PAGE_LOGOUT = "index.php?action=UserLogout";
        public const String _PAGE_SBLIST = "index.php?page=ShoutboxEntryXMLList";
        public const String _PAGE_SBPOST = "index.php?action=ShoutboxEntryAdd";
        public const String _PAGE_USERSON = "index.php?page=UsersOnline";

        //Http Parameter Fields
        private const String _USER_FIELD = "loginUsername";
        private const String _PASS_FIELD = "loginPassword";
        private const String _COOKIES_FIELD = "useCookies";
        private const String _LOGIN_KEEP = "1";

        #endregion

        /// <summary>
        /// Standard Constructor
        /// </summary>
        public CodeBotAdapter() {
            this._restClient = new RestSharp.RestClient(_PAGE_BASE);
            this._restClient.UserAgent = _USER_AGENT;

            this.ShoutBoxEntries = new List<CodeBotShoutBoxEntry>();
            this._document = new HtmlAgilityPack.HtmlDocument();

            Debug.WriteLine("CodeBotAdapter Instance was created");
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~CodeBotAdapter() {
            Debug.WriteLine("CodeBotAdapter Instance was deleted");
        }

        /// <summary>
        /// Tries to log into an account
        /// </summary>
        /// <param name="loginData">the account you want to log in with</param>
        public void Login(CodeBotUser loginData) {
            RestRequest request = new RestRequest(_PAGE_LOGIN, Method.POST);
            request.AddParameter(_USER_FIELD, loginData.Name);
            request.AddParameter(_PASS_FIELD, loginData.Password);
            request.AddParameter(_COOKIES_FIELD, _LOGIN_KEEP);

            this._restClient.ExecuteAsync(request, LoginCallback);
        }

        /// <summary>
        /// Gets called when logging in finished
        /// </summary>
        /// <param name="response"></param>
        void LoginCallback(IRestResponse response) {
            this._document.LoadHtml(response.Content);

            if (this.OnLoginResponse != null) {
                this.OnLoginResponse(this, new OnLoginResponseEventArgs(true));
            }
        }

        public void GetShoutBoxXml() {
            RestRequest request = new RestRequest(_PAGE_SBLIST);

            this._restClient.ExecuteAsync(request, ShoutBoxXmlCallback);
        }

        void ShoutBoxXmlCallback(IRestResponse response) {
            XDocument doc = XDocument.Parse(response.Content);

            this.ShoutBoxEntries.Clear();

            foreach (XElement element in doc.Descendants()) {
                if (element.Name == "entry") {
                    string name = string.Empty;
                    string message = string.Empty;
                    DateTime time = DateTime.Now;
                    int id = 0;
                    int userId = 0;
                    foreach (XElement el in element.Descendants()) {
                        if (el.Name == "entryID") {
                            id = Convert.ToInt32(el.Value);
                        } else if (el.Name == "userID") {
                            userId = Convert.ToInt32(el.Value);
                        } else if (el.Name == "time") {
                            time = Utils.TimestampToDate(Convert.ToInt64(el.Value));
                        } else if (el.Name == "message") {
                            message = el.Value;
                        } else if (el.Name == "username") {
                            name = el.Value;
                        }
                    }

                    Debug.WriteLine(time.ToString() + " - " + name + ": " + message);
                    this.ShoutBoxEntries.Add(new CodeBotShoutBoxEntry(id, userId, name, time, message));
                }
            }

            if (this.OnShoutBoxXmlRecieved != null) {
                this.OnShoutBoxXmlRecieved(this, new OnShoutBoxXmlRecievedEventArgs(response.Content));
            }
        }
    }
}
