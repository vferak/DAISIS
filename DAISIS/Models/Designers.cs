using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Designers : Database<Designers>
    {
        [Key]
        public int? designerID { get; set; }
        
        [Required]
        public string name { get; set; }
    }
}