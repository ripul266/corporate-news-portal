using AutoMapper;
using CorporateNewsPortal.Data;
using CorporateNewsPortal.Models;
using Microsoft.AspNetCore.Http;
using CorporateNewsPortal.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        IMapper _mapper;
        INewsRepository repo;

        public NewsController(INewsRepository repository, IMapper mapper)
        {
            this.repo = repository;
            this._mapper = mapper;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<EmployeeNews>>> GetAll()
        {
            var employeeList = await repo.GetAllNews();
            if (employeeList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeReadNewsDto>>(employeeList));
        }
        [HttpGet("id", Name = "GetNewsById")]

        public async Task<ActionResult<EmployeeReadDto>> GetNewsById(int id)
        {

            EmployeeNews p = await repo.FindEmployeeNewsById(id);
            if (p != null)
                return Ok(_mapper.Map<EmployeeReadNewsDto>(p));
            else
                return NotFound("Id Not Found");
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateEmployee([FromBody] EmployeeCreateNewsDto emp)
        {
            EmployeeNews modelEmpNews = _mapper.Map<EmployeeNews>(emp);
            var result = await repo.CreateNews(modelEmpNews);
            if (!result)
            {
                return BadRequest("The Object Not Created");
            }
            var edtReadDto = _mapper.Map<EmployeeReadNewsDto>(modelEmpNews);
            return CreatedAtRoute(new { productId = edtReadDto.NewsId }, edtReadDto);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var pdtFound = await repo.FindEmployeeNewsById(id);
            if (pdtFound == null)
            {
                return NotFound($"id {id} not found");
            }
            var isDeleted = await repo.DeleteNews(pdtFound);
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

