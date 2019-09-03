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
  
    public class AddProjectViewModel
    {
        private ICommand projectCommand;
        private AddProjectView addProjectView;
        public ProjectViewModel ProjectViewModel { get; set; }


        public AddProjectViewModel(AddProjectView addProjectView)
        {
       
            this.addProjectView = addProjectView;
            addProjectView.Closing += addlist;
        }

        private void addlist(object sender, CancelEventArgs e)
        {
            ProjectViewModel.ProjectViewList.Add(new Project {

                name = TbProjectName
                
            });
        }


        

        public ICommand ProjectCommand
        {
            get
            {
                if (projectCommand == null)
                    projectCommand = new RelayCommand(SaveProject);
                return projectCommand;
            }
        }
        private string projectname;
        public string TbProjectName
        {
            get { return projectname; }
            set { projectname = value; NotifyPropertyChanged(nameof(TbProjectName)); }
        }

        private void SaveProject()
        {
            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "insert into project (name) values('" + projectname + "')";
            sqlitecmd.ExecuteNonQuery();
            
            sqlitecon.Close();

            MessageBox.Show("Kayıt Eklendi");


            addProjectView.Close();
         


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
