using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            TotalText.Text= $"Total: €{total:F2}";
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
