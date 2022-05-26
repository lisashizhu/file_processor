using System;
using FileProcessor.Common.Models;

namespace FileProcessor.Services.Interfaces
{
	public interface IFileProcessorFactory
	{
		IProcess Get(FileType fileType);
	}
}

