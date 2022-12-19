using System.ComponentModel.DataAnnotations;

namespace Shop.Data
{
    public class Genders
    {
        [Key]
        public int GenderId { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
