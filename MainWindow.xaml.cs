using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
        private List<Product> allProducts = new List<Product>();
        private List<Product> currentBill = new List<Product>();
        public MainWindow(Employee employee)
        {
            InitializeComponent();
            loggedInEmployee = employee;
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductPanel.Children.Clear();

            using (var db = new AppDBContext())
            {
                allProducts = db.Products.Where(p => !p.IsDiscontinued).OrderBy(p => p.ProductType).ToList();
            }
            // Create buttons for each product
            foreach (var product in allProducts)
            {
                var button = new Button
                {
                    Content = $"{product.ProductName}\n€{product.ProductPrice}",
                    Tag = product,
                    Width = 120,
                    Height = 60,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(0, 242, 255)),
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold
                };
                button.Click += ProductButton_Click;
                ProductPanel.Children.Add(button);
            }
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
           //adds products to current bill - 1st phase, Multiple running tabs not currently developed
           if (sender is Button btn && btn.Tag is Product product)
            {
                currentBill.Add(product);
                BillList.Items.Add($"{product.ProductName} - €{product.ProductPrice}");
                UpdateTotal();
            }
        }
        private void UpdateTotal()
        {
            decimal total = currentBill.Sum(p => p.ProductPrice);
            TotalText.Text= $"€{total:F2}";
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn && btn.Tag is string productType)
            {
                ProductPanel.Children.Clear();
                var filter = productType == "All" ? allProducts : allProducts.Where(p => p.ProductType == productType).ToList(); //Co-Pilot suggested improvement

                foreach (var product in filter)
                {
                    var button = new Button
                    {
                        Content = $"{product.ProductName}\n€{product.ProductPrice}",
                        Tag = product,
                        Width = 120,
                        Height = 60,
                        Margin = new Thickness(5),
                        Background = new SolidColorBrush(Color.FromRgb(0, 242, 255)),
                        Foreground = Brushes.Black,
                        FontWeight = FontWeights.Bold
                    };
                    button.Click += ProductButton_Click;
                    ProductPanel.Children.Add(button);
                }
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
            MessageBox.Show("Logged out successfully.");
        }

        private void Bill_Click(object sender, RoutedEventArgs e)
        {
            if (!currentBill.Any())
            {
                MessageBox.Show("No items in the bill.");
                return;
            }

            // Group items by product name
            var groupedItems = currentBill.GroupBy(p => p.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    Quantity = g.Count(),
                    Price = g.First().ProductPrice,
                    VATRate = g.First().VATRate,
                    LineTotal = g.Sum(p => p.ProductPrice)
                })
                .ToList();

            StringBuilder bill = new StringBuilder();
            bill.AppendLine("Bill:");
            bill.AppendLine($"Server:{loggedInEmployee.FirstName}");
            bill.AppendLine($"Time {DateTime.Now:dd/MM/yyyy HH:mm}");
            bill.AppendLine("Item                  Qty   Price     Total");
            bill.AppendLine("----------------------------------------------");

            foreach (var item in groupedItems)
            {
                bill.AppendLine($"{item.ProductName,-20} {item.Quantity,3}   €{item.Price,6:F2}   €{item.LineTotal,7:F2}"); //ChatGPT helped formatting for clean alignment
            }

            decimal grossTotal = groupedItems.Sum(i => i.LineTotal);
            bill.AppendLine("----------------------------------------------");
            bill.AppendLine($"Gross Total:                         €{grossTotal:F2}");
            bill.AppendLine("----------------------------------------------\n");

            //Vat breakdown
            var vatBreakdown = groupedItems
                .GroupBy(i => i.VATRate) //This will group by VAT rate i.e. hot food 13.5% if/when added in future
                .Select(g => new
                {
                    VATRate = g.Key,
                    Gross = g.Sum(i => i.LineTotal),
                    Net = g.Sum(i => i.LineTotal / (1 + g.Key)),
                    VAT = g.Sum(i => i.LineTotal) - g.Sum(i => i.LineTotal / (1 + g.Key))
                })
                .ToList();

            bill.AppendLine("VAT Breakdown:");
            bill.AppendLine("Rate      Net        VAT        Gross");
            bill.AppendLine("----------------------------------------------");

            decimal totalVAT = 0;
            decimal totalNet = 0;

            foreach (var v in vatBreakdown)
            {
                totalNet += v.Net;
                totalVAT += v.VAT;
                bill.AppendLine(
                    $"{(v.VATRate * 100),2}%   €{v.Net,8:F2}   €{v.VAT,8:F2}   €{v.Gross,8:F2}");
            }

            bill.AppendLine("----------------------------------------------");
            bill.AppendLine($"Total Net:                          €{totalNet:F2}");
            bill.AppendLine($"Total VAT:                          €{totalVAT:F2}");
            bill.AppendLine($"Total (Gross):                      €{grossTotal:F2}");
            bill.AppendLine("----------------------------------------------\n");
            bill.AppendLine("Note: This is a customer bill summary.");
            bill.AppendLine("A full VAT invoice can be generated on request."); //Future when sales history is implemented, payment done -> generate invoice with all needed details

            MessageBox.Show(bill.ToString(), "Current Bill");
        }



        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentBill.Clear();
            BillList.Items.Clear();
            UpdateTotal(); //In future, Addition of Void handling will be implemented
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            // Cash Card or voucher
            if (!currentBill.Any())
            {
                MessageBox.Show("No items in the bill.");
                return;
            }

            var total = currentBill.Sum(p => p.ProductPrice);
            //Need to implement history next - Handle Stock Control and Sales History
        }

        private void Tabs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tabs feature not implemented yet.");
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow(loggedInEmployee);
            settings.Show();
            //this.Close(); //This seems to cause LoggedInEmployee data to disappear
        }

    }
}
