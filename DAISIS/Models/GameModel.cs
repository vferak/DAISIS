using System;

namespace DAISIS.Models
{
    public class GamesModel
    {
        public int GameId  { get; set; }

        public int DesignerId { get; set; }
        
        public int PublisherId { get; set; }

        public string Name { get; set; }

        public int MinPlayerCount { get; set; }
        
        public int MaxPlayerCount { get; set; }
        
        public int MinGameTime { get; set; }
        
        public int MaxGameTime { get; set; }
        
        public int AgeLimit { get; set; }
        
        public string Description { get; set; }
        
    }
}