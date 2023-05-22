using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_STUDY.ViewModels;

namespace WPF_STUDY
{
    /// <summary>
    /// ICommand 추가 
    /// </summary>
    class TestClickCommand : ICommand
    {
        private bool rtnCan = true;

        private MainVIewModel _mainVIewModel;


        public event EventHandler? CanExecuteChanged;

        public TestClickCommand( MainVIewModel mainVIewModel)
        {
            _mainVIewModel = mainVIewModel;
        }

            public bool CanExecute(object? parameter)
        {

            return rtnCan;
        }

        public void Execute(object? parameter)
        {
            rtnCan = false;

            MessageBox.Show(_mainVIewModel.ProgressValue+"");
        }
    }
}
