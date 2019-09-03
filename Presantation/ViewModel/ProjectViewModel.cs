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
    public class ProjectViewModel
    {

        #region Members

        private ProjectView projectView;

        #endregion

        #region Properties

        public ObservableCollection<User> UserViewList { get; set; }

        public User SelectedUser { get; set; }

        public Project SelectedProject { get; set; }

        public ObservableCollection<Project> ProjectViewList { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructors Metodu
        /// </summary>
        /// <param name="projectView"></param>
        public ProjectViewModel(ProjectView projectView)
        {
            UserViewList = SelectUser();
            ProjectViewList = SelectProject();
            this.projectView = projectView;
        }

        #endregion

        #region ICommand

        private ICommand gotoUserView;

        private ICommand gotoProjectView;

        private ICommand getUserListItem;

        private ICommand deleteUserListItem;

        private ICommand getProjectListItem;

        private ICommand deleteProjectListItem;

        #endregion

        #region Commands

        public ICommand GotoUserView
        {
            get
            {
                if (gotoUserView == null)
                    gotoUserView = new RelayCommand(GotoUser);
                return gotoUserView;
            }

        }

        public ICommand GotoProjectView
        {
            get
            {
                if (gotoProjectView == null)
                    gotoProjectView = new RelayCommand(GotoProject);
                return gotoProjectView;
            }

        }

        public ICommand GetUserListItem
        {
            get
            {
                if (getUserListItem == null)
                    getUserListItem = new RelayCommand(GetUserDetail);
                return getUserListItem;
            }

        }

        public ICommand GetProjectListItem
        {
            get
            {
                if (getProjectListItem == null)
                    getProjectListItem = new RelayCommand(GetProjectDetail);
                return getProjectListItem;
            }

        }

        public ICommand DeleteUserListItem
        {
            get
            {
                if (deleteUserListItem == null)
                    deleteUserListItem = new RelayCommand(DeleteUser);
                return deleteUserListItem;
            }

        }

        public ICommand DeleteProjectListItem
        {
            get
            {
                if (deleteProjectListItem == null)
                    deleteProjectListItem = new RelayCommand(DeleteProject);
                return deleteProjectListItem;
            }

        }

        #endregion

        #region Methods

        public ObservableCollection<User> SelectUser()
        {

            ObservableCollection<User> ulist = new ObservableCollection<User>();
            User user;

            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "Select * from user";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();
            while (sqlitedr.Read())
            {
                user = new User
                {
                    id = Convert.ToInt32(sqlitedr["id"]),
                    username = sqlitedr["username"].ToString(),
                    password = sqlitedr["password"].ToString(),
                    name = sqlitedr["name"].ToString()

                };

                ulist.Add(user);
            }
            sqlitecon.Close();

            return ulist;
        }

        public ObservableCollection<Project> SelectProject()
        {
            ObservableCollection<Project> plist = new ObservableCollection<Project>();
            Project project;

            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "Select * from project";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();
            while (sqlitedr.Read())
            {
                project = new Project
                {

                    id = Convert.ToInt32(sqlitedr["id"]),
                    name = sqlitedr["name"].ToString()

                };

                plist.Add(project);
            }
            sqlitecon.Close();

            return plist;
        }

        public void GotoUser()
        {

            using (AddUserView adv = new AddUserView())
            {
                adv.addUserViewModel.ProjectViewModel = this;
                adv.Show();

            }
        }

        public void GotoProject()
        {

            //projectView.Hide();
            using (AddProjectView apv = new AddProjectView())
            {
                apv.addProjectViewModel.ProjectViewModel = this;
                apv.Show();

            }

        }

        public void GetUserDetail()
        {

            if (SelectedUser != null)
            {
                //MessageBox.Show(SelectedUser.id + " - " + SelectedUser.username + " - " + SelectedUser.password + " - " + SelectedUser.name);
                using (UpdateUserView updateUserView = new UpdateUserView())
                {
                    updateUserView.updateUserViewModel.UpdateUser = SelectedUser;
                    updateUserView.updateUserViewModel.ProjectViewModel = this;
                    updateUserView.Show();


                }
            }
            else
            {
                MessageBox.Show("Please Select User..");
            }

        }

        public void GetProjectDetail()
        {

            if (SelectedProject != null)
            {
                MessageBox.Show(SelectedProject.id + " - " + SelectedProject.name);

                using (UpdateProjectView updateProjectView = new UpdateProjectView())
                {
                    updateProjectView.updateProjectViewModel.UpdateProject = SelectedProject;
                    updateProjectView.updateProjectViewModel.ProjectViewModel = this;
                    updateProjectView.Show();

                }
            }
            else
            {
                MessageBox.Show("Please Select Project..");
            }

        }

        public void DeleteUser()
        {
            if (SelectedUser != null)
            {
                string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

                SQLiteConnection sqlitecon;
                SQLiteCommand sqlitecmd;
                sqlitecon = new SQLiteConnection(cs);
                sqlitecon.Open();

                sqlitecmd = sqlitecon.CreateCommand();
                sqlitecmd.CommandText = "delete from user where id ='" + SelectedUser.id + "'";
                SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();

                sqlitecon.Close();

                MessageBox.Show("Silindi");

                UserViewList.Remove(SelectedUser);
            }
            else
            {
                MessageBox.Show("Please Select User..");
            }
        }

        public void DeleteProject()
        {


            if (SelectedProject != null)
            {
                string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

                SQLiteConnection sqlitecon;
                SQLiteCommand sqlitecmd;
                sqlitecon = new SQLiteConnection(cs);
                sqlitecon.Open();

                sqlitecmd = sqlitecon.CreateCommand();
                sqlitecmd.CommandText = "delete from project where id ='" + SelectedProject.id + "'";
                SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();


                sqlitecon.Close();

                MessageBox.Show("Silindi");

                ProjectViewList.Remove(SelectedProject);
            }
            else
            {
                MessageBox.Show("Please Select Project..");
            }
        }

        #endregion

    }

}
