using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCrudApp.Models;
using System.Diagnostics;

namespace MyCrudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContextOptions<Employee_management_SystemContext> _dbconnection;
        private readonly string conn = "server=localhost;database=Employee_management_System;user id=sa;password=_bmservice_;MultipleActiveResultSets=True;Integrated Security=False;trusted_connection=true;TrustServerCertificate=True";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbconnection = new DbContextOptionsBuilder<Employee_management_SystemContext>().UseSqlServer(conn).Options;
        }

        public IActionResult Index()
        {
            var db = new Employee_management_SystemContext(_dbconnection);
            return View();
        }


        public IActionResult AddMethod(EmployeeTb employee)
        {
            var db = new Employee_management_SystemContext(_dbconnection);

            if (employee.Emp_id == 0)
            {
                db.EmployeeTb.Add(employee);
                db.SaveChanges();
                return Json("Add");
            }
            else
            {
                EmployeeTb employeeTb = new EmployeeTb();
                employeeTb  = db.EmployeeTb.Where(x=> x.Emp_id==employee.Emp_id).FirstOrDefault();
                employeeTb.Emp_name = employee.Emp_name;    
                employeeTb.Emp_pno = employee.Emp_pno;
                employeeTb.Emp_salary = employee.Emp_salary;    
                employeeTb.Emp_gender = employee.Emp_gender;    
                employeeTb.Emp_age = employee.Emp_age;  
                employeeTb.Emp_department = employee.Emp_department;    
                employeeTb.Emp_designation = employee.Emp_designation;
                db.SaveChanges();
            }
            return Json("Edit");

        }

        public IActionResult GetMethod()
        {
            
                var db = new Employee_management_SystemContext(_dbconnection);
                var getData = db.EmployeeTb.ToList();
                return Json(getData);
           
        }



        public IActionResult DeleteMethod(int id)
        {
            var db = new Employee_management_SystemContext(_dbconnection);
            EmployeeTb employeeTb = new EmployeeTb();
            employeeTb = db.EmployeeTb.Where(x => x.Emp_id == id).FirstOrDefault();
            db.Remove(employeeTb);
            db.SaveChanges();

            return Json("Success");  
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
