using Moq;
using Moq.Protected;
using PersonnelManagement.Application.FileOperations;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Infrastracture.FileOperations.Originals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PersonnelManagement.UnitTests.OriginalsTests
{
    public class OriginalServiceTests
    {
        private Mock<IFtpService> _ftpServiceMock = new ();
        private IOriginalService _originalService;
        private Mock<IOriginalRepository> _originalRepoMock = new();
        private FtpStructureSettings _ftpStructSettings;

        public OriginalServiceTests()
        {
            _ftpStructSettings = new FtpStructureSettings
            {
                EmpoloyeesDirectoryPath = "Employees",
                OrdersDirectoryPath = "Orders"
            };

            _originalService = new OriginalService(_ftpServiceMock.Object,
                _ftpStructSettings, _originalRepoMock.Object);
        }

        [Fact]
        public async Task AddOriginalAsync_ShouldReturnAddedOriginal_WhenAddingOriginalToOrder()
        {
            // Arrange
            var origType = OriginalType.Orders;
            var fileText = "Some text";
            var fileName = "tst.txt";

            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(executableLocation, fileName);

            await File.WriteAllTextAsync(filePath, fileText);

            var origCreateParams = new OriginalCreateParams 
            { 
                OrderId = Guid.NewGuid(),
                Bytes = File.ReadAllBytes(filePath),
                FileName = fileName
            };

            File.Delete(filePath);

            var remotePath = origType switch
            {
                OriginalType.Orders => _ftpStructSettings.OrdersDirectoryPath,
                OriginalType.Employees => _ftpStructSettings.EmpoloyeesDirectoryPath,
                _ => throw new NotImplementedException("Specified original type could not be handled")
            };

            var resultFilePath = Path.Combine(remotePath, fileName);

            _ftpServiceMock
                .Setup(x => x.SaveFileToFtpAsync(filePath, resultFilePath))
                .ReturnsAsync(true);

            var returnedOriginal = new Original
            {
                OriginalPath = resultFilePath
            };
            _originalRepoMock
                .Setup(x => x.CreateAsync(It.IsAny<Original>()))
                .ReturnsAsync(returnedOriginal);

            // Act
            var original = await _originalService.AddOriginalAsync(
                origCreateParams, origType);

            // Assert
            Assert.NotNull(original?.OriginalPath);
        }
    }
}
