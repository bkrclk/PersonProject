using Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace Presantation.Helper
{
    public class DatabaseHelper
    {

        public readonly string databasePath = @"Data Source=" + Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Presantation\\Database\\DBProject.db;Version=3";
        /// <summary>
        /// User Listesini Select Yapıp Liste olarak Döndüren Metod
        /// </summary>
        public ObservableCollection<User> GetUserList()
        {

            ObservableCollection<User> ulist = new ObservableCollection<User>();
            User user;
            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "Select * from user";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();
            while (sqlitedr.Read())
            {
                user = new User
                {
                    Id = Convert.ToInt32(sqlitedr["id"]),
                    Username = sqlitedr["username"].ToString(),
                    Password = sqlitedr["password"].ToString(),
                    Name = sqlitedr["name"].ToString(),
                    Phone = sqlitedr["phone"].ToString(),
                    Base64Image = sqlitedr["base64Image"].ToString()

                };

                ulist.Add(user);
            }
            sqlitecon.Close();

            return ulist;
        }

        /// <summary>
        /// Project Listesini Select Yapıp Liste olarak Döndüren Metod
        /// </summary>
        public ObservableCollection<Project> GetProjectList()
        {
            ObservableCollection<Project> plist = new ObservableCollection<Project>();
            Project project;


            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "Select * from project";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();
            while (sqlitedr.Read())
            {
                project = new Project
                {

                    Id = Convert.ToInt32(sqlitedr["id"]),
                    Name = sqlitedr["name"].ToString()

                };

                plist.Add(project);
            }
            sqlitecon.Close();

            return plist;
        }

        /// <summary>
        /// User Güncelleme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void UserUpdate(User user)
        {

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();


            sqlitecmd.CommandText = "update user set username='" + user.Username + "', password='" + user.Password + "', name='" + user.Name + "' , phone='" + user.Phone + "' , base64Image='" + user.Base64Image + "' where id ='" + user.Id + "'";
            sqlitecmd.ExecuteNonQuery();


            sqlitecon.Close();


        }


        /// <summary>
        /// User Kaydetme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void SaveUser(User user)
        {

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;

            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "insert into user (username,password,name,phone,base64Image) values('" + user.Username + "','" + user.Password + "','" + user.Name + "','" + user.Phone + "','" + user.Base64Image + "')";
            sqlitecmd.ExecuteNonQuery();

            sqlitecon.Close();



        }

        /// <summary>
        /// Project Kaydetme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void SaveProject(Project project)
        {
            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;

            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "insert into project (name) values('" + project.Name + "')";
            sqlitecmd.ExecuteNonQuery();

            sqlitecon.Close();

        }

        /// <summary>
        /// Project Güncelleme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void ProjectUpdate(Project project)
        {

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();


            sqlitecmd.CommandText = "update project set name='" + project.Name + "' where id ='" + project.Id + "'";
            sqlitecmd.ExecuteNonQuery();


            sqlitecon.Close();


        }

        /// <summary>
        /// User Silme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void DeleteUser(User user)
        {


            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(databasePath);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "delete from user where id ='" + user.Id + "'";
            sqlitecmd.ExecuteNonQuery();

            sqlitecon.Close();




        }

        /// <summary>
        /// Project Silme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void DeleteProject(Project project)
        {
            if (project == null)
            {
                MessageBox.Show("Please Select Project..");
                return;

            }
            else
            {


                SQLiteConnection sqlitecon;
                SQLiteCommand sqlitecmd;
                sqlitecon = new SQLiteConnection(databasePath);
                sqlitecon.Open();

                sqlitecmd = sqlitecon.CreateCommand();
                sqlitecmd.CommandText = "delete from project where id ='" + project.Id + "'";
                sqlitecmd.ExecuteNonQuery();


                sqlitecon.Close();

            }


        }



    }
}

