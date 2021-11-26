using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public string DbPath { get; private set; }

        public DataContext()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;
            DbPath = projectDirectory + "\\Database\\TogetherСheaper.db";



            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}TogetherСheaper.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
