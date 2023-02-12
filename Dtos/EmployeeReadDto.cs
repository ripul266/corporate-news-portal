using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Dtos
{
    public class EmployeeReadDto
    {
        public int EmployeeId
        {
            get; set;
        }
        public string EmployeeName
        {
            get; set;
        }
        public string PhoneNumber
        {
            get; set;
        }

        public string Gender
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
   /*     public string Password
        {
            get; set;
        }*/
        public bool Approval
        {
            get;
            set;
        }

    }
}
