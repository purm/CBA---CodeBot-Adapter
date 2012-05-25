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
    public static class Utils {
        public static void LongDebugOutput(string output) {
            for (int i = 0; i < output.Length; i += 714) {
                System.Diagnostics.Debug.WriteLine(output.Substring(i, (int)Math.Min(714, output.Length - i)));
            }
        }

        public static byte[] StringToByteArray(string str) {
            return System.Text.Encoding.Unicode.GetBytes(str);
        }

        public static string ByteArrayToString(byte[] arr) {
            return System.Text.Encoding.Unicode.GetString(arr, 0, arr.Length);
        }

        public static DateTime TimestampToDate(long Timestamp) {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dateTime.AddSeconds(Timestamp);
        }
    }
}
