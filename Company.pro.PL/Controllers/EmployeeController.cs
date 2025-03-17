using Company.pro.BLL.Interfaces;
using Company.pro.DAL.Models;
using Company.pro.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.pro.PL.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }


        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,    
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,

                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
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
