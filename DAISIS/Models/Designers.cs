using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DAISIS.Models
{
    public class Designers : Database<Designers>
    {
        [Key]
        public int? designerID { get; set; }
        
        [Required][Display(Name="Jméno")]
        public string name { get; set; }
        
        public static IEnumerable<SelectListItem> GetSelectList(IEnumerable<Designers> designers)
        {
            return designers.Select(publisher => new SelectListItem {Text = publisher.name, Value = publisher.designerID.ToString()}).ToList();
        }
    }
}