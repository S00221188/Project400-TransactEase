using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project400_TransactEase.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public int EmployeeID { get; set; } //Foreign key, linking employee to sale
        public DateTime SaleTime { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
