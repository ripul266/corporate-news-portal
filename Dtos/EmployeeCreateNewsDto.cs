using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Dtos
{
    public class EmployeeCreateNewsDto
    {
        public int NewsId
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Description
        {
            get;
            set;
        }
        public DateTime Date
        {
            get; set;
        }


    }
}
