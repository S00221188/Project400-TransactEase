using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project400_TransactEase.Models
{
    public class ClockInRecord
    {
        public int ClockInID { get; set; }
        public int EmployeeID { get; set; } //Foreign key, links employee to Clock in record
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }  //Null until employee clocks out

        public virtual Employee Employee { get; set; }
    }
}
