using System;
using System.Threading.Tasks;
using FileProcessor.Common.Exceptions;
using FileProcessor.Common.Models;
using FileProcessor.Services.Interfaces;
using FileProcessor.WebApi.Controllers;
using FileProcessor.WebApi.Models;
using Moq;
using Xunit;

namespace FileProcessor.Test
{
    public class FileControllerTestFixture
    {
        private readonly Mock<IFileProcessorFactory> _mockFileProcessorFactory = new Mock<IFileProcessorFactory>();

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task Ensure_FileController_ThrowExceptionWhenFileContentIsNullOrEmpty(string fileContent)
        {
            var fileController = new FileController(_mockFileProcessorFactory.Object);
            var res = await Assert.ThrowsAsync<BusinessException>(() => fileController.CreateDataFromFile(new CreateDataFromFileRequest
            {
                FileContent= fileContent,
                FileType = Common.Models.FileType.Csv
            }));

            Assert.NotNull(res);
            Assert.Equal(res.Message, "No data to be saved.");
        }

        [Fact]
        public async Task Ensure_FileController_ThrowExceptionWhenFileTypeIsNotSupported()
        {
            var fileController = new FileController(_mockFileProcessorFactory.Object);
            _mockFileProcessorFactory.Setup(obj => obj.Get(It.IsAny<FileType>())).Returns((IProcess)null);
            var res = await Assert.ThrowsAsync<BusinessException>(() => fileController.CreateDataFromFile(new CreateDataFromFileRequest
            {
                FileContent = "abc",
                FileType = Common.Models.FileType.Null
            }));

            Assert.NotNull(res);
            Assert.Equal(res.Message, "No processor can handle requested file type.");
        }

        [Fact]
        public async Task Ensure_FileController_CreateDataFromFile()
        {
            var fileController = new FileController(_mockFileProcessorFactory.Object);
            Mock<IProcess> mockProcess = new Mock<IProcess>();
            _mockFileProcessorFactory.Setup(obj=>obj.Get(It.IsAny<FileType>())).Returns(mockProcess.Object);
            var res = await fileController.CreateDataFromFile(new CreateDataFromFileRequest
            {
                FileContent = "abc",
                FileType = Common.Models.FileType.Csv
            });

            Assert.NotNull(res);
            _mockFileProcessorFactory.Verify(x => x.Get(It.IsAny<FileType>()), Times.Once);
        }
    }
}

