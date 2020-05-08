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

        private Events _clonedEvent = new Events();
        
        private User_events _userEvents = new User_events(){};

        public string Run()
        {
            var result = "";

            // Funkce seřazeny podle logiky databáze, nikoliv dle číslování z analýzy
            
            // 2.1. Přidání designéra
            _designers.Save();
            // 2.2. Zobrazení designéra
            _designers = _designers.LoadOne();
            
            // 3.1. Přidání vydavatele
            _publishers.Save();
            // 3.2. Zobrazení vydavatele
            _publishers = _publishers.LoadOne();
            
            _games.designerID = _designers.designerID;
            _games.publisherID = _publishers.publisherID;
            // 1.1. Přidání hry
            _games.Save();
            // 1.2. Zobrazení hry
            _games = _games.LoadOne();
            
            // 4.1. Registrace uživatele
            _users.RegisterUser();
            _users.password = null;
            // 4.2. Zobrazení uživatele
            _users = _users.LoadOne();
            // 4.5. Zjištění všech neaktivních uživatelů
            _users.GetInactive();

            _userGameRankings.gameID = _games.gameID;
            _userGameRankings.userID = _users.userID;
            // 1.6. Přidání hodnocení hry
            _userGameRankings.Save();
            // 1.7. Zobrazení hodnocení hry
            _userGameRankings = _userGameRankings.LoadOne();
            
            _threads.gameID = _games.gameID;
            _threads.userID = _users.userID;
            // 5.1. Přidání příspěvku
            _threads.Save();
            // 5.2. Zobrazení příspěvků
            _threads = _threads.LoadOne();

            _userThreadRankings.threadID = _threads.threadID;
            _userThreadRankings.userID = _threads.userID;
            // 5.5. Přidání hodnocení příspěvků
            _userThreadRankings.Save();
            // 5.6. Zobrazení hodnocení příspěvku
            _userThreadRankings = _userThreadRankings.LoadOne();

            _threadComments.threadID = _threads.threadID;
            _threadComments.userID = _users.userID;
            // 6.1. Přidání komentáře
            _threadComments.Save();
            // 6.2. Zobrazení komentáře
            _threadComments = _threadComments.LoadOne();
            
            // 7.1. Přidání události
            _events.Save();
            // 7.2. Zobrazení události
            _events = _events.LoadOne();

            _userEvents.eventID = _events.eventID;
            _userEvents.userID = _users.userID;
            // 8.1. Přidání účasti
            _userEvents.Save();
            // 8.2. Zobrazení účasti
            _userEvents = _userEvents.LoadOne();
            
            _clonedEvent.eventID = _events.eventID;
            // 7.6 Vytvoření nové události z předchozí
            _clonedEvent.CloneEvent();
            // 7.5 Zobrazit nejlépe hodnocené hry pro zúčastněné uživatele události
            _clonedEvent.GetBestGamesFromParticipants();

            _games.name = "Upravené jméno hry";
            // 1.3. Editace hry
            _games.Save();
            _userGameRankings.rating = 4;
            // 1.8. Editace hodnocení hry
            _userGameRankings.Save();
            _designers.name = "Upravené jméno designéra";
            // 2.3. Editace designéra
            _designers.Save();
            _publishers.name = "Upravené jméno vydavatele";
            // 3.3. Editace vydavatele
            _publishers.Save();
            _users.username = "Upravené jméno uživatele";
            // 4.3. Editace uživatele
            _users.Save();
            _threads.title = "Upravený název příspěvku";
            // 5.3 Editace příspěvku
            _threads.Save();
            _userThreadRankings.rating = 9;
            // 5.7 Editace hodnocení příspěvku
            _userThreadRankings.Save();
            _threadComments.text = "Upravený text komentáře příspěvku";
            // 6.3. Editace komentáře příspěvku
            _threadComments.Save();
            _events.title = "Upravený název události";
            // 7.3. Editace události
            _events.Save();
            
            // 8.3 Nelze implementovat, účast lze jen přidat, zobrazit nebo odebrat

            _userEvents.eventID = null;
            // 8.4. Odstranění účasti
            _userEvents.Delete();
            // 7.4. Odstranění události
            _events.Delete();
            // 6.4. Odstranění komentáře
            _threadComments.Delete();
            // 5.8. Odstranění hodnocení příspěvku
            _userThreadRankings.Delete();
            // 5.4. Odstranění příspěvku
            _threads.Delete();
            // 1.9. Odstranění hodnocení hry
            _userGameRankings.Delete();
            // 4.4. Odstranění uživatele
            _users.Delete();
            // 1.4. Odtranění hry
            _games.Delete();
            
            // 1.5. TRIGGER Automatické smazání designéru a vydavatelů při odstranění hry (2.4., 3.4.)
            
            result = "Testy proběhly úspěšně!";
            
            return result;
        }
    }
}