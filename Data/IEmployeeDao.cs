using CorporateNewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
    interface IEmployeeDao
    {
        int InsertProduct(Employee e);
     /*   int UpdateProductPrice(int id, decimal newPrice);*//**//*
        List<Employee> GetAllProducts();
        int DeleteProduct(int id);
        Employee GetProductById(int id);*/
    }
}
