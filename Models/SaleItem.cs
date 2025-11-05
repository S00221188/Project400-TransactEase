using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project400_TransactEase.Models
{
    public class SaleItem
    {
        public int SaleItemID { get; set; }
        public int SaleID { get; set; } // Foreign Key, Links to sale
        public int ProductID { get; set; } //Foreign Key, Links purchased product('s)
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
    }
}
