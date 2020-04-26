using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAISIS.Models
{
    public class Games : Database<Games>
    {
        [Key]
        public int? gameID { get; set; }

        [Required]
        public int? designerID { get; set; }
        
        [Required]
        public int? publisherID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int? min_player_count { get; set; }
        
        [Required]
        public int? max_player_count { get; set; }
        
        [Required]
        public int? min_game_time { get; set; }
        
        [Required]
        public int? max_game_time { get; set; }
        
        [Required]
        public int? age_limit { get; set; }
        
        [Required]
        public string description { get; set; }
        
        public string main_image { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }

        public Publishers GetPublisher(IEnumerable<Publishers> publishers)
        {
            return publishers.FirstOrDefault(element => element.publisherID == publisherID);
        }
    }
}