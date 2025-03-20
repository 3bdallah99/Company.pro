using AutoMapper;
using Company.pro.BLL.Interfaces;
using Company.pro.DAL.Models;
using Company.pro.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.pro.PL.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper
            ) 
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            this.mapper = mapper;
        }


        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }else
            {
                employees = _employeeRepository.GetByName(SearchInput);
            }
            // Dictionary : 3 Property
            // 1.ViewData   : Transfer Extra Information From Controller (Action) To View 
            //ViewData["Message"] = "Hello From ViewData ";

            // 2.ViewBag    : Transfer Extra Information From Controller (Action) To View
            ViewBag.Message = "Hello From ViewBag"; 
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                var employee = mapper.Map<Employee>(model);
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created !!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id ,string viewName= "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); // 400
            var employees = _employeeRepository.Get(id.Value);
            if (employees == null) return NotFound(new { statusCode = 404, message = $"Employee With Id : {id} is not Found "});
        
            return View(viewName,employees);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
                return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id != id) return BadRequest();
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(employee);
        }
    }
}
