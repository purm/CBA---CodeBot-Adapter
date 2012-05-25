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
    public class CodeBotShoutBoxEntry {
        public int EntryId {
            get;
            private set;
        }

        public int UserId {
            get;
            private set;
        }

        public string UserName {
            get;
            private set;
        }

        public DateTime Time {
            get;
            private set;
        }

        public string Message {
            get;
            private set;
        }

        public CodeBotShoutBoxEntry(int id, int userId, string userName, DateTime time, string message) {
            this.EntryId = id;
            this.UserName = userName;
            this.Time = time;
            this.Message = message;
            this.UserId = userId;
        }
    }
}
