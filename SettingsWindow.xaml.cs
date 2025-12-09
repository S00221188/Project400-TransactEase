using Project400_TransactEase.Models;
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
using System.Windows.Shapes;

namespace Project400_TransactEase
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        //Issues Ran into: 
        //Role handling, looked up online Role Handling  but it only showed for the Whole Window
        //
        private Employee loggedInEmployee;

        public SettingsWindow(Employee employee)
        {
            InitializeComponent();
            loggedInEmployee = employee;

            //Initial Code for security - Worked for whole window, but Employees will have access to Sales History so needs to be implemented individually.

            //if (employee.Role != "Admin" && employee.Role != "Manager")
            //{
            //    MessageBox.Show("Access restricted to Admins and Managers.");
            //    this.Close();
            //    var main = new MainWindow(loggedInEmployee);
            //    main.Show();
            //}
        }

        private void SalesHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("History feature not implemented yet.");
            var salesWindow = new SalesWindow(loggedInEmployee);
            salesWindow.Show();
        }
        private void EndOfDay_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInEmployee.Role !="Admin" && loggedInEmployee.Role !="Manager") //Need to test with Staff Role user Logged in.
            {
                MessageBox.Show("Access denied. Admin / Manager Access Only");
            }

            MessageBox.Show("End of Day feature not implemented yet.");
            
        }
        private void ActiveUsers_Click(object sender, RoutedEventArgs e)
        {
            //Initial Planned code for security - Realised that All I needed was Not an Admin && not a Manager
            //MessageBox.Show("Active Users feature not implemented yet.");
            //if (loggedInEmployee.Role == "Staff" || loggedInEmployee.Role != "Admin" || loggedInEmployee.Role != "Manager") //Need to test with Staff Role user Logged in.
            //{
            //    MessageBox.Show("Access denied. Admin / Manager Access Only");
            //}

            if (loggedInEmployee.Role != "Admin" && loggedInEmployee.Role != "Manager") //Need to test with Staff Role user Logged in.
            {
                MessageBox.Show("Access denied. Admin / Manager Access Only");
            }
            MessageBox.Show("Active Users feature not implemented yet.");
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //This may have not been an issue as my role handling initially was not correct

            //var main = new MainWindow(loggedInEmployee); Both Commented out, as testing theory that Closing and opening new windows clear User Role data.
            //main.Show();
            this.Close();
        }
    }
}
