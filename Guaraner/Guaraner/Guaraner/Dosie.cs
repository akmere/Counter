using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guaraner
{
    public class Dosie : INotifyPropertyChanged
    {
        private string _date;
        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }

        private string _hour;
        public string Hour
        {
            get { return _hour; }
            set { _hour = value; OnPropertyChanged("Hour"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
