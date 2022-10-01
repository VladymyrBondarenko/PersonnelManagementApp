using Moq;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
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
    public class FireOrderTests
    {
        private readonly IOrderService orderService;
        private readonly Mock<IOrderRepository> orderRepMock = new();
        private readonly Mock<IOrderFactory> orderFactoryMock = new();
        private readonly Mock<IEmployeeService> employeeServiceMock = new();

        public FireOrderTests()
        {
            orderService = new OrderService(orderRepMock.Object, orderFactoryMock.Object, null);
        }

        [Fact]
        public async Task AcceptOrderAsync_ShouldFireEmployee()
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
                EmployeeId = employee.Id,
                Employee = employee
            };

            orderRepMock
                .Setup(x => x.CreateAsync(order))
                .ReturnsAsync(() => { order.Id = Guid.NewGuid(); return order; });

            orderRepMock
                .Setup(x => x.UpdateAsync(order))
                .ReturnsAsync(true);

            var fireOrder = new FireOrder(order, orderRepMock.Object, employeeServiceMock.Object);
            employeeServiceMock
                .Setup(x => x.UpdateAsync(employee))
                .ReturnsAsync(true);

            orderFactoryMock
                .Setup(x => x.GetOrder(order))
                .Returns(fireOrder);

            // Act
            var newOrder = await orderService.CreateAsync(order);

            // Assert
            var success = await newOrder.AcceptOrderAsync();
            Assert.True(success);
            Assert.True(newOrder.Order.Employee.EmployeeState == EmployeeState.Fired);
            Assert.True(newOrder.Order.OrderState ==  OrderState.Accepted);
        }

        [Fact]
        public async Task RollbackOrderAsync_ShouldHireEmployeeBack()
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
                EmployeeId = employee.Id,
                Employee = employee
            };

            orderRepMock
                .Setup(x => x.CreateAsync(order))
                .ReturnsAsync(() => { order.Id = Guid.NewGuid(); return order; });

            orderRepMock
                .Setup(x => x.UpdateAsync(order))
                .ReturnsAsync(true);

            var fireOrder = new FireOrder(order, orderRepMock.Object, employeeServiceMock.Object);
            employeeServiceMock
                .Setup(x => x.UpdateAsync(employee))
                .ReturnsAsync(true);

            orderFactoryMock
                .Setup(x => x.GetOrder(order))
                .Returns(fireOrder);

            // Act
            var newOrder = await orderService.CreateAsync(order);

            // Assert
            var success = await newOrder.AcceptOrderAsync();
            if (success)
            {
                success = await newOrder.RollbackOrderAsync();
            }

            Assert.True(success);
            Assert.True(newOrder.Order.Employee.EmployeeState == EmployeeState.Hired);
            Assert.True(newOrder.Order.OrderState == OrderState.Canceled);
        }
    }
}
