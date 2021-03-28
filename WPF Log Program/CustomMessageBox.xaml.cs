#region Using Directives
using System.Windows;
#endregion

#region Main Code
namespace WPF_Log_Program
{
    /// <summary>
    /// Custom made YesNoCancel messagebox for wpf.
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        #region Fields
        private MessageBoxResult result;
        #endregion
        #region Properties
        public MessageBoxResult Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
        #endregion

        #region Constructor(s)
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        #endregion


        #region Click Event Handlers
        private void yes_button_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Hide();
        }

        private void no_button_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Hide();
        }

        private void cencel_button_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.Hide();
        }
        #endregion
    }
}
#endregion