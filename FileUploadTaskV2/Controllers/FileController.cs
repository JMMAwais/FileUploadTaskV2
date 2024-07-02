using FileUploadTaskV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadTaskV2.Controllers
{
	public class FileController : Controller
	{
		private readonly ApplicationDbContext _context;
        public FileController(ApplicationDbContext context)
        {
				_context = context;
        }
        public IActionResult Index()
		{
			var model = new FilesViewModel();
			//foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "upload")))
			//{
				//model.Files.Add(
				//	new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
			//}

			return View();
		}

	


        [HttpPost]
      
        public IActionResult Index(IFormFile[] files, FilesViewModel models)
		{
			//Path.GetExtension(model.Files)
			var supportType = new[] { ".txt", ".doc", ".pdf" };
			foreach (var item in files)
			{
				var aa = System.IO.Path.GetExtension(item.Name);/*Substring(1)*/
				if (!supportType.Contains(aa))
				{
					ViewBag.Message = "Only txt, doc, are allowed  ";
					return View();
				}
			}
			   

			if(files == null || files.Length == 0)
			{
				ViewBag.Message = "Atleast Select 1 file to further proceed";
				return View();
			}


			if (files.Any(x => x.Length > 1000000))
			{
				return BadRequest("more than 1 MB");
			}

			if ( files.Length < 3)
			{
				// Iterate each file
				foreach (var file in files)
                {
					
                    
                        // Get the file name from the browser
                        var fileName = System.IO.Path.GetFileName(file.FileName);

					// Get the file path to be uploaded
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "D:\\PracaticeProject", fileName);

					// Check if a file with the same name exists and delete it
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
					}

					// Create a new local file and copy contents of the uploaded file
					using (var localFile = System.IO.File.OpenWrite(filePath))
					using (var uploadedFile = file.OpenReadStream())
					{
						uploadedFile.CopyTo(localFile);
					}
				}
			}
			else
			{
				ViewBag.Message = "You cannot upload more than 3 files!";
				return View();
			}
			
			ViewBag.Message = "Files are successfully uploaded";
			var model = new FilesViewModel();
			foreach (var file in files)
			{
				model.Files.Add(
					  new FileDetails { Name = System.IO.Path.GetFileName(file.FileName) });
			}

			// Get files from the server
			//var model = new FilesViewModel();
			//foreach (var item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "D:\\PracaticeProject")))
			//{
			//	model.Files.Add(
			//		new FileDetails { Name = System.IO.Path.GetFileName(item), Path = item });
			//}
			return View(model);
		}

	}
}
