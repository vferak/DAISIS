using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        
        [Required][Display(Name="Název")]
        public string title { get; set; }
        
        [Required][Display(Name="Text")]
        public string text { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }
        
        public Users GetUser()
        {
            return new Users(){userID = userID}.LoadOne();
        }

        public int GetRating()
        {
            return new User_thread_rankings() {threadID = threadID}.Load().Count();
        }
    }
}