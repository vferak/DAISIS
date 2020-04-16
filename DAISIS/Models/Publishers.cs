namespace DAISIS.Models
{
    public class Publishers : Database<Publishers>
    {
        public int publisherId { get; set; }
        
        public string name { get; set; }
        
        public string email { get; set; }
        
        public string web_page { get; set; }
    }
}