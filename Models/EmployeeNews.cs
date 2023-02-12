using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Models
{
    public class EmployeeNews
    {
        public int NewsId
        {
            get;set;
        }
        public string Title
        {
            get;set;
        }
        public string Description
        {
            get;
            set;
        }
        public bool Approval
        {
            get; set;
        } = false;

    }
}
