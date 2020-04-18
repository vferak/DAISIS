using System;
using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Events : Database<Events>
    {
        [Key]
        public int? eventID { get; set; }
        
        [Required]
        public string title { get; set; }
        
        [Required]
        public DateTime? date { get; set; }
        
        [Required]
        public string place { get; set; }
        
        public string text { get; set; }
    }
}