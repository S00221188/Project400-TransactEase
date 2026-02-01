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
        public string Username { get; set; } = string.Empty; //No longer used for logging employee in - Password is now a Pincode for faster log-ins
        public string Password { get; set; } = string.Empty; //Now a Pincode for faster log-ins - Biometrics to be added in future updates
                                                             //Validation for numbers only and length to be implemented in the AddEmployee / EditEmployee features later.
        public string FirstName { get; set; } = string.Empty; //Purely for identification purposes
        public string LastName { get; set; } = string.Empty; //Purely for identification purposes
        //Employee Roll (i.e. Staff, Manager, Admin)
        public string Role { get; set; } = "Employee"; //Defaulted option - Changed to Employee

    }
}
