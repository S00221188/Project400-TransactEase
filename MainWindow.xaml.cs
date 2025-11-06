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
using Project400_TransactEase.Models;

namespace Project400_TransactEase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employee loggedInEmployee;  
        public MainWindow(Employee employee)
        {
            InitializeComponent();
            loggedInEmployee = employee;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logout feature not implemented yet.");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings feature not implemented yet.");
        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bill feature not implemented yet."); //Implement a split bill option here or elsewhere
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clear feature not implemented yet.");
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pay feature not implemented yet."); // Cash Card or voucher
        }

        private void Tabs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tabs feature not implemented yet.");
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Filter feature not implemented yet.");
        }
    }
}
