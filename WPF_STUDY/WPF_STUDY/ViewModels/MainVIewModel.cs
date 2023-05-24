using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_STUDY.Views;

namespace WPF_STUDY.ViewModels
{
    class MainVIewModel : INotifyPropertyChanged
    {
        private int progressValue;
        public ICommand TestClick { get; set; }

        public ICommand SecondShow { get; set; }


        public MainVIewModel()
        {
            //TestClick = new RelayCommand<object>(ExecuteMyButton,CanMyButton);
            TestClick = new AsyncRelayCommand(ExecuteMyButton2);
            SecondShow = new AsyncRelayCommand(ExecuteMyButton3);

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
        private void NotifyPropertyChanged([CallerMemberName] String PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
