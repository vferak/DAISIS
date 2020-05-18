using System;
using System.ComponentModel.DataAnnotations;

namespace DAISIS.Models
{
    public class Thread_comments : Database<Thread_comments>
    {
        [Key]
        public int? commentID { get; set; }
        
        [Required]
        public int? threadID { get; set; }
        
        [Required]
        public int? userID { get; set; }
        
        [Required][Display(Name="Text")]
        public string text { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }

        public Users GetUser()
        {
            return new Users(){userID = userID}.LoadOne();
        }
    }
}