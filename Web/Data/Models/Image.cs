using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace Web.Data.Models
{
    public class Image
    {
        [DisplayName("Save path")]
        public string Path { get; set; }

        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
