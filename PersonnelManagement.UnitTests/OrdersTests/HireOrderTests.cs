using Moq;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Infrastracture.Orders.OrderBase;
using PersonnelManagement.Infrastracture.Orders.OrderBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersonnelManagement.UnitTests.OrdersTests
{
    public class HireOrderTests
    {
        private readonly IOrderService orderService;
        private readonly Mock<IOrderRepository> orderRepMock = new();
        private readonly Mock<IOrderFactory> orderFactoryMock = new();
        private readonly Mock<IEmployeeService> employeeServiceMock = new();
        private readonly Mock<IOriginalService> originalServiceMock = new();

        public HireOrderTests()
        {
            orderService = new OrderService(orderRepMock.Object, orderFactoryMock.Object);
        }

        [Fact]
        public async Task AcceptOrderAsync_ShouldCreateNewEmployeeAndBindedData()
        {
            // Arrange
            var order = new Order
            {
                FirstName = "Vladymyr",
                LastName = "Bondarenko",
                OrderDescriptionId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
                EmployeeId = null
            };

            orderRepMock
                .Setup(x => x.CreateAsync(order))
                .ReturnsAsync(() => { order.Id = Guid.NewGuid(); return order; });

            orderRepMock
                .Setup(x => x.UpdateAsync(order))
                .ReturnsAsync(true);

            var hireOrder = new HireOrder(order, orderRepMock.Object, employeeServiceMock.Object, originalServiceMock.Object);

            var employee = new Employee
            {
                Id = Guid.NewGuid()
            };

            employeeServiceMock
                .Setup(x => x.CreateAsync(hireOrder))
                .ReturnsAsync(employee);

            originalServiceMock
                .Setup(x => x.GetOriginalsAsync(It.IsAny<PaginationQuery>(), It.IsAny<GetAllOriginalsFilter>()))
                .ReturnsAsync(new List<Original>());
            
            orderFactoryMock
                .Setup(x => x.GetOrder(order))
                .Returns(hireOrder);

            // Act
            var newOrder = await orderService.CreateAsync(order);

            // Assert
            var success = await newOrder.AcceptOrderAsync();
            Assert.True(success);
            Assert.True(newOrder.Order.OrderState == OrderState.Accepted);
        }

        [Fact]
        public async Task RollbackOrderAsync_ShouldDeleteNewEmployeeAndBindedData()
        {
            // Arrange
            var employee = new Employee
            {
                Id = Guid.NewGuid()
            };

            var order = new Order
            {
                FirstName = "Vladymyr",
                LastName = "Bondarenko",
                OrderDescriptionId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
                EmployeeId = null
            };

            orderRepMock
                .Setup(x => x.CreateAsync(order))
                .ReturnsAsync(() => { order.Id = Guid.NewGuid(); return order; });

            orderRepMock
                .Setup(x => x.UpdateAsync(order))
                .ReturnsAsync(true);

            var hireOrder = new HireOrder(order, orderRepMock.Object, employeeServiceMock.Object, originalServiceMock.Object);

            employeeServiceMock
                .Setup(x => x.CreateAsync(hireOrder))
                .ReturnsAsync(employee);

            originalServiceMock
                .Setup(x => x.GetOriginalsAsync(It.IsAny<PaginationQuery>(), It.IsAny<GetAllOriginalsFilter>()))
                .ReturnsAsync(new List<Original>());

            employeeServiceMock
                .Setup(x => x.DeleteAsync(employee.Id))
                .ReturnsAsync(true);

            orderFactoryMock
                .Setup(x => x.GetOrder(order))
                .Returns(hireOrder);

            // Act
            var newOrder = await orderService.CreateAsync(order);

            // Assert
            var success = await newOrder.AcceptOrderAsync();
            if (success)
            {
                newOrder.Order.Employee = employee;
                success = await newOrder.RollbackOrderAsync();
            }
            Assert.True(success);
            Assert.True(newOrder.Order.OrderState == OrderState.Canceled);
        }
    }
}
