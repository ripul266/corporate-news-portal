using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CorporateNewsPortal.Data;
using CorporateNewsPortal.Dtos;
using CorporateNewsPortal.Models;

namespace CorporateNewsPortal.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();

        }
        
    }
}
