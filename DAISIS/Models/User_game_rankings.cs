using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DAISIS.Models
{
    public class User_game_rankings : Database<User_game_rankings>
    {
        [Key]
        public int? eventID { get; set; }
        
        [Key]
        public int? userID { get; set; }
        
        [Required]
        public int? rating { get; set; }
        
        public string text { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }
    }
}