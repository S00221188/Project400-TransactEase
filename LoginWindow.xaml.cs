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
using Project400_TransactEase.Models;

namespace Project400_TransactEase
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ClockOutBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            using (var db = new AppDBContext())
            {
                var employee = db.Employees.FirstOrDefault(u => u.Username == username);

                if (employee == null || !BCrypt.Net.BCrypt.Verify(password, employee.Password)) 
                {
                    ErrorText.Text = "Invalid username or password.";
                    return;
                } 

                var openClockIn = db.ClockInRecords
                    .FirstOrDefault(r => r.EmployeeID == employee.EmployeeID && r.ClockOutTime == null);

                if (openClockIn == null)
                {
                    MessageBox.Show("You are not currently clocked in.", "Clock out Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                openClockIn.ClockOutTime = DateTime.Now;
                db.SaveChanges();

                //Calculate & Display Shift duration (Future update print receipt)
                TimeSpan duration = openClockIn.ClockOutTime.Value - openClockIn.ClockInTime;

                int hours = (int)duration.Hours;
                int minutes = (int)duration.Minutes;

                MessageBox.Show(
                    $"Clock out successful, {employee.FirstName}.\n" +
                    $"You worked for {hours} hour(s) and {minutes} minute(s).\n" +
                    $"{DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm")}",
                    "Clocked Out", MessageBoxButton.OK, MessageBoxImage.Information
                    );

                //Clear User detail fields
                UsernameBox.Clear();
                PasswordBox.Clear();
                ErrorText.Text = string.Empty;
                    
            }
        }

        private void LoginAndClockInBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            var clickedButton = sender as Button;

            using (var db = new AppDBContext())
            {
                var employee = db.Employees.FirstOrDefault(u => u.Username == username);

                if (employee == null || !BCrypt.Net.BCrypt.Verify(password, employee.Password))
                {
                    ErrorText.Text = "Invalid username or password.";
                    return;
                }
                
                //Checking for a running Clock-in Record
                bool isAlreadyClockedIn = db.ClockInRecords
                    .Any(r => r.EmployeeID == employee.EmployeeID && r.ClockOutTime == null);

                if (!isAlreadyClockedIn) //Verifying User is not already clocked in as to save double clock-ins (Saving headaches for Payroll in future)
                {
                    //Clock in
                    var clockIn = new ClockInRecord
                    {
                        EmployeeID = employee.EmployeeID,
                        ClockInTime = DateTime.Now
                    };
                    db.ClockInRecords.Add(clockIn);
                    db.SaveChanges();
                    //Employee now clocked in (Saves someone forgetting to clock in!)
                    MessageBox.Show($"Clock-in successful for {employee.FirstName} {employee.LastName}. \nYou are now logged in. Enjoy your shift!", "Clocked-In", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else 
                {
                    var clockInTime = db.ClockInRecords
                    .Where(r => r.EmployeeID == employee.EmployeeID && r.ClockOutTime == null)
                    .Select(r => r.ClockInTime)
                    .FirstOrDefault();

                    TimeSpan timeSinceClockIn = DateTime.Now - clockInTime;
                    MessageBox.Show($"Welcome back, {employee.FirstName}.\n" + 
                        $"You clocked in {timeSinceClockIn.Hours} hour(s) & {timeSinceClockIn.Minutes} minute(s) ago.", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //Navigate to main window when successful
                MainWindow main = new MainWindow(employee); //Passing employee object
                main.Show();
                this.Close();
            }
        }
    }
}
