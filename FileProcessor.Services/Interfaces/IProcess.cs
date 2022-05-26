using System;
using System.Threading.Tasks;

namespace FileProcessor.Services.Interfaces
{
	public interface IProcess
	{
		Task SaveData(string csvString);
	}
}

