using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using PersonnelManagement.Infrastracture.FileOperations;
using PersonnelManagement.UI.Models;
using System.Diagnostics;
using System.IO;

namespace PersonnelManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService employeeService;
        private readonly IOrderService orderService;
        private readonly IOrderDescriptionService orderDescription;
        private readonly IPositionService positionService;
        private readonly IDepartmentService departmentService;
        private readonly IFtpService ftpService;

        public HomeController(ILogger<HomeController> logger,
            IEmployeeService employeeService, IOrderService orderService, 
            IOrderDescriptionService orderDescription, IPositionService positionService, 
            IDepartmentService departmentService, IFtpService ftpService)
        {
            _logger = logger;
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.orderDescription = orderDescription;
            this.positionService = positionService;
            this.departmentService = departmentService;
            this.ftpService = ftpService;
        }

        public async Task<IActionResult> Index()
        {
            //testOrders();

            testFileManager();

            return View();
        }

        private async Task testOrders()
        {
            var dep = await departmentService.CreateAsync(new Department { DepartmentTitle = "Department 1" });
            var pos = await positionService.CreateAsync(new Position { PositionTitle = "Department 1" });
            var desc = await orderDescription.CreateAsync(new OrderDescription { OrderDescriptionTitle = "Hire Order", OrderType = OrderType.HireOrder });
            var order = await orderService.CreateAsync(new Order
            {
                FirstName = "Vladymyr",
                LastName = "Bondarenko",
                OrderDescriptionId = desc.Id,
                DepartmentId = dep.Id,
                PositionId = pos.Id,
                EmployeeId = null
            });
            var success = await order.AcceptOrderAsync();

            var employee = (await orderService.GetOrderAsync(order.Order.Id))?.Order.Employee;

            //var employee = await employeeService.GetAsync(new Guid("6d4ed215-6af8-498b-7795-08da9e4f35e0"));
            //var desc = await orderDescription.CreateAsync(new OrderDescription 
            //{ 
            //    OrderDescriptionTitle = "Fire Order", 
            //    OrderType = OrderType.FireOrder 
            //});

            //var order = await orderService.CreateAsync(new Order
            //{
            //    EmployeeId = employee.Id,
            //    OrderDescriptionId = desc.Id,
            //    DateFrom = DateTime.Now
            //});
            //order?.AcceptOrderAsync();
        }

        private void testFileManager()
        {
            //var files = ftpService.GetFileNames();
            var path = @"C:\Users\38095\Desktop\Перевод.txt";
            var bytes = System.IO.File.ReadAllBytes(path);

            //ftpService.SaveFileToFtp(bytes, @"‪C:\Users\38095\Desktop\ftp\Orders");
            ftpService.SaveFileToFtp(bytes, @"Orders\Переводы.txt");
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