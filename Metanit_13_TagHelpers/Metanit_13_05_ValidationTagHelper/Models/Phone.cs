using System.ComponentModel.DataAnnotations;

namespace Metanit_13_05_ValidationTagHelper.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Range(50, 500)]
        [Required]
        public int Price { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
