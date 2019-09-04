using Data.Models;
using Microsoft.Win32;
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
using System.Windows.Documents;
using System.Windows.Input;

namespace Presantation.ViewModel
{
    public class AddUserViewModel : INotifyPropertyChanged
    {
        #region Members

        private AddUserView addUserView;
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
            get
            {
                if (userGetAll == null)
                    userGetAll = new User();
                return userGetAll;
            }
            set
            {
                userGetAll = value;
                NotifyPropertyChanged(nameof(UserGetAll));
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        public AddUserViewModel(AddUserView addUserView)
        {
            this.addUserView = addUserView;
            addUserView.Closing += addlist;

            SelectImageUri = "/Presantation;component/image/profile.png";


        }


        #endregion

        #region ICommand

        /// <summary>
        /// AddUserView Formunu tetikleyen Command
        /// </summary>
        private ICommand userCommand;
        private ICommand userImageCommand;
        

        #endregion

        #region Command

        public ICommand UserCommand
        {
            get
            {
                if (userCommand == null)
                    userCommand = new RelayCommand(Save);
                return userCommand;
            }
        }

        public ICommand UserImageCommand
        {
            get
            {
                if (userImageCommand == null)
                    userImageCommand = new RelayCommand(GetUserImage);
                return userImageCommand;
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
        /// ProjectView Formundaki UserViewList Listesine Ekleme Yapan Metod
        /// </summary>
        private void addlist(object sender, CancelEventArgs e)
        {
            ProjectViewModel.UserViewList.Add(new User
            {
                Username = UserGetAll.Username,
                Password = UserGetAll.Password,
                Name = UserGetAll.Name
            });
        }
        /// <summary>
        /// Kullanıcı Ekleme İşlemini Gerçekleştiren Metod
        /// </summary>
        public void Save()
        {
            DatabaseHelper.SaveUser(UserGetAll);
            MessageBox.Show("Added..");
            addUserView.Close();
        }










        private string selectImageUri;
        public string SelectImageUri
        {
            get { return selectImageUri; }
            set
            {
                selectImageUri = value;
                NotifyPropertyChanged(nameof(SelectImageUri));
            }
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                NotifyPropertyChanged(nameof(ImageName));
            }
        }



        public void GetUserImage()
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image documents (.png)|*.png";
            


            if (openFileDialog.ShowDialog()==true)
            {
                ImageName = System.IO.Path.GetFileName(openFileDialog.FileName);
                SelectImageUri = openFileDialog.FileName;

               


            }
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
