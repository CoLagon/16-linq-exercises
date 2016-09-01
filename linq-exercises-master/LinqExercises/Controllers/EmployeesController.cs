using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class EmployeesController : ApiController
    {
        private NORTHWNDEntities _db;

        public EmployeesController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/employees
        [HttpGet, Route("api/employees"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            //var getEmployees = from emp in _db.Employees
            //                   select _db.Employees;
            //return Ok(getEmployees);

            var getEmployee = _db.Employees.Select(e => e);
            return Ok(getEmployee);
        }

        // GET: api/employees/title/Sales Manager
        [HttpGet, Route("api/employees/title/{title}"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployeesByTitle(string title)
        {
            var getEmpTitle = _db.Employees.Where(e => e.Title == title);
            return Ok(getEmpTitle);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
