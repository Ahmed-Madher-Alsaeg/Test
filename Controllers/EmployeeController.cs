using AlphaBeratung.Data;
using g13lec6.Models;
using g13lec6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace g13lec6.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly ApplicationDbContext context;
        private readonly IRepository<Employee> repository;

        public EmployeeController( ApplicationDbContext context, IRepository<Employee> repository)
        {
           this.repository = repository;
            this.context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(emp);
                }
                else
                {
                    var res = repository.Add(emp);
                    
                    TempData["msg"] = "تمت الاضافة بنجاح";
                    return RedirectToAction("Create");
                    //return RedirectToAction("Index", "Dept");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=====Exp In Edit Dept Action");
                Console.WriteLine($"{ex.Message}");
                TempData["msg"] = "خطأ غير متوقع";
                return View(emp);
            }
        }

        public IActionResult Edit(int Id)
        {
            var emp =  repository.GetById(Id);
            return View(emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(employee);

                }
                else
                {
                    var emp = repository.GetById(employee.Id);
                    emp.Name = employee.Name;
                    emp.DeptId = employee.DeptId;
                    repository.Update(emp);
                    
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View(employee);
            }
        }
        public IActionResult Delete(int Id)
        {
            var emp = repository.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
