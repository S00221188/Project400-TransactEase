using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project400_TransactEase.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; //To be Hashed
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //Employee Roll (i.e. Staff, Manager, Admin)
        public string Role { get; set; } = "Staff"; //Defaulted option

    }
}
