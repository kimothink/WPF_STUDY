using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_STUDY.Models;
using WPF_STUDY.Views;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace WPF_STUDY.ViewModels
{
    class MainVIewModel : INotifyPropertyChanged
    {
        private int progressValue;
        public ICommand TestClick { get; set; }

        public ICommand SecondShow { get; set; }

        public ICommand SelectClick { get; set; }

        public ICommand InsertClick { get; set; }


        private List<USERINFO> myLiseUser = new List<USERINFO>();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                NotifyPropertyChanged(nameof(Name));

            }
        }


        private string img;

        public string Img
        {
            get { return img; }
            set { img = value;
                NotifyPropertyChanged(nameof(Img));

            }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value;
                NotifyPropertyChanged(nameof(Age));

            }
        }



        public List<USERINFO> MyListUser
        {
            get { return myLiseUser; }
            set { myLiseUser = value;
                NotifyPropertyChanged(nameof(MyListUser));  
            }
        }



        public MainVIewModel()
        {
            //TestClick = new RelayCommand<object>(ExecuteMyButton,CanMyButton);
            TestClick = new AsyncRelayCommand(ExecuteMyButton2);
            
            SecondShow = new AsyncRelayCommand(ExecuteMyButton3);

            SelectClick = new AsyncRelayCommand(SelectDatabase);

            InsertClick = new AsyncRelayCommand(InsertDatabase);


        }
        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                NotifyPropertyChanged(nameof(ProgressValue));
            }
        }

         
        bool CanMyButton(object param)
        {
            if(param == null)
            {
                return true ;   
            }
            return param.ToString().Equals("ABC") ? true : false; 
        }

        void ExecuteMyButton(object param)
        {
            //MessageBox.Show(param.ToString()+"AAA");
            int w = 0;
            Task task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    ProgressValue = i;
                }
            });

            Task rtn2 = Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    ProgressValue = i;
                    w = i;
                    Thread.Sleep(2000);
                }
            });

            rtn2.Wait();
            MessageBox.Show(w + "");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public async Task ExecuteMyButton2()
        {
            int w = 0;
            Task<int> rtn2 = Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    ProgressValue = i;
                    w = i;
                    Thread.Sleep(2000);
                }
                int j = 5;
                return j;
            });

            w = await rtn2;

            MessageBox.Show(w + "");

        }

        public async Task ExecuteMyButton3()
        {
            SecondView secondView = new SecondView();
            SecondViewModel aa = new SecondViewModel();
            aa.ProgressValue = 70;
            secondView.DataContext = aa;
            secondView.ShowDialog();

            await Task.Delay(0);
        }

        public async Task SelectDatabase()
        {
            DataSet ds = new DataSet();
            List<USERINFO> listUserTemp = new List<USERINFO>();
            Exception exectpion = null;


            Task t = Task.Run(() =>
            {
                try
                {
                    using(MySqlConnection sqlConnection = new MySqlConnection(Properties.Settings.Default.connectionString))
                    {
                        sqlConnection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from USERINFO", sqlConnection);
                        adapter.Fill(ds);
                    }

                    if(ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            USERINFO userinfo = new USERINFO();
                            userinfo.USERNAME = dt.Rows[i]["USERNAME"].ToString();
                            userinfo.USERIMG = dt.Rows[i]["USERIMG"].ToString();
                            userinfo.USERAGE = Int32.Parse(dt.Rows[i]["USERAGE"].ToString());

                            listUserTemp.Add(userinfo); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    exectpion = ex;
                }
            });

            await t;

            if (exectpion != null)
            {
                MessageBox.Show(exectpion.Message.ToString());
            }

            MyListUser =    listUserTemp;   
        }

        public async Task InsertDatabase()
        {
            Exception exectpion = null;

            Task t = Task.Run(() =>
            {
                try
                {
                    using (MySqlConnection sqlConnection = new MySqlConnection(Properties.Settings.Default.connectionString))
                    {
                        sqlConnection.Open();
                        MySqlCommand sqlCommand = sqlConnection.CreateCommand();
                        sqlCommand.CommandText = "INSERT INTO USERINFO (USERNAME, USERIMG, USERAGE) VALUES('"+Name+"', '"+Img+"', '"+Age+"');";
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }

                  
                }
                catch (Exception ex)
                {
                    exectpion = ex;
                }
            });

            await t;

            if (exectpion != null)
            {
                MessageBox.Show(exectpion.Message.ToString());
            }

            await SelectDatabase(); 
        }
        private void NotifyPropertyChanged([CallerMemberName] String PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
