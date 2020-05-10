using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAISIS.Models
{
    public class Games : Database<Games>, IValidatableObject
    {
        [Key][Display(Name = "Id hry")]
        public int? gameID { get; set; }

        [Required][Display(Name = "Designér")]
        public int? designerID { get; set; }
        
        [Required][Display(Name = "Vydavatel")]
        public int? publisherID { get; set; }

        [Required][Display(Name="Název")]
        public string name { get; set; }

        [Required][Display(Name="Minimální počet hráčů")]
        public int? min_player_count { get; set; }
        
        [Required][Display(Name="Maximální počet hráčů")]
        public int? max_player_count { get; set; }
        
        [Required][Display(Name="Minimální herní doba")]
        public int? min_game_time { get; set; }
        
        [Required][Display(Name="Maximální herní doba")]
        public int? max_game_time { get; set; }
        
        [Required][Display(Name="Věkový limit")]
        public int? age_limit { get; set; }
        
        [Required][Display(Name="Popis")]
        public string description { get; set; }
        
        [Display(Name="Hlavní obrázek")]
        public string main_image { get; set; }
        
        [Editable(false)][Display(Name="Datum vytvoření")]
        public DateTime? create_date { get; set; }
        
        [Editable(false)][Display(Name="Hlavní obrázek")]
        public HttpPostedFileBase image_file { get; set; }  

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (min_player_count > max_player_count) {
                yield return new ValidationResult("Maximální počet hráčů musí být větší než minimální.");
            }
            if (min_game_time > max_game_time) {
                yield return new ValidationResult("Maximální herní doba musí být delší než minimální.");
            }
        }
        public Publishers GetPublisher(IEnumerable<Publishers> publishers)
        {
            return publishers.FirstOrDefault(element => element.publisherID == publisherID);
        }

        public Designers GetDesigner()
        {
            return new Designers(){ designerID = designerID }.LoadOne();
        }
    }
}