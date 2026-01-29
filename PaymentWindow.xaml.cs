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
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    

    public partial class PaymentWindow : Window
    {
        private decimal totalAmount;
        private decimal paidAmount = 0;
        private Employee employee;
        private List<Product> bill;
        public bool SaleCompleted { get; private set; }
        public PaymentWindow(decimal total, Employee loggedIn, List<Product> currentBill)
        {
            InitializeComponent();
            totalAmount = total;
            TotalDueText.Text = $"€{total:F2}";
            employee = loggedIn;
            bill = currentBill;
        }

        private void QuickPay_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && decimal.TryParse(btn.Content.ToString().Replace("€","").Trim(), out var amount))
            paidAmount = amount;
            ConfirmPayment();
        }
        private void CardPay_Click(object sender, RoutedEventArgs e)
        {
            // Simulate card payment processing
            paidAmount = totalAmount;
            ConfirmPayment(card: true);
        }
        private void NumberPadButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                CustomAmountBox.Text += btn.Content.ToString();
            }
        }
        private void ClearAmountButton_Click(object sender, RoutedEventArgs e)
        {
            CustomAmountBox.Text = string.Empty;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(CustomAmountBox.Text, out var enteredAmount))
            {
                paidAmount = enteredAmount;
                ConfirmPayment();
            }
            else
            {
                MessageBox.Show("Enter a valid amount.");
            }
        }
        private void BackSpaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomAmountBox.Text.Length > 0)
            {
                CustomAmountBox.Text = CustomAmountBox.Text.Substring(0, CustomAmountBox.Text.Length - 1);
            }
        }
        private void ConfirmPayment(bool card = false)
        {
            if (paidAmount < totalAmount && !card)
            {
                MessageBox.Show("Amount is less than total due.");
                return;
            }

            decimal change = paidAmount - totalAmount;

            if (!card)
            {
                MessageBox.Show($"Change to give: €{change:F2}");
            }

            using (var db = new AppDBContext())
            {
                var sale = new Sale
                {
                    EmployeeID = employee.EmployeeID,
                    SaleTime = DateTime.Now,
                    TotalAmount = totalAmount,
                    SaleItems = bill
                        .GroupBy(p => p.ProductID)
                        .Select(g => new SaleItem
                        {
                            ProductID = g.Key,
                            Quantity = g.Count(),
                            UnitPrice = g.First().ProductPrice
                        })
                        .ToList()
                };

                db.Sales.Add(sale);
                db.SaveChanges();
            }

            SaleCompleted = true;
            this.Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
