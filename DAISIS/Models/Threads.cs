using System;
using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Threads : Database<Threads>
    {
        [Key]
        public int? threadID { get; set; }
        
        [Required]
        public int? gameID { get; set; }
        
        [Required]
        public int? userID { get; set; }
        
        [Required]
        public string title { get; set; }
        
        [Required]
        public string text { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }
    }
}