using Data.Models;
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
    public class AddProjectViewModel : INotifyPropertyChanged
    {
        #region Members

        private AddProjectView addProjectView;

        private DatabaseHelper databaseHelper;

        private Project projectGetAll;

        #endregion

        #region Properties

        public ProjectViewModel ProjectViewModel { get; set; }

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

        /// <summary>
        /// Proje Ekleme ve Güncelleme Formundaki Textboxların Verilerini Çeken Properties
        /// </summary>
        public Project ProjectGetAll
        {
            get
            {
                if (projectGetAll == null)
                    projectGetAll = new Project();
                return projectGetAll;
            }
            set
            {
                projectGetAll = value;
                NotifyPropertyChanged(nameof(ProjectGetAll));
            }
        }

        #endregion

        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        #region Constructor

        public AddProjectViewModel(AddProjectView addProjectView)
        {

            this.addProjectView = addProjectView;
            addProjectView.Closing += addlist;
        }

        #endregion

        #region ICommand
        /// <summary>
        /// AddProjectView Formunu tetikleyen Command
        /// </summary>
        private ICommand projectCommand;

        #endregion

        #region Command

        public ICommand ProjectCommand
        {
            get
            {
                if (projectCommand == null)
                    projectCommand = new RelayCommand(SaveProject);
                return projectCommand;
            }
        }

        #endregion

        #region Event
        /// <summary>
        /// Tüm İşlemleri Gerçekleştiresini Tetikleyen Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Metod
        /// <summary>
        /// ProjectView Formundaki ProjectViewList Listesine Ekleme Yapan Metod
        /// </summary>
        private void addlist(object sender, CancelEventArgs e)
        {
            ProjectViewModel.ProjectViewList.Add(new Project
            {

                Name = ProjectGetAll.Name

            });
        }
        /// <summary>
        /// Proje Ekleme İşlemini Gerçekleştiren Metod
        /// </summary>
        public void SaveProject()
        {
            DatabaseHelper.SaveProject(ProjectGetAll);
            MessageBox.Show("Added..");
            addProjectView.Close();
        }

        /// <summary>
        /// Tüm İşlemlerin Gerçekleşmesini Tetiklenmesini Sağlayan Metod
        /// </summary>
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        #endregion

    }
}
