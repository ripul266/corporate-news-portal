using CorporateNewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
    public interface INewsRepository
    {
        Task<IEnumerable<EmployeeNews>> GetAllNews();
        Task<EmployeeNews> FindEmployeeNewsById(int NewsId);
        Task<bool> CreateNews(EmployeeNews eNews);
        Task<bool> DeleteNews(EmployeeNews eNews);

    }
}
