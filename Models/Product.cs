using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project400_TransactEase.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public decimal ProductStockCount { get; set; } //Changed to decimal to enable half pints (Shared inventory between Pint & Half pint)
        public string ProductType { get; set; }
        public bool IsDiscontinued { get; set; }

        //Value added tax for receipts
        public decimal VATRate { get; set; } = 0.23m; // Default VAT rate of 23%

        public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
