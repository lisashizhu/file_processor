using System.ComponentModel.DataAnnotations;
using FileProcessor.Common.Models;

namespace FileProcessor.WebApi.Models
{
    public class CreateDataFromFileRequest
    {
        [Required]
        public string FileContent { get; set; }
        [Required]
        public FileType FileType   {get;set;}
    }
}