using System;
using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Games
    {
        [Key]
        public int gameID  { get; set; }

        public int designerID { get; set; }
        
        public int publisherID { get; set; }

        public string name { get; set; }

        public int min_player_count { get; set; }
        
        public int max_player_count { get; set; }
        
        public int min_game_time { get; set; }
        
        public int max_game_time { get; set; }
        
        public int age_limit { get; set; }
        
        public string description { get; set; }
        
        public string main_image { get; set; }
        
        public DateTime create_date { get; set; }
    }
}