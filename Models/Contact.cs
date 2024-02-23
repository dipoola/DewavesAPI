using System.ComponentModel.DataAnnotations;

namespace DewavesAPI.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }

}
