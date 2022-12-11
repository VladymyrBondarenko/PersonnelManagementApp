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
                FtpRootFolder = "ftp",
                EntityFilesFolder = "Folder"
            };

            _originalService = new OriginalService(_ftpServiceMock.Object,
                _ftpStructSettings, _originalRepoMock.Object);
        }

        [Fact]
        public async Task AddOriginalAsync_ShouldReturnAddedOriginal_WhenAddingOriginalToOrder()
        {
            // Arrange
            var fileText = "Some text";
            var fileName = "tst.txt";

            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(executableLocation, fileName);

            await File.WriteAllTextAsync(filePath, fileText);

            var origCreateParams = new OriginalCreateParams
            {
                EntityId = Guid.NewGuid(),
                OriginalEntity = OriginalEntity.Orders,
                Bytes = File.ReadAllBytes(filePath),
                FileName = fileName
            };

            File.Delete(filePath);

            var remotePath = $"{_ftpStructSettings.FtpRootFolder}\\{_ftpStructSettings.EntityFilesFolder}";

            var resultFilePath = Path.Combine(remotePath, Path.GetRandomFileName() + Path.GetExtension(fileName));

            _ftpServiceMock
                .Setup(x => x.SaveFileToFtpAsync(filePath, It.IsAny<string>()))
                .ReturnsAsync(true);

            var returnedOriginal = new Original
            {
                OriginalPath = resultFilePath
            };
            _originalRepoMock
                .Setup(x => x.CreateAsync(It.IsAny<Original>()))
                .ReturnsAsync(returnedOriginal);

            // Act
            var original = await _originalService.AddOriginalAsync(origCreateParams);

            // Assert
            Assert.NotNull(original?.OriginalPath);
        }
    }
}
