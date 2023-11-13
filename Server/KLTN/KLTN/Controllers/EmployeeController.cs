using BL;
using BO;
using DL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KLTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        IBLEmployee _bLEmployee;

        public EmployeeController(IBLEmployee bLEmployee)
        {
            _bLEmployee = bLEmployee;
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetEmployee()
        {
            List<Employee> employee = _bLEmployee.GetEmployee();
            return Ok(employee);
        }
    }
}
