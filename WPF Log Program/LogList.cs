#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#endregion

#region Main Code
namespace LogClasses
{
    [Serializable]
    public class LogList : IEnumerable<Log>
    {
        #region Fields
        private List<Log> listLogs; //List of logs
        private int currentId;      //Id of last added object.
        #endregion

        #region Properties
        public int CurrentId
        {
            get
            {
                return currentId;
            }

            set
            {
                currentId = value;
            }
        }

        public List<Log> ListLogs
        {
            get
            {
                return listLogs;
            }

            private set
            {
                listLogs = value;
            }
        }
        #endregion

        #region Constructors
        //Default Constructor
        public LogList()
        {
            this.listLogs = new List<Log>();
            this.CurrentId = 0;
        }

        //Constructor for Reading from file.
        public LogList(List<Log> logList)
        {
            this.listLogs = logList;
            this.currentId = logList.ElementAt(logList.Count - 1).Id + 1;
        }
        #endregion

        #region Methods
        public void Add(Log item)
        {
            item.Id = this.CurrentId;
            currentId++;
            listLogs.Add(item);
        }

        public void Remove(int index)
        {
            listLogs.RemoveAt(index);
        }

        //Sorts logs by date.
        public void Sort()
        {
            listLogs = listLogs.OrderByDescending(x => x.Id).ToList();
        }

        #endregion

        #region Interface Implementations
        public IEnumerator<Log> GetEnumerator()
        {
            return listLogs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listLogs.GetEnumerator();
        }
        #endregion
    }
}
#endregion