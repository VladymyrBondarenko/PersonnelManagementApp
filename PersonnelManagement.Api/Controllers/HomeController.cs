using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Api.Models;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Application.Positions;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using System.Diagnostics;
using System.IO;

namespace PersonnelManagement.Api.Controllers
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
        private readonly IOriginalService originalService;

        public HomeController(ILogger<HomeController> logger,
            IEmployeeService employeeService, IOrderService orderService,
            IOrderDescriptionService orderDescription, IPositionService positionService,
            IDepartmentService departmentService, IFtpService ftpService,
            IOriginalService originalService)
        {
            _logger = logger;
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.orderDescription = orderDescription;
            this.positionService = positionService;
            this.departmentService = departmentService;
            this.ftpService = ftpService;
            this.originalService = originalService;
        }

        public async Task<IActionResult> Index()
        {
            //await testOrders();

            //await testFileManager();

            //await testOriginalService();

            return View();
        }

        private async Task testOrders()
        {
            //var dep = await departmentService.CreateAsync(new Department { DepartmentTitle = "Department 1" });
            //var pos = await positionService.CreateAsync(new Position { PositionTitle = "Department 1" });
            //var desc = await orderDescription.CreateAsync(new OrderDescription { OrderDescriptionTitle = "Hire Order", OrderType = OrderType.HireOrder });
            //var order = await orderService.CreateAsync(new Order
            //{
            //    FirstName = "Vladymyr",
            //    LastName = "Bondarenko",
            //    OrderDescriptionId = desc.Id,
            //    DepartmentId = dep.Id,
            //    PositionId = pos.Id
            //});
            //var success = await order.AcceptOrderAsync();

            //var employee = (await orderService.GetOrderAsync(order.Order.Id))?.Order.Employee;

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

        private async Task testFileManager()
        {
            //var files = ftpService.GetFileNames();
            //var path = @"C:\Users\38095\Desktop\Перевод.txt";
            //var bytes = System.IO.File.ReadAllBytes(path);

            ////ftpService.SaveFileToFtp(bytes, @"‪C:\Users\38095\Desktop\ftp\Orders");
            //var filepath = @"Orders\Переводы.txt";
            //var success = await ftpService.SaveFileToFtpAsync(bytes, filepath);

            //if (success)
            //{
            //    bytes = await ftpService.ReadAllBytesAsync(filepath);
            //    Console.WriteLine(bytes);
            //}
        }

        private async Task testOriginalService()
        {
            //var path1 = @"C:\Users\38095\Desktop\Перевод.txt";
            //var path2 = @"C:\Users\38095\Desktop\Новый текстовый документ.txt";

            //var dep = await departmentService.CreateAsync(new Department { DepartmentTitle = "Department 1" });
            //var pos = await positionService.CreateAsync(new Position { PositionTitle = "Department 1" });

            //var employee = await employeeService.CreateAsync(
            //    new Employee
            //    {
            //        FirstName = "Vladymyr",
            //        LastName = "Bondarenko",
            //        DepartmentId = dep.Id,
            //        PositionId = pos.Id,
            //        EmployeeState = EmployeeState.Hired,
            //        HireDate = DateTime.Now
            //    });

            //var orig = await employeeService.AddOriginalAsync(new OriginalCreateParams
            //{
            //    EmployeeId = employee.Id,
            //    SourceFilePath = path2
            //});

            //await employeeService.DeleteOriginalAsync(new OriginalDeleteParams
            //{
            //    EmployeeId = employee.Id,
            //    OriginalId = orig.Id
            //});

            //var desc = await orderDescription.CreateAsync(new OrderDescription { OrderDescriptionTitle = "Hire Order", OrderType = OrderType.HireOrder });
            //var order = await orderService.CreateAsync(new Order
            //{
            //    FirstName = "Vladymyr",
            //    LastName = "Bondarenko",
            //    OrderDescriptionId = desc.Id,
            //    DepartmentId = dep.Id,
            //    PositionId = pos.Id
            //});

            //var orig1 = await orderService.AddOriginalAsync(new OriginalCreateParams { OrderId = order.Order.Id, SourceFilePath = path1 });
            //var orig2 = await orderService.AddOriginalAsync(new OriginalCreateParams { OrderId = order.Order.Id, SourceFilePath = path2 });

            //var originals = await originalService.GetOriginalsAsync();
            //foreach (var o in originals)
            //{
            //    await originalService.DeleteOriginalAsync(o);
            //}

            //var orderModel = await orderService.GetOrderAsync(new Guid("0976cc07-9161-4cbc-4dff-08daa250e94c"));
            //var origs = orderModel.Order.Originals.ToList();

            //var deleted = await orderService.DeleteOriginalAsync(
            //    new OriginalDeleteParams
            //    {
            //        OrderId = orderModel.Order.Id,
            //        OriginalId = origs.First().Id
            //    });
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