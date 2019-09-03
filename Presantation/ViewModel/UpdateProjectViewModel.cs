using PersonClassLibrary.Models;
using Presantation.Helper;
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
    public class UpdateProjectViewModel: INotifyPropertyChanged
    {
        
        #region Members

        private UpdateProjectView updateProjectView;
        
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
            get { return projectGetAll; }
            set
            {
                projectGetAll = value;
                NotifyPropertyChanged(nameof(ProjectGetAll));
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        public UpdateProjectViewModel(UpdateProjectView updateProjectView)
        {
            this.updateProjectView = updateProjectView;
            updateProjectView.Closing += updateList;

        }

        #endregion

        #region ICommand
        /// <summary>
        /// UpdateProjectView Formunu tetikleyen Command
        /// </summary>
        private ICommand projectUpdateCommand;

        #endregion

        #region Command

        public ICommand ProjectUpdateCommand
        {
            get
            {
                if (projectUpdateCommand == null)
                    projectUpdateCommand = new RelayCommand(Update);
                return projectUpdateCommand;
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
        /// ProjectView Formundaki ProjectViewList Listesinde Güncelleme Yapan Metod
        /// </summary>
        private void updateList(object sender, CancelEventArgs e)
        {

            foreach (var item in ProjectViewModel.ProjectViewList)
            {
                if (item.Id == ProjectGetAll.Id)
                {
                    item.Name = ProjectGetAll.Name;
                }
            }
        }

        /// <summary>
        /// Proje Güncelleme İşlemini Gerçekleştiren Metod
        /// </summary>
        public void Update()
        {
            DatabaseHelper.ProjectUpdate(ProjectGetAll);
            MessageBox.Show("Updated..");
            updateProjectView.Close();

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

