using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAISIS.Models
{
    public class Users : Database<Users>
    {
        [Key]
        public int? userID { get; set; }
        
        [Required]
        public string username { get; set; }
        
        [Required]
        public string password { get; set; }
        
        [Required]
        public string email { get; set; }
        
        [Required]
        public string avatar { get; set; }
        
        public int? admin { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }
    }
}