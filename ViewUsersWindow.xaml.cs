using Project400_TransactEase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ViewUsersWindow.xaml
    /// </summary>
    public partial class ViewUsersWindow : Window
    {
        private readonly Employee loggedInEmployee;
        public ViewUsersWindow(Employee employee)
        {
            InitializeComponent();
            loggedInEmployee = employee;
            RoleBox.SelectedIndex = 0; 
            LoadEmployees();
        }
        
        private void LoadEmployees(string search="")
        {
            UserList.Items.Clear();
            UserDetailsList.Items.Clear();
            StatusText.Text = "";

            using (var db = new AppDBContext())
            {
                var query = db.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(e => e.Username.Contains(search) || e.FirstName.Contains(search) || e.LastName.Contains(search));
                }

                var employees = query.OrderBy(e => e.LastName).ToList();

                foreach (var e in employees)
                {
                    UserList.Items.Add($"#{e.EmployeeID} - {e.FirstName} {e.LastName} ({e.Role})");
                }
            }
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserDetailsList.Items.Clear();
            if (UserList.SelectedItem == null) return;
            var selectedText = UserList.SelectedItem.ToString();

            if (!selectedText.StartsWith("#")) return;
            var idPart = selectedText.Split('-')[0].Trim().TrimStart('#');

            if (!int.TryParse(idPart, out int employeeID )) return;

            using (var db = new AppDBContext())
            {
                var employee = db.Employees.Find(employeeID);
                if (employee == null) return;

                UserDetailsList.Items.Add($"Employee ID: {employee.EmployeeID}");
                UserDetailsList.Items.Add($"Fist Name: {employee.FirstName}");
                UserDetailsList.Items.Add($"Last Name: {employee.LastName}");
                UserDetailsList.Items.Add($"Username: {employee.Username}");
                UserDetailsList.Items.Add($"User Role: {employee.Role}");
            }
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "";

            string username = UsernameBox.Text.Trim();
            string pin = PinBox.Password.Trim();
            string firstName = FirstNameBox.Text.Trim();
            string lastName = LastNameBox.Text.Trim();
            string role = (RoleBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Employee";

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(pin) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName))
            {
                StatusText.Text = "All fields are required.";
                return;
            }

            if (!Regex.IsMatch(pin, @"^\d{4,6}$"))
            {
                StatusText.Text = "PIN must be 4–6 digits.";
                return;
            }

            using (var db = new AppDBContext())
            {
                bool usernameTaken = db.Employees.Any(em => em.Username == username);
                if (usernameTaken)
                {
                    StatusText.Text = "That username is already taken.";
                    return;
                }

                var emp = new Employee
                {
                    Username = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(pin),
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role
                };

                db.Employees.Add(emp);
                db.SaveChanges();
            }


            ClearFormOnly();
            LoadEmployees();
        }
        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "";
            ClearFormOnly();
        }

        private void ClearFormOnly()
        {
            UsernameBox.Clear();
            PinBox.Clear();
            FirstNameBox.Clear();
            LastNameBox.Clear();
            RoleBox.SelectedIndex = 2;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees(SearchBox.Text.Trim());
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees(SearchBox.Text.Trim());
        }
    }
}
