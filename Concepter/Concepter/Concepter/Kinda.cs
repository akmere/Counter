using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concepter
{
    public class Kinda : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }

        private int _pId;
        public int PId
        {
            get { return _pId; }
            set { _pId = value; OnPropertyChanged("PId"); }
        }


        //private ObservableCollection<Kinda> _projects;
        //public ObservableCollection<Kinda> Projects
        //{
        //    get { return _projects; }
        //    set { _projects = value; OnPropertyChanged("Projects"); }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
