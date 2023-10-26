using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_MVVM.Model
{
    public class Book: INotifyPropertyChanged
    {
        private string bookName { get; set; }
        private string autor { get; set; }
        private short acr { get; set; }
        private DateTime age { get; set; }
        private int count { get; set; }
        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged("BookName");
            }
        }
        public string Autor
        {
            get { return autor; }
            set
            {
                autor = value;
                OnPropertyChanged("Autor");
            }
        }
        public short Acr
        {
            get { return acr; }
            set
            {
                acr = value;
                OnPropertyChanged("Acr");
            }
        }
        public DateTime Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }

        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
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
