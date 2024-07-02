
using Microsoft.EntityFrameworkCore;

namespace FileUploadTaskV2.Models
{
    public class ApplicationDbContext: DbContext
    {


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FileDetails> FileDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer("Server=DESKTOP-0LFAO0F;Database=NtierAppDB;TrustServerCertificate=True;Trusted_Connection=True;");
        }
    }
}
