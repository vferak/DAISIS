using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DAISIS.Models
{
    public class User_game_rankings : Database<User_game_rankings>
    {
        [Key]
        public int? gameID { get; set; }
        
        [Key]
        public int? userID { get; set; }
        
        [Required]
        public int? rating { get; set; }
        
        public string text { get; set; }
        
        [Editable(false)]
        public DateTime? create_date { get; set; }
        
        public static IEnumerable<SelectListItem> GetSelectList()
        {
            var selectList = new List<SelectListItem>();
            for (var i = 1; i <= 10; i++)
            {
                selectList.Add(new SelectListItem {Text = i.ToString(), Value = i.ToString()});
            }
            return selectList;
        }
    }
}