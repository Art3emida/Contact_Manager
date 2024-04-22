using Contact_Manager.Data;
using Contact_Manager.Models;
using Contact_Manager.Dto;
using Contact_Manager.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contact_Manager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Employee> objEmployeeList = _db.Employees.ToList();
            return View(objEmployeeList);
        }

        [HttpPost]
        public IActionResult UploadCsv(IFormFile file)
        {
            var csvImportService = new CsvImportService();
            List<EmployeeCsvDto> items;

            try
            {
                items = csvImportService.ExtractData<EmployeeCsvDto>(file);
            }
            catch (FileNotFoundException)
            {
                TempData["error"] = "File not found!";
                return RedirectToAction("Index");
            }
            catch (InvalidDataException)
            {
                TempData["error"] = "Invalid file format. Supported: .csv";
                return RedirectToAction("Index");
            }

            foreach (var item in items)
            {
                var employee = new Employee()
                {
                    Name = item.Name,
                    DateOfBirth = item.DateOfBirth,
                    Married = item.Married,
                    Phone = item.Phone,
                    Salary = item.Salary,
                };
                _db.Employees.Add(employee);
            }

            _db.SaveChanges();

            TempData["success"] = "Successfully imported. Added: " + items.Count.ToString();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateRecord(int id, string fieldName, string fieldValue)
        {
            var entity = _db.Employees.Find(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Employee not found" });
            }

            var propertyInfo = entity.GetType().GetProperty(fieldName);
            if (propertyInfo == null)
            {
                return Json(new { success = false, message = "Property not found" });
            }

            try
            {
                object value;

                if (propertyInfo.PropertyType == typeof(DateOnly))
                {
                    value = DateOnly.Parse(fieldValue);
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    value = bool.Parse(fieldValue);
                }
                else if (propertyInfo.PropertyType.IsEnum)
                {
                    value = Enum.Parse(propertyInfo.PropertyType, fieldValue);
                }
                else
                {
                    value = Convert.ChangeType(fieldValue, propertyInfo.PropertyType);
                }

                propertyInfo.SetValue(entity, value);

                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = $"Error converting value: {e.Message}" });
            }

            return Json(new { success = true, message = "Successfully updated!" });
        }

        [HttpPost]
        public IActionResult DeleteRecord(int id)
        {
            var entity = _db.Employees.Find(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Employee not found" });
            }

            _db.Employees.Remove(entity);
            _db.SaveChanges();

            return Json(new { success = true, message = "Successfully deleted!" });
        }
    }
}
