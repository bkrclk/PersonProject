﻿using Data.Models;
using Presantation.Helper;
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
    /// <summary>
    /// Kullanıcı ve Projelerin Listelendiği View Model
    /// </summary>
    public class ProjectViewModel
    {

        #region Members

        private DatabaseHelper databaseHelper;

        #endregion

        #region Properties
        /// <summary>
        /// Database İşlemlerini Gerçekleştiren Sınıfı Çağıran Properties
        /// </summary>
        public DatabaseHelper DatabaseHelper
        {
            get
            {
                if (databaseHelper == null)
                    databaseHelper = new DatabaseHelper();
                return databaseHelper;
            }

            set { databaseHelper = value; }
        }

        public ObservableCollection<User> UserViewList { get; set; }

        public User SelectedUser { get; set; }

        public Project SelectedProject { get; set; }

        public ObservableCollection<Project> ProjectViewList { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        public ProjectViewModel()
        {
            UserViewList = DatabaseHelper.GetUserList();
            ProjectViewList = DatabaseHelper.GetProjectList();
        }

        #endregion

        #region ICommand

        /// <summary>
        /// UserView Formunu tetikleyen Command
        /// </summary>
        private ICommand gotoUserView;

        /// <summary>
        /// ProjectView Formunu tetikleyen Command
        /// </summary>
        private ICommand gotoProjectView;

        /// <summary>
        /// User Listesini UserView Formuna Göndermesini tetikleyen Command
        /// </summary>
        private ICommand getUserListItem;

        /// <summary>
        /// User Listesinden Silme İşlemini Tetikleyen Command
        /// </summary>
        private ICommand deleteUserListItem;

        /// <summary>
        /// Project Listesini UserView Formuna Göndermesini tetikleyen Command
        /// </summary>
        private ICommand getProjectListItem;

        /// <summary>
        /// Project Listesinden Silme İşlemini Tetikleyen Command
        /// </summary>
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
                    deleteUserListItem = new RelayCommand(UserDelete);
                return deleteUserListItem;
            }

        }

        public ICommand DeleteProjectListItem
        {
            get
            {
                if (deleteProjectListItem == null)
                    deleteProjectListItem = new RelayCommand(ProjectDelete);
                return deleteProjectListItem;
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Kullanıcı Formunu Getiren Metod
        /// </summary>       
        public void GotoUser()
        {

            using (AddUserView addUserView = new AddUserView())
            {

                addUserView.addUserViewModel.ProjectViewModel = this;
                addUserView.Show();

            }
        }

        /// <summary>
        /// Proje Formunu Getiren Metod
        /// </summary>
        public void GotoProject()
        {

            using (AddProjectView addProjectView = new AddProjectView())
            {
                addProjectView.addProjectViewModel.ProjectViewModel = this;

                addProjectView.Show();

            }

        }

        /// <summary>
        /// ProjectView daki User Listesini UpdateUserView Formuna Gönderen Metod
        /// </summary>
        public void GetUserDetail()
        {

            if (SelectedUser != null)
            {

                using (UpdateUserView updateUserView = new UpdateUserView())
                {
                    updateUserView.updateUserViewModel.UserGetAll = SelectedUser;
                    updateUserView.updateUserViewModel.ProjectViewModel = this;
                    updateUserView.Show();


                }
            }
            else
            {
                MessageBox.Show("Please Select User..");
            }

        }

        /// <summary>
        /// ProjectView daki Project Listesini UpdateProjectView Formuna Gönderen Metod
        /// </summary>
        public void GetProjectDetail()
        {

            if (SelectedProject != null)
            {
                
                using (UpdateProjectView updateProjectView = new UpdateProjectView())
                {
                    updateProjectView.updateProjectViewModel.ProjectGetAll = SelectedProject;
                    updateProjectView.updateProjectViewModel.ProjectViewModel = this;
                    updateProjectView.Show();

                }
            }
            else
            {
                MessageBox.Show("Please Select Project..");
            }

        }


        /// <summary>
        /// Proje Silme İşlemnini Gerçekleştiren Metod
        /// </summary>
        public void ProjectDelete()
        {
            if (SelectedProject != null)
            {
                DatabaseHelper.DeleteProject(SelectedProject);
                MessageBox.Show("Deleted..");
                ProjectViewList.Remove(SelectedProject);
            }
            else
            {
                MessageBox.Show("Please Select Project..");
            }
        }

        /// <summary>
        /// User Silme İşlemnini Gerçekleştiren Metod
        /// </summary>
        public void UserDelete()
        {
            if (SelectedUser != null)
            {
                DatabaseHelper.DeleteUser(SelectedUser);
                MessageBox.Show("Deleted..");
                UserViewList.Remove(SelectedUser);
            }
            else
            {
                MessageBox.Show("Please Select User..");
            }

        }

        #endregion

    }

}
