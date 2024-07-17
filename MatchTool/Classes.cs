using System;

namespace Classes
{

    public class Player
    {
        public string username;
        public int elo;
        public int hiddenRating;
        public int placementGamesLeft;

        public Player(string username, int elo, int hiddenRating, int placementGamesLeft)
        {
            this.username = username;
            this.elo = elo;
            this.hiddenRating = hiddenRating;
            this.placementGamesLeft = placementGamesLeft;
        }
    }

    public class Game
    {
        public string name;
        public List<Player> players;

        public Game(string name, List<Player> players)
        {
            this.name = name;
            this.players = players;
        }

        private void addPlayer(string username, int defaultElo, int placementGameCount)
        {
            foreach (Player player in this.players)
            {
                if (player.username == username) //check if user already exists
                {
                    Console.WriteLine("Error: User already exists with that username!");
                    return;
                } else
                {
                    Player newPlayer = new Player(username, defaultElo, defaultElo, placementGameCount);
                    this.players.Add(newPlayer);
                    return;
                }
            }
        }
    }

    public class Match
    {
        public Player player1;
        public Player player2;

        public Match(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }
    }
}
