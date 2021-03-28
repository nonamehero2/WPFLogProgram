#region Using Directives
using LogClasses;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
#endregion

#region Main Code
namespace WPF_Log_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogListDisplay : Window
    {
        #region Fields
        public static string SAVED_DATA = "data.dat";   //Filename for the stored logs

        private LogList logs;   //List of logs.
        #endregion

        #region Constructor(s)
        public LogListDisplay()
        {
            InitializeComponent();
            
            FileInfo file = new FileInfo(SAVED_DATA);

            if (file.Exists)
            {
                readFiles();
            }
            else
            {
                file.Create();
                newLogList();
            }


        }
        #endregion

        #region Methods
        private void newLogList()
        {
            logs = new LogList();
        }

        public void writeFiles()
        {
            logs.Sort();
            using (Stream stream = File.Open(SAVED_DATA, FileMode.Create))
            {
                if (logs.Count() > 0)
                {
                    var binFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binFormater.Serialize(stream, logs);
                }
            }
        }

        public void readFiles()
        {
            using (Stream stream = File.Open(SAVED_DATA, FileMode.Open))
            {
                if (stream.Length > 0)
                {
                    var binFormater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    logs = (LogList)binFormater.Deserialize(stream);
                }
                else
                {
                    newLogList();
                }
            }
            UpdateListBox();
        }

        public void UpdateListBox()
        {
            logs.Sort();

            listBox.Items.Clear();

            foreach (Log item in logs)
            {
                listBox.Items.Add(item.ToString());
            }

        }
        private void OpenExistingLog(bool isReadOnly)
        {
            if (listBox.SelectedIndex != -1)
            {
                this.Hide();
                new LogDisplay(this, logs.ElementAt(listBox.SelectedIndex), isReadOnly).Show();
            }
            UpdateListBox();
        }
        #endregion

        #region Event Handlers
        private void ClickNewButton(object sender, RoutedEventArgs e)
        {
            Log newLog = new Log(DateTime.Now, "", "");
            logs.Add(newLog);
            this.Hide();
            new LogDisplay(this, newLog, false).Show();
            UpdateListBox();
        }

        private void ClickDeleteButton(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                logs.Remove(listBox.SelectedIndex);
            }
            writeFiles();
            UpdateListBox();
        }

        private void ClickEditButton(object sender, RoutedEventArgs e)
        {
            OpenExistingLog(false);
        }

        private void DoubleClickListBox(object sender, MouseButtonEventArgs e)
        {
            OpenExistingLog(true);
        }
        #endregion
    }
}
#endregion