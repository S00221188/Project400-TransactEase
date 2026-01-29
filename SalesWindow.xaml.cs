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
            StartDate_Filter.SelectedDate = DateTime.Now.AddDays(-7); // Default to last 7 days
            EndDate_Filter.SelectedDate = DateTime.Now; // Default to today
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDate_Filter.SelectedDate ?? DateTime.MinValue;
            var endDate = EndDate_Filter.SelectedDate?.Date.AddDays(1) ?? DateTime.MaxValue;

            using (var db = new AppDBContext())
            {
                var sales = db.Sales
                    .Where(s => s.SaleTime >= startDate && s.SaleTime <= endDate)
                    .OrderByDescending(s => s.SaleTime)
                    .ToList();

                SalesList.Items.Clear();

                foreach (var sale in sales)
                {
                    var employee = db.Employees.Find(sale.EmployeeID);
                    SalesList.Items.Add($"#{sale.SaleID} | {sale.SaleTime} | Employee: {employee.FirstName} {employee.LastName} | Total: €{sale.TotalAmount:F2}");
                }    
            }
        }

        private void SalesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaleDetailsList.Items.Clear();

            if (SalesList.SelectedItem == null)
                return;

            var selectedText = SalesList.SelectedItem.ToString();
            if (!selectedText.StartsWith("#")) return;

            var saleIdPart = selectedText.Split('|')[0].Trim().TrimStart('#');
            if (!int.TryParse(saleIdPart, out int saleId)) return;

            using (var db = new AppDBContext())
            {
                var saleItems = db.SaleItems
                                  .Where(si => si.SaleID == saleId)
                                  .ToList();

                foreach (var item in saleItems)
                {
                    var product = db.Products.Find(item.ProductID);
                    SaleDetailsList.Items.Add($"{product.ProductName} x{item.Quantity} - €{item.UnitPrice * item.Quantity:F2}");
                }
            }
        }
    }
}
