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
    public class DapartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context; //null
        public DapartmentRepository()
        {
            _context = new CompanyDbContext();
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Department.ToList();
        }
        public Department? Get(int Id)
        {
            return _context.Department.Find(Id);
        }
        public int Add(Department model)
        {
            _context.Department.Add(model);  
            return _context.SaveChanges();

        }
        public int Update(Department model)
        {
            _context.Department.Update(model);
            return _context.SaveChanges();
        }
        public int Delete(Department model)
        {
            _context.Department.Remove(model);
            return _context.SaveChanges();
        }


    }
}
