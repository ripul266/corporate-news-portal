using CorporateNewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNews();
        Task<News> FindEmployeeNewsById(int NewsId);
        Task<bool> CreateNews(News eNews);
        Task<bool> CreateNewsAdmin(News eNews);
        Task<bool> DeleteNews(News eNews);
        Task<IEnumerable<News>> GetNotApprovedNews();
        Task<IEnumerable<News>> GetApprovedNews();
        Task<bool> ApproveNews(News edt);

      
        

    }
}
