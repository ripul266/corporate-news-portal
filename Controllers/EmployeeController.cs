using AutoMapper;
using CorporateNewsPortal.Data;
using CorporateNewsPortal.Dtos;
using CorporateNewsPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        IMapper _mapper;
        IEmployeeRepository repo;
        
        public EmployeeController(IEmployeeRepository repository, IMapper mapper)
        {
            this.repo = repository;
            this._mapper = mapper;
        }
        
        [HttpGet]
      
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var employeeList = await repo.GetAllEmployees();
            
            if (employeeList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employeeList));
        }
        [HttpGet("id", Name = "GetById")]
       
        public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
        {

            Employee p = await repo.FindEmployeeById(id);
            if (p != null)
                return Ok(_mapper.Map<EmployeeReadDto>(p));
            else
                return NotFound("Id Not Found");
        }
        [HttpGet]
        [Route("Search/{name}")]
        /*        [HttpGet("{name:alpha}")]*/
        public async Task<ActionResult<EmployeeReadDto>> GetByname(string name)
        {

            Employee p = await repo.FindEmployeeByName(name);
            if (p != null)
                return Ok(_mapper.Map<EmployeeReadDto>(p));
            else
                return NotFound("Id Not Found");
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateEmployee([FromBody] EmployeeCreateDto emp)
        {
            

            Employee modelEmp = _mapper.Map<Employee>(emp);
            var result = await repo.CreateEmployee(modelEmp);
            if (!result)
            {
                return BadRequest("The Object Not Created");
            }
           var edtReadDto= _mapper.Map<EmployeeReadDto>(modelEmp);
            return CreatedAtRoute(new { productId = edtReadDto.EmployeeId }, edtReadDto);
        }
        [HttpPut("id")]
        public async Task<ActionResult<EmployeeUpdateDto>> Put(int id, EmployeeUpdateDto UpdateDto)
        {
            var pdtFound = await repo.FindEmployeeById(id);
            if (pdtFound == null)
            {
                return NotFound($"id {id} not found");
            }
           
            _mapper.Map(UpdateDto, pdtFound);
            var result = await repo.UpdateEmployee(pdtFound);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
           

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var pdtFound = await repo.FindEmployeeById(id);
            if (pdtFound == null)
            {
                return NotFound($"id {id} not found");
            }
            var isDeleted = await repo.DeleteEmployee(pdtFound);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
