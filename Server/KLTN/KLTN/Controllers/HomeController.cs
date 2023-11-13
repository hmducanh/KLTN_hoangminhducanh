using BO;
using Microsoft.AspNetCore.Mvc;

namespace KLTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            Employee employee = new Employee();
            employee.FullName = "hdanh1";
            return Ok(employee);
        }
    }
}
