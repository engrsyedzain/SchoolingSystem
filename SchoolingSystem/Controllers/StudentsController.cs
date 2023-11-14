using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolingSystem.Dtos;
using SchoolingSystem.Models;
using SchoolingSystem.Repositories;

namespace SchoolingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepositories _studentRepositories;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepositories studentRepositories, IMapper mapper)
        {
            this._studentRepositories = studentRepositories;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not logged in or Unauthorized");

            var students = await _studentRepositories.GetAll();
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not logged in or Unauthorized");

            var student = await _studentRepositories.Get(id);
            if (student == null)
                return NotFound("Student not found");
            return Ok(_mapper.Map<StudentDto>(student));

        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDto studentDto)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not logged in or Unauthorized");

            var student = _mapper.Map<Student>(studentDto);
            var newStudent = await _studentRepositories.Add(student);
            return CreatedAtAction("GetById", new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, StudentDto studentDto)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not logged in or Unauthorized");

            var std = await _studentRepositories.Get(id);
            studentDto.Id = id;
            if (std == null)
                return NotFound("Student not found");

            var student = _mapper.Map<Student>(studentDto);
            await _studentRepositories.Update(student);
            return Ok("Student Updated succussfully");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not logged in or Unauthorized");

            var student = await _studentRepositories.Get(id);
            if (student == null)
                return NotFound("Student not found");

            await _studentRepositories.Delete(id);
            return Ok("Student Deleted");

        }
    }
}
