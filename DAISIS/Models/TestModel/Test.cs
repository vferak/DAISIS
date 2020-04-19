using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DAISIS.Models.TestModel
{
    public class Test
    {
        private Designers _designers = new Designers()
        {
            name = "Testovací Designér"
        };
        private Publishers _publishers = new Publishers()
        {
            name = "Testovací vydavatel",
            email = "test@test.cz",
            web_page = "www.test.cz"
        };
        
        private Games _games = new Games()
        {
            name = "Testovací název hry",
            min_player_count = 2,
            max_player_count = 4,
            min_game_time = 30,
            max_game_time = 60,
            age_limit = 10,
            description = "Testovací popisek hry"
        };

        private Events _events = new Events()
        {
            title = "Testovací událost",
            date = DateTime.Now,
            place = "Testovací místo události",
            text = "Testovací text události"
        };
        private Thread_comments _threadComments = new Thread_comments()
        {
            
        };
        private Threads _threads = new Threads()
        {
            
        };
        private User_events _userEvents = new User_events()
        {
            
        };
        private User_game_rankings _userGameRankings = new User_game_rankings()
        {
            
        };
        private User_thread_rankings _userThreadRankings = new User_thread_rankings()
        {
            
        };
        private Users _users = new Users()
        {
            
        };
        

        public string Run()
        {
            var result = "";

            _designers.Save();
            _publishers.Save();
            
            _designers = _designers.LoadOne();
            _publishers = _publishers.LoadOne();
            
            _games.designerID = _designers.designerID;
            _games.publisherID = _publishers.publisherID;
            
            _games.Save();
            
            _games = _games.LoadOne();
            
            _games.Delete();
            _designers.Delete();
            _publishers.Delete();

            return result;
        }
    }
}