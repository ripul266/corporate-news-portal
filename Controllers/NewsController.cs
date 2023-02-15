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

        public async Task<ActionResult<IEnumerable<News>>> GetAll()
        {
            var employeeList = await repo.GetAllNews();
            if (employeeList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeReadNewsDto>>(employeeList));
        }
        [HttpGet]
        [Route("ApprovedNews")]
        public async Task<ActionResult<IEnumerable<News>>> GetApprovedNews()
        {
            var employeeList = await repo.GetApprovedNews();
            if (employeeList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeReadNewsDto>>(employeeList));
        }
        [HttpGet]
        [Route("NotApprovedNews")]
        public async Task<ActionResult<IEnumerable<News>>> GetNotApprovedNews()
        {
            var employeeList = await repo.GetNotApprovedNews();
            if (employeeList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EmployeeReadNewsDto>>(employeeList));
        }
        [HttpGet("id", Name = "GetNewsById")]

        public async Task<ActionResult<EmployeeReadDto>> GetNewsById(int id)
        {

            News p = await repo.FindEmployeeNewsById(id);
            if (p != null)
                return Ok(_mapper.Map<EmployeeReadNewsDto>(p));
            else
                return NotFound("Id Not Found");
        }
        [HttpPost]
        [Route("EmployeeNews")]
        public async Task<ActionResult<bool>> CreateEmployeeNews([FromBody] EmployeeCreateNewsDto emp)
        {
            News modelEmpNews = _mapper.Map<News>(emp);
            var result = await repo.CreateNews(modelEmpNews);
            if (!result)
            {
                return BadRequest("The Object Not Created");
            }
            var edtReadDto = _mapper.Map<EmployeeReadNewsDto>(modelEmpNews);
            return CreatedAtRoute(new { productId = edtReadDto.NewsId }, edtReadDto);
        }
        [HttpPost]
        [Route("AdminNews")]
        public async Task<ActionResult<bool>> CreateAdminNews([FromBody] EmployeeCreateNewsDto emp)
        {
            News modelEmpNews = _mapper.Map<News>(emp);
            var result = await repo.CreateNewsAdmin(modelEmpNews);
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
        [HttpPut()]
        [Route("ApproveNews")]
        public async Task<ActionResult<bool>> ApproveEmployeeNews(int id)
        {
            var pdtFound = await repo.FindEmployeeNewsById(id);
            if (pdtFound == null)
            {
                return NotFound($"id {id} not found");
            }


            var result = await repo.ApproveNews(pdtFound);
            if (result)
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

