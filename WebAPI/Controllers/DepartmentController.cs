using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        // PUT: api/Department
        [HttpPut]
        public JsonResult PutDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            _context.SaveChanges();
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Department
        [HttpPost]
        public JsonResult PostDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public JsonResult DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return new JsonResult("Deparment Not Found");
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }        
    }
}
