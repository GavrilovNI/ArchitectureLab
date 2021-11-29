using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
