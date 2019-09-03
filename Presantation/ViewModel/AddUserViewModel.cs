using PersonClassLibrary.Models;
using Presantation.View;
using Presantation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presantation.ViewModel
{
    public class AddUserViewModel
    {
        private ICommand userCommand;
        private AddUserView addUserView;
        public ProjectViewModel ProjectViewModel { get; set; }

        public AddUserViewModel(AddUserView addUserView )
        {
            this.addUserView = addUserView;
            addUserView.Closing += addlist;
        }

        private void addlist(object sender, CancelEventArgs e)
        {
            ProjectViewModel.UserViewList.Add(new User {
                username = TbUserUsername,
                password = TbUserPassword,
                name = TbUserName
            });
        }

        public ICommand UserCommand
        {
            get
            {
                if (userCommand == null)
                    userCommand = new RelayCommand(SaveUser);
                return userCommand;
            }
        }

        private string username;
        public string TbUserUsername
        {
            get { return username; }
            set { username = value; NotifyPropertyChanged(nameof(TbUserUsername)); }
        }
        
        private string password;
        public string TbUserPassword
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged(nameof(TbUserPassword)); }
        }

        private string name;
        public string TbUserName
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(TbUserName)); }
        }

        
        private void SaveUser()
        {

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "insert into user (username,password,name) values('" + username + "','" + password + "','" + name + "')";
            sqlitecmd.ExecuteNonQuery();
            
            sqlitecon.Close();
            MessageBox.Show("Kayıt Eklendi");


            addUserView.Close();


        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
