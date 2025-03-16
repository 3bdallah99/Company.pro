using Company.pro.BLL.Interfaces;
using Company.pro.DAL.Data.Contexts;
using Company.pro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.pro.BLL.Repositories
{
    internal class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            
        }


        //private readonly CompanyDbContext _context;
        //public EmployeeRepository(CompanyDbContext context)
        //{
        //   _context = context;
        //}

        //public int Add(Employee model)
        //{
        //     _context.Employees.Add(model);
        //    return _context.SaveChanges();  

        //}

        //public int Delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}

        //public Employee? Get(int Id)
        //{
        //    return _context.Employees.Find(Id);

        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}

        //public int Update(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}
    }
}
