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
        
        private Users _users = new Users()
        {
            username = "Testovací jméno uživatele",
            password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08",
            email = "test@test.cz",
            avatar = "testovaciObrazek.jpg"
        };
        
        private User_game_rankings _userGameRankings = new User_game_rankings()
        {
            rating = 8,
        };
        
        private Threads _threads = new Threads()
        {
            title = "Testovací název příspěvku",
            text = "Testovací text příspěvku"
        };
        
        private User_thread_rankings _userThreadRankings = new User_thread_rankings()
        {
            rating = 4
        };
        
        private Thread_comments _threadComments = new Thread_comments()
        {
            text = "Testovací text komentáře příspěvku"
        };

        private Events _events = new Events()
        {
            title = "Testovací událost",
            date = DateTime.Now,
            place = "Testovací místo události",
            text = "Testovací text události"
        };
        
        private User_events _userEvents = new User_events(){};

        public string Run()
        {
            var result = "";

            _designers.Save();
            _designers = _designers.LoadOne();
            
            _publishers.Save();
            _publishers = _publishers.LoadOne();
            
            _games.designerID = _designers.designerID;
            _games.publisherID = _publishers.publisherID;
            _games.Save();
            _games = _games.LoadOne();
            
            _users.Save();
            _users = _users.LoadOne();

            _userGameRankings.gameID = _games.gameID;
            _userGameRankings.userID = _users.userID;
            _userGameRankings.Save();
            _userGameRankings = _userGameRankings.LoadOne();
            
            _threads.gameID = _games.gameID;
            _threads.userID = _users.userID;
            _threads.Save();
            _threads = _threads.LoadOne();

            _userThreadRankings.threadID = _threads.threadID;
            _userThreadRankings.userID = _threads.userID;
            _userThreadRankings.Save();
            _userThreadRankings = _userThreadRankings.LoadOne();

            _threadComments.threadID = _threads.threadID;
            _threadComments.userID = _users.userID;
            _threadComments.Save();
            _threadComments = _threadComments.LoadOne();
            
            _events.Save();
            _events = _events.LoadOne();

            _userEvents.eventID = _events.eventID;
            _userEvents.userID = _users.userID;
            _userEvents.Save();
            _userEvents = _userEvents.LoadOne();

            _designers.name = "Upravené jméno designéra";
            _designers.Save();
            _events.title = "Upravený název události";
            _events.Save();
            _games.name = "Upravené jméno hry";
            _games.Save();
            _publishers.name = "Upravené jméno vydavatele";
            _publishers.Save();
            _threadComments.text = "Upravený text komentáře příspěvku";
            _threadComments.Save();
            _threads.title = "Upravený název příspěvku";
            _threads.Save();
            _userGameRankings.rating = 4;
            _userGameRankings.Save();
            _userThreadRankings.rating = 9;
            _userThreadRankings.Save();
            _users.username = "Upravené jméno uživatele";
            _users.Save();

            _userEvents.Delete();
            _events.Delete();
            _threadComments.Delete();
            _userThreadRankings.Delete();
            _threads.Delete();
            _userGameRankings.Delete();
            _users.Delete();
            _games.Delete();
            _designers.Delete();
            _publishers.Delete();

            return result;
        }
    }
}