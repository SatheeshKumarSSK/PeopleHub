using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // PUT: api/Employee
        [HttpPut]
        public JsonResult PutEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
            return new JsonResult("Updated Successfully");
        }

        // POST: api/Employee
        [HttpPost]
        public JsonResult PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChangesAsync();
            return new JsonResult("Added Successfully");
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public JsonResult DeleteEmployee(int id)
        {
            var Employee = _context.Employees.Find(id);
            if (Employee == null)
            {
                return new JsonResult("Deparment Not Found");
            }

            _context.Employees.Remove(Employee);
            _context.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            List<string> allDepartments = new List<string>();
            var allNames = _context.Departments.ToList();
            foreach(var name in allNames)
            {
                allDepartments.Add(name.DepartmentName);
            }
            return new JsonResult(allDepartments);
        }
    }
}
