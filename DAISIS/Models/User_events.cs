using System;
using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class User_events : Database<User_events>
    {
        [Key]
        public int? eventID { get; set; }
        
        [Key]
        public int? userID { get; set; }
    }
}