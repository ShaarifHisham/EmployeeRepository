using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpRecord.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Graduation { get; set; }
        public string Role { get; set; }
        public int Experience { get; set; }
        public int Salary { get; set; }
    }

}
