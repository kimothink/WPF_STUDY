using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_STUDY.Models;

namespace WPF_STUDY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonTest1_Click(object sender, RoutedEventArgs e)
        {
            labelTest1.Content = "Test";

            User userA = new User();
            userA.Name = "John";
            userA.UserAge = 36;

            User userB = new User();
            userB.Name = "test";
            userB.UserAge = 31;

            List<User> Listuser = new List<User>();

            Listuser.Add(userA);
            Listuser.Add(userB);        

            listview1.ItemsSource = Listuser; 

        }
    }
}
