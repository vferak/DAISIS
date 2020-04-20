using System;
using System.Collections.Generic;
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

        public IEnumerable<Games> GetBestGamesFromParticipants()
        {
            var query = $"SELECT g.gameID, g.name FROM games g JOIN user_game_rankings ugr ON g.gameID = ugr.gameID WHERE ugr.userID IN (SELECT ue.userID FROM events e JOIN user_events ue on e.eventID = ue.eventID WHERE e.eventID = {eventID} ) GROUP BY g.gameID, g.name ORDER BY FORMAT(AVG(CAST(ugr.rating AS FLOAT)), 'N2') DESC;";
            return new Games().LoadSql(query);
        }
    }
}