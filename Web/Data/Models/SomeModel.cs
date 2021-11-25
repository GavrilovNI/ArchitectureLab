using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Web.Data.Models
{
    public class SomeModel
    {
        [BindNever]
        public IEnumerable<Car> cars { get; set; }


        [Display(Name = "Display")]
        [StringLength(10)]
        [Required(ErrorMessage = "Name Error!!!")]
        public string Name { get; set; }
    }
}
