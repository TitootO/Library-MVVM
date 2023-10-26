using Library_MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library_MVVM.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private User chosenUser;
        private Book chosenBook;
        private Book chosenUserBook;
        string bookSearch;
        string userSerch;
        public string UserSearch
        {
            get { return userSerch; } 
            set
            {
                userSerch = value;
                OnPropertyChanged(nameof(SearchUsers));
            }
        }
        public ObservableCollection<User> SearchUsers
        {
            get
            {
                if (userSerch == null) return Users;
                return new(Users.Where(x=>x.Name.Contains(userSerch, StringComparison.OrdinalIgnoreCase)||x.Family.Contains(userSerch, StringComparison.OrdinalIgnoreCase)));
            }
        }
        public string BookSearch
        {
            get { return bookSearch; }
            set
            {
                bookSearch = value;
                OnPropertyChanged(nameof(SearchBooks));
            }
        }
        public ObservableCollection<Book> SearchBooks
        {
            get
            {
                if (bookSearch == null) return Books;
                return new(Books.Where(x => x.Acr.ToString().Contains(bookSearch, StringComparison.OrdinalIgnoreCase) || x.BookName.Contains(bookSearch, StringComparison.OrdinalIgnoreCase) || x.Autor.Contains(bookSearch, StringComparison.OrdinalIgnoreCase)));
            }
        }
        public User ChosenUser
        {
            get { return chosenUser; }
            set
            {
                chosenUser = value;
                UpdateUserBooks();
                OnPropertyChanged(nameof(chosenUser));
            }
        }
        public Book ChosenBook
        {
            get { return chosenBook;} 
            set
            {
                chosenBook = value;
                OnPropertyChanged(nameof(chosenBook));
            }
        }
        public Book ChosenUserBook
        {
            get { return chosenUserBook; }
            set
            {
                chosenUserBook = value;
                OnPropertyChanged(nameof(chosenUserBook));
            }
        }


        private RelayCommand giveBook;
        public RelayCommand GiveBook
        {
            get
            {
                return giveBook ??
                  (giveBook = new RelayCommand(obj =>
                  {
                      if (chosenUser != null && chosenBook != null && chosenBook.Count>0)
                      {
                          chosenBook.Count--;
                          chosenUser.BookList.Add(chosenBook.Acr);
                          //UserBooks = new(Books.Where(x => chosenUser.BookList.Contains(x.Acr)));
                          UpdateUserBooks();
                      }
                      else
                          MessageBox.Show("Выберите книгу и пользователя из списка, которые есть в наличии");
                  }));
            }
        }
        private RelayCommand returnBook;
        public RelayCommand ReturnBook
        {
            get
            {
                return returnBook ??
                  (returnBook = new RelayCommand(obj =>
                  {
                      if (chosenUser != null && chosenUserBook != null)
                      {
                          Books.Where(x => x.Acr == chosenUserBook.Acr).First().Count++;
                          chosenUser.BookList.Remove(chosenUserBook.Acr);
                          UpdateUserBooks();
                      }
                      else
                          MessageBox.Show("Выберите книгу и пользователя из списка, которые есть в наличии");
                  }));
            }
        }
        private void UpdateUserBooks()
        {
            UserBooks = new();
            foreach (var i in chosenUser.BookList)
                foreach (var book in Books)
                    if (i == book.Acr)
                    {
                        UserBooks.Add(book);
                        break;
                    }
            OnPropertyChanged(nameof(UserBooks));
        }
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Book> UserBooks { get; set; } = new();
        public ApplicationViewModel() 
        {
            Books = new ObservableCollection<Book>
            {
                new Book{ BookName = "Война и мир", Autor=" Лев Толстой", 
                    Acr = 1 , Age = new(2005, 05, 05), Count=20},
                new Book{ BookName = "1984", Autor="Джордж Оруэлл",
                    Acr = 2 , Age = new(2015, 06, 15), Count=22},
                new Book{ BookName = "Улисс", Autor="Джеймс Джойс",
                    Acr = 3 , Age = new(2020, 05, 05), Count=10},
                new Book{ BookName = "Лолита", Autor="Владимир Набоков",
                    Acr = 4 , Age = new(2017, 10, 05), Count=18},
                new Book{ BookName = "Звук и ярость", Autor="Уильям Фолкнер",
                    Acr = 5 , Age = new(2001, 10, 25), Count=5},
                new Book{ BookName = "Человек-невидимка", Autor="Ральф Эллисон",
                    Acr = 6 , Age = new(2005, 09, 08), Count=13}
            };
            Users = new ObservableCollection<User>
            {
                new User{Id=0, Name = "Алиса", Family= "Беляева", BookList = new()},
                new User{Id=1, Name = "София", Family= "Нестерова", BookList = new()},
                new User{Id=2, Name = "Максим", Family= "Николаев", BookList = new()},
                new User{Id=3, Name = "Давид", Family= "Фомин", BookList = new()},
                new User{Id=4, Name = "Софья", Family= "Кулешова", BookList = new()},
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
