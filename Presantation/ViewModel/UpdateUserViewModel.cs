using Data.Models;
using Microsoft.Win32;
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

        private string base64Image;

        public string Base64Image
        {
            get { return base64Image; }
            set
            {
                base64Image = value;
                NotifyPropertyChanged(nameof(Base64Image));
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
        #endregion

        #region Constructors




        /// <summary>
        /// Sınıf Oluşturulğunda ilk Çalışan Metod
        /// </summary>
        public UpdateUserViewModel(UpdateUserView updateUserView, User user)
        {
            this.updateUserView = updateUserView;
            //UserGetAll = user;
            UserGetAll = new User()
            {
                Id = user.Id,
                Username =user.Username,
                Password = user.Password,
                Name = user.Name,
                Phone = user.Phone,
                Base64Image= user.Base64Image
                
            };
            Base64Image = UserGetAll.Base64Image;
            
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
        public void UpdateList()
        {

            for (int i = 0; i < ProjectViewModel.UserViewList.Count; i++)
            {
                if (ProjectViewModel.UserViewList[i].Id.Equals(UserGetAll.Id))
                {
                    ProjectViewModel.UserViewList[i] = UserGetAll;
                }
            }
        }
      

        /// <summary>
        /// Kullanıcı Güncelleme İşlemini Gerçekleştiren Metod
        /// </summary>
        private void Update()
        {
            DatabaseHelper.UserUpdate(UserGetAll);
            UpdateList();
            MessageBox.Show("Updated..");
            updateUserView.Close();
        }
        private ICommand userImageCommand;
        public ICommand UserImageCommand
        {
            get
            {
                if (userImageCommand == null)
                    userImageCommand = new RelayCommand(GetUserImage);
                return userImageCommand;
            }
        }

        public void GetUserImage()
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image Document (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";



            if (openFileDialog.ShowDialog() == true)
            {
                ImageName = System.IO.Path.GetFileName(openFileDialog.FileName);
                UserGetAll.Base64Image = Base64ImageConverter(openFileDialog.FileName);
                Base64Image = UserGetAll.Base64Image;
            }
        }
        private string Base64ImageConverter(string filepath)
        {

            byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
            string base64Image = Convert.ToBase64String(imageArray);

            return base64Image;

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
