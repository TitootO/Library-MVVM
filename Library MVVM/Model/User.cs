using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_MVVM.Model
{
    public class User : INotifyPropertyChanged
    {
        private int id { get; set; }
        private string name { get; set; }
        private string family { get; set; }
        private List<short> bookList { get; set; }
        public int Id
        {
            get { return  id; } 
            set 
            { 
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChanged("Family");
            }
        }
        public List<short> BookList 
        { 
            get { return bookList; }
            set
            {
                bookList = value;
                OnPropertyChanged("BookList");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
