using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Publishers : Database<Publishers>
    {
        [Key]
        public int? publisherID { get; set; }
        
        [Required]
        public string name { get; set; }
        
        [Required]
        public string email { get; set; }
        
        [Required]
        public string web_page { get; set; }
    }
}