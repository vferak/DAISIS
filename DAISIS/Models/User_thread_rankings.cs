using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAISIS.Models
{
    public class User_thread_rankings : Database<User_thread_rankings>
    {
        [Key]
        public int? threadID { get; set; }
        
        [Key]
        public int? userID { get; set; }
        
        [Required]
        public int? rating { get; set; }
    }
}