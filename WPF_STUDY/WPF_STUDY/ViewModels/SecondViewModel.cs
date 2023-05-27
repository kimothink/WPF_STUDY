using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF_STUDY.ViewModels
{
    public class SecondViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private int progressValue;

        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                NotifyPropertyChanged(nameof(ProgressValue));
            }
        }
    }
}

