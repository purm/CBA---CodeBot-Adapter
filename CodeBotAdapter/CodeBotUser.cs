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
    /// <summary>
    /// Stellt einen Benutzer/Account auf der Codebot Seite dar
    /// </summary>
    public class CodeBotUser {
        /// <summary>
        /// Name
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password {
            get;
            set;
        }

        /// <summary>
        /// WBB specific ID
        /// </summary>
        public int ID {
            get;
            private set;
        }
    }
}
