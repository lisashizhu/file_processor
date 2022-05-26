using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessor.Common.Exceptions;
using FileProcessor.Services.Interfaces;
using FileProcessor.WebApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace FileProcessor.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IFileProcessorFactory _fileProcessorFactory;

        public FileController(IFileProcessorFactory fileProcessorFactory)
        {
            _fileProcessorFactory = fileProcessorFactory;
        }

        [HttpPost("Save")]
        public async Task<ActionResult> CreateDataFromFile([FromBody] CreateDataFromFileRequest createDataFromFileRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(createDataFromFileRequest?.FileContent))
                {
                    throw new BusinessException("No data to be saved.");
                }

                var processor = _fileProcessorFactory.Get(createDataFromFileRequest.FileType);
                if (processor == null)
                {
                    throw new BusinessException("No processor can handle requested file type.");
                }
                await processor.SaveData(createDataFromFileRequest.FileContent);
                return StatusCode(201);
            }
            catch(BusinessException)
            {
                throw;
            }
            catch
            {
                throw new Exception("System Error.");
            }
        }
    }
}

