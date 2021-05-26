using EmpRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpRecord.Services
{
    public interface IEmployeeRepo
    {
        List<Employee> GetAll();
        void save();
        Employee getById(int id);
        void update(Employee employee);
        void insert(Employee employee);
        void Delete(int id);
        List<Employee> GetSalaryOrder();
        List<Employee> Developer();
        List<Employee> Designer();
        List<Employee> Marketing();

    }
}
