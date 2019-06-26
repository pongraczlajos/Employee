using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.ServiceLayer.Model
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Status { get; set; }
    }
}
