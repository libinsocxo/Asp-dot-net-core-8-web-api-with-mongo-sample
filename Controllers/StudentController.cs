using firstapiproject.Data;
using firstapiproject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace firstapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IConfiguration _configuration;

        public StudentController(DataContext context, IConfiguration configuration)
        
        { 
            _context = context; 

            _configuration = configuration;

        }

        //[HttpGet]
        //public async Task<IActionResult> Getallstudents()
        //{
        //    var students = await _context.Students.ToListAsync();

        //    return Ok(students);
        
        //}

        [HttpGet]
        public IActionResult Get()
        {
            string? connectionString = _configuration.GetConnectionString("MongoDBConnection");

            if(connectionString == null)
            {
                return NotFound();
            }

            // Now you can use the connection string as needed
            return Ok($"MongoDB connection string: {connectionString}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getstudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student); 
        }

        [HttpPost]
        public async Task<IActionResult> Addstudent(Student student)
        {
            var newstudent = _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var updatedstudent = await _context.Students.FindAsync(student.Id);

            if(updatedstudent == null)
            {
                return NotFound("Student not found to update");
            }

            updatedstudent.Name = student.Name;
            updatedstudent.FirstName = student.FirstName;
            updatedstudent.LastName = student.LastName; 
            updatedstudent.Place = student.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestudent(int id)
        {
            var delete_student = await _context.Students.FindAsync(id);

            if(delete_student == null)
            {
                return BadRequest();
            }

             _context.Students.Remove(delete_student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
            
        }



    }
}
