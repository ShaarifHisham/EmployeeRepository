using EmpRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpRecord.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeeContext _context;
        public EmployeeRepo(EmployeeContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
        public List<Employee> GetSalaryOrder()
        {
            var employee = _context.Employees.OrderByDescending(emp => emp.Salary).ToList();
            return employee;
        }
        public List<Employee> Developer()
        {
            var employee = _context.Employees.Where(emp => emp.Role == "Developer" || emp.Role == "developer").ToList();
            return employee;
        }
        public List<Employee> Designer()
        {
            var employee = _context.Employees.Where(emp => emp.Role == "Designer" || emp.Role == "designer").ToList();
            return employee;
        }
        public List<Employee> Marketing()
        {
            var employee = _context.Employees.Where(emp => emp.Role == "Marketing" || emp.Role == "marketing").ToList();
            return employee;
        }
        public Employee getById(int id)
        {
            return _context.Employees.Where(emp => emp.Id == id).FirstOrDefault();
        }

        public void insert(Employee employee)
        {
            _context.Add(employee);
        }

        public void Delete(int id)
        {
            Employee emp = getById(id);
            _context.Remove(emp);
        }

        public void update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void save()
        {
            _context.SaveChanges();
        }

    }
}
