using System.ComponentModel.DataAnnotations;

namespace RAZOR_CRUD
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(49)]
        public string Name { get; set; }

    }
}