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
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        private Employee loggedInEmployee;
        public SalesWindow(Employee employee)
        {
            InitializeComponent();
            loggedInEmployee = employee;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Filter Not yet fully implemented.");
        }
    }
}
