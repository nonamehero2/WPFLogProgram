#region Using Directives
using LogClasses;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Threading.Tasks;
using System;
using System.Windows.Input;
#endregion

#region Main Code
namespace WPF_Log_Program
{
    /// <summary>
    /// This window displays the log class given to it by the calling object.
    /// </summary>
    public partial class LogDisplay : Window
    {
        #region Fields
        private LogListDisplay mainWindow;      //MainWindow object passed.
        private Log logFile;                //Log that is to be displayed.
        private bool isSaved;               //Is the log saved?
        #endregion

        #region Constructor(s)
        public LogDisplay(LogListDisplay mainWindow, Log log, bool isReadOnly)
        {
            this.logFile = log;
            this.mainWindow = mainWindow;

            InitializeComponent();

            this.Height = mainWindow.Height;
            this.Width = mainWindow.Width;

            name_textBox.Text = log.Name;
            date_textBox.Text = log.Date;
            text_textBox.AppendText(log.Text);
            this.isSaved = true;

            if (isReadOnly)
            {
                name_textBox.IsReadOnly = true;
                text_textBox.IsReadOnly = true;
                save_button.IsEnabled = false;
            }
        }

        #endregion

        #region Methods

        //Saves the log
        private void Save()
        {
            logFile.Text = new TextRange(text_textBox.Document.ContentStart, text_textBox.Document.ContentEnd).Text;
            logFile.Name = name_textBox.Text;
            mainWindow.writeFiles();
            isSaved = true;
        }

        //Checks if the log is saved.
        private bool CheckSaved()
        {
            if (!isSaved)
            {
                CustomMessageBox cmb = new CustomMessageBox();
                cmb.ShowDialog();
                MessageBoxResult messageResult = cmb.Result;
                cmb.Close();

                if (messageResult == MessageBoxResult.Yes)
                {
                    Save();
                }
                else if (messageResult == MessageBoxResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Event Handlers
        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Update_Label.Content = "Saved!";
            Task.Delay(2000).ContinueWith(x =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        Update_Label.Content = "";
                    });
                }
            );
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CheckSaved())
            {
                e.Cancel = true;
                return;
            }
            mainWindow.UpdateListBox();
            mainWindow.Show();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isSaved = false;
        }

        #endregion

    }
}
#endregion