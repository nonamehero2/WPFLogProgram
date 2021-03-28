#region Using Directives
using System;
#endregion

#region Main Code
namespace LogClasses
{
    [Serializable]
    public class Log
    {
        #region Fields
        private int id;     //ID for log.
        DateTime date;      //Date for log.
        string name;        //Title of log.
        string text;        //Main body of log.
        #endregion

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Date
        {
            get
            {
                return date.ToString("MMM dd, yyyy - hh:mm tt");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }
        #endregion

        #region Constructor(s)
        public Log(DateTime date, string name, string text)
        {
            this.id = -1;
            this.date = date;
            this.Name = name;
            this.Text = text;
        }
        #endregion

        #region Methods

        //This override is mainly used to display the log in a gui.
        public override string ToString()
        {
            return String.Format("{0} | {1}", this.date.ToString("MM/dd/yyyy"), this.Name);
        }
        #endregion

    }
}
#endregion
