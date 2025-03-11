using Company.pro.BLL.Interfaces;
using Company.pro.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.pro.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        // Ask CLR Create Object From DepartmentRepository 
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // GET : /Department/Index 
        public IActionResult Index()
        {
            var depatment = _departmentRepository.GetAll();
            return View(depatment);
        }
    }
}
