using PersonClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presantation.Helper
{
    public class DatabaseHelper
    {
        /// <summary>
        /// User Listesini Select Yapıp Liste olarak Döndüren Metod
        /// </summary>
        public ObservableCollection<User> GetUserList()
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
                    Id = Convert.ToInt32(sqlitedr["id"]),
                    Username = sqlitedr["username"].ToString(),
                    Password = sqlitedr["password"].ToString(),
                    Name = sqlitedr["name"].ToString()

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
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();


            sqlitecmd.CommandText = "update user set username='" + user.Username + "', password='" + user.Password + "', name='" + user.Name + "' where id ='" + user.Id + "'";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();


            sqlitecon.Close();


        }


        /// <summary>
        /// User Kaydetme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void SaveUser(User user)
        {

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();
            sqlitecmd.CommandText = "insert into user (username,password,name) values('" + user.Username + "','" + user.Password + "','" + user.Name + "')";
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
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            sqlitecon = new SQLiteConnection(cs);
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
            string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

            SQLiteConnection sqlitecon;
            SQLiteCommand sqlitecmd;
            sqlitecon = new SQLiteConnection(cs);
            sqlitecon.Open();

            sqlitecmd = sqlitecon.CreateCommand();


            sqlitecmd.CommandText = "update project set name='" + project.Name + "' where id ='" + project.Id + "'";
            SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();


            sqlitecon.Close();


        }

        /// <summary>
        /// User Silme İşlemini Gerçekleştiren Veritabanı Metodu
        /// </summary>
        public void DeleteUser(User user)
        {
           
                string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

                SQLiteConnection sqlitecon;
                SQLiteCommand sqlitecmd;
                sqlitecon = new SQLiteConnection(cs);
                sqlitecon.Open();

                sqlitecmd = sqlitecon.CreateCommand();
                sqlitecmd.CommandText = "delete from user where id ='" + user.Id + "'";
                SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();

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
                string cs = @"Data Source=C:\Users\COMPUTER\Documents\Visual Studio 2017\Projects\PersonProject\PersonClassLibrary\Database\DBProject.db;Version=3";

                SQLiteConnection sqlitecon;
                SQLiteCommand sqlitecmd;
                sqlitecon = new SQLiteConnection(cs);
                sqlitecon.Open();

                sqlitecmd = sqlitecon.CreateCommand();
                sqlitecmd.CommandText = "delete from project where id ='" + project.Id + "'";
                SQLiteDataReader sqlitedr = sqlitecmd.ExecuteReader();


                sqlitecon.Close();

            }


        }


       
    }
}

