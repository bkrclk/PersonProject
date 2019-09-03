using PersonClassLibrary.Models;
using Presantation.View;
using Presantation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presantation.ViewModel
{
    public class UpdateUserViewModel
    {
        private ICommand userUpdateCommand;
        private UpdateUserView updateUserView;
        public ProjectViewModel ProjectViewModel { get; set; }



        public UpdateUserViewModel(UpdateUserView updateUserView)
        {
            this.updateUserView = updateUserView;
            updateUserView.Closing += updateList;

        }
        private void updateList(object sender, CancelEventArgs e)
        {
           
            foreach (var item in ProjectViewModel.UserViewList)
            {
                if(item.id == UpdateUser.id)
                {
                    item.username = UpdateUser.username;
                    item.password = UpdateUser.password;
                    item.name = UpdateUser.name;
                }
            }       
        }

        private User updateUser;

        public User UpdateUser
        {
            get { return updateUser; }
            set
            {
                updateUser = value;
                NotifyPropertyChanged(nameof(UpdateUser));
            }
        }




        public ICommand UserUpdateCommand
        {
            get
            {
                if (userUpdateCommand == null)
                    userUpdateCommand = new RelayCommand(Update);
                return userUpdateCommand;
            }

        }

        public void Update()
        {
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            

            sqlitecmd.CommandText = "update user set username='" + UpdateUser.username + "', password='" + UpdateUser.password + "', name='" + UpdateUser.name + "' where id ='" + UpdateUser.id + "'";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();
            

            sqlitecon.Close();

            MessageBox.Show("Updated..");
            updateUserView.Close();

            
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
