using System;
using System.Collections.Generic;
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

        public IEnumerable<Users> GetInactive()
        {
            var query =
                "SELECT * FROM users WHERE userID NOT IN (SELECT userID FROM threads WHERE create_date > DATEADD(month, -6, GETDATE())) AND userID NOT IN (SELECT userID FROM thread_comments WHERE create_date > DATEADD(month, -6, GETDATE()) ) AND userID NOT IN (SELECT userID FROM user_game_rankings WHERE create_date > DATEADD(month, -6, GETDATE()) );";
            return LoadSql(query);
        }
    }
}