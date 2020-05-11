using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace DAISIS.Models
{
    public class Publishers : Database<Publishers>
    {
        [Key]
        public int? publisherID { get; set; }
        
        [Required][Display(Name="Jméno")]
        public string name { get; set; }
        
        [Required][Display(Name="E-mail")]
        public string email { get; set; }
        
        [Required][Display(Name="Webová stránka")]
        public string web_page { get; set; }

        public static IEnumerable<SelectListItem> GetSelectList(IEnumerable<Publishers> publishers)
        {
            return publishers.Select(publisher => new SelectListItem {Text = publisher.name, Value = publisher.publisherID.ToString()}).ToList();
        }
    }
}