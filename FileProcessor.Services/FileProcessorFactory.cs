using System;
using FileProcessor.Common.Models;
using FileProcessor.Services;
using FileProcessor.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileProcessor.Business
{
	public class FileProcessorFactory : IFileProcessorFactory
	{
        private readonly IServiceProvider _serviceProvider;
		public FileProcessorFactory(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
		}

        public IProcess Get(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Csv:
                    return ActivatorUtilities.CreateInstance<CsvFileProcessor>(_serviceProvider);
                default: return null;
            }
        }
    }
}

