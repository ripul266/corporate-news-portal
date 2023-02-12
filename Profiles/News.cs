using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporateNewsPortal.Dtos;
using CorporateNewsPortal.Models;
namespace CorporateNewsPortal.Profiles
{
    public class News:Profile
    {
        public News()
        {
            CreateMap<EmployeeNews, EmployeeReadNewsDto>();
            CreateMap<EmployeeCreateNewsDto, EmployeeNews>();
        }
        }
}
