using System.ComponentModel.DataAnnotations;

namespace FileUploadTaskV2.Models
{
	public class FilesViewModel
	{
		public List<FileDetails> Files { get; set; } = new List<FileDetails>();

		[DataType(DataType.Upload)]
		
		public IFormFile file {  get; set; }	
	}
}
