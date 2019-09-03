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
    public class UpdateProjectViewModel
    {
        private ICommand projectUpdateCommand;
        private UpdateProjectView updateProjectView;
        public ProjectViewModel ProjectViewModel { get; set; }


        public UpdateProjectViewModel(UpdateProjectView updateProjectView)
        {
            this.updateProjectView = updateProjectView;
            updateProjectView.Closing += updateList;

        }
        private void updateList(object sender, CancelEventArgs e)
        {

            foreach (var item in ProjectViewModel.ProjectViewList)
            {
                if (item.id == UpdateProject.id)
                {
                    item.name = UpdateProject.name;
                }
            }
        }

        private Project updateProject;

        public Project UpdateProject
        {
            get { return updateProject; }
            set
            {
                updateProject = value;
                NotifyPropertyChanged(nameof(UpdateProject));
            }
        }




        public ICommand UserUpdateCommand
        {
            get
            {
                if (projectUpdateCommand == null)
                    projectUpdateCommand = new RelayCommand(Update);
                return projectUpdateCommand;
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


            sqlitecmd.CommandText = "update project set name='" + UpdateProject.name + "' where id ='" + UpdateProject.id + "'";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();


            sqlitecon.Close();

            MessageBox.Show("Updated..");
            updateProjectView.Close();


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

