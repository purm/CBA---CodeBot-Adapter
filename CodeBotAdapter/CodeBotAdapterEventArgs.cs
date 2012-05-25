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

namespace CodeBotAdapter {
    public class OnLoginResponseEventArgs : EventArgs {
        /// <summary>
        /// If true the login succeded, otherwise it failed. In this case see ErrorInformation
        /// </summary>
        public bool Succes {
            get;
            private set;
        }

        //TODO: Define Type
        public object ErrorInformation {
            get;
            private set;
        }

        public OnLoginResponseEventArgs(bool succes) {
            this.Succes = succes;
        }
    }

    public class OnShoutBoxXmlRecievedEventArgs : EventArgs {
        /// <summary>
        /// content of the xml file
        /// </summary>
        public string XmlContent {
            get;
            private set;
        }

        public OnShoutBoxXmlRecievedEventArgs(string xmlContent) {
            this.XmlContent = xmlContent;
        }
    }
}
