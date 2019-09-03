using PersonClassLibrary.Models;
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
    public class UpdateUserViewModel : INotifyPropertyChanged
    {
        #region Members


        private UpdateUserView updateUserView;
        private DatabaseHelper databaseHelper;
        private User userGetAll;

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
        /// User Ekleme ve Güncelleme Formundaki Textboxların Verilerini Çeken Properties
        /// </summary>
        public User UserGetAll
        {
            get { return userGetAll; }
            set
            {
                userGetAll = value;
                NotifyPropertyChanged(nameof(UserGetAll));
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        public UpdateUserViewModel(UpdateUserView updateUserView)
        {
            this.updateUserView = updateUserView;
            updateUserView.Closing += updateList;

        }

        #endregion

        #region ICommand
        /// <summary>
        /// UpdateUserView Formunu tetikleyen Command
        /// </summary>
        private ICommand userUpdateCommand;

        #endregion

        #region Command

        public ICommand UserUpdateCommand
        {
            get
            {
                if (userUpdateCommand == null)
                    userUpdateCommand = new RelayCommand(Update);
                return userUpdateCommand;
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
        /// ProjectView Formundaki UserViewList Listesinde Güncelleme Yapan Metod
        /// </summary>
        private void updateList(object sender, CancelEventArgs e)
        {

            foreach (var item in ProjectViewModel.UserViewList)
            {
                if (item.Id == userGetAll.Id)
                {
                    item.Username = userGetAll.Username;
                    item.Password = userGetAll.Password;
                    item.Name = userGetAll.Name;
                }
            }
        }
        /// <summary>
        /// Kullanıcı Güncelleme İşlemini Gerçekleştiren Metod
        /// </summary>
        private void Update()
        {
            DatabaseHelper.UserUpdate(userGetAll);
            MessageBox.Show("Updated..");
            updateUserView.Close();
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
