using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporateNewsPortal.Dtos;
using CorporateNewsPortal.Models;
namespace CorporateNewsPortal.Profiles
{
    public class NewsProfile:Profile
    {
        public NewsProfile()
        {
            CreateMap<News, EmployeeReadNewsDto>();
            CreateMap<EmployeeCreateNewsDto, News>();
        }
        }
}
