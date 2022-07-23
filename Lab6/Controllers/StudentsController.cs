using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Data;
using Lab6.Models;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        
        [ProducesResponseType(StatusCodes.Status200OK)] // returned when we return list of Students successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)]// returend when the server can not understand the request and can not send 404 page in any reason 
        [ProducesResponseType(StatusCodes.Status404NotFound)]// returend when requested resources do not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an error in processing the request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5

        [ProducesResponseType(StatusCodes.Status200OK)] // returned when we return requested Student info successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)]// returend when the server can not understand the request and can not send 404 page in any reason 
        [ProducesResponseType(StatusCodes.Status404NotFound)]// returend when requested resource dose not exist
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an error in processing the request
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status201Created)] // returned when returned when The request succeeded, and a student was updated as a result.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]// returend when the server can not understand the request and can not send 404 page in any reason 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an error in processing the request
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> PutStudent(string id, Student student)
        {
            if (id != student.ID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            /*            return NoContent();*/
            
            return student;
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status201Created)] // returned when The request succeeded, and a new student was created as a result.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]// returend when The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing)reason 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when The server has encountered a situation it does not know how to handle
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(student.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudent", new { id = student.ID }, student);
        }

        // DELETE: api/Students/5
        [ProducesResponseType(StatusCodes.Status200OK)] // returned when defined Students id is deleted successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)]// returend when The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing)reason 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when The server has encountered a situation it does not know how to handle
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
