using System;

namespace Classes
{

    public class Player
    {
        public string username;
        public int elo;
        public int hiddenRating;
        public int placementGamesLeft;

        public Player(string username, int elo, int hiddenRating)
        {
            this.username = username;
            this.elo = elo;
            this.hiddenRating = hiddenRating;
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

        private void addPlayer(string username, int defaultElo)
        {
            foreach (Player player in this.players)
            {
                if (player.username == username) //check if user already exists
                {
                    Console.WriteLine("Error: User already exists with that username!");
                    return;
                } else
                {
                    Player newPlayer = new Player(username, defaultElo, defaultElo);
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

        public double matchFairness;

        public int player1Win;
        public int player2Win;
        public int player1Lose;
        public int player2Lose;

        public Match(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            double hrDifference = Math.Abs(player1.hiddenRating - player2.hiddenRating);
            this.matchFairness = 10 - (hrDifference <= 300 ? (10 / (1 + Math.Pow(Math.E, (-0.01 * (hrDifference - 300))))) : (10 / (1 + Math.Pow(Math.E, (-0.03 * (hrDifference - 300))))));

            int adjustment1 = player1.hiddenRating - player1.elo;
            int adjustment2 = player2.hiddenRating - player2.elo;

            if (player1.hiddenRating == player2.hiddenRating)
            {
                this.player1Win = 20 + (adjustment1 / 10);
                this.player1Lose = 20 + (adjustment1 / 10);
                this.player2Win = 20 + (adjustment2 / 10);
                this.player2Lose = 20 + (adjustment2 / 10);
                Console.WriteLine("1");
            } else if (player1.hiddenRating > player2.hiddenRating)
            {
                this.player1Win = 20 - (Convert.ToInt32(10 - matchFairness) * 20) + (adjustment1 / 10);
                this.player1Lose = 20 + (Convert.ToInt32(10 - matchFairness) * 20) + (adjustment1 / 10);
                this.player2Win = 20 + (Convert.ToInt32(10 - matchFairness) * 20) + (adjustment2 / 10);
                this.player2Lose = 20 - (Convert.ToInt32(10 - matchFairness) * 20) + (adjustment2 / 10);
                Console.WriteLine("2");
            } else
            {
                this.player1Win = 20 + (Convert.ToInt32(10 - matchFairness) / 10 * 20) + (adjustment1 / 10);
                this.player1Lose = (Convert.ToInt32(10 - matchFairness) / 10 * 20) + (adjustment1 / 10) - 20;
                this.player2Win = 20 - (Convert.ToInt32(10 - matchFairness) / 10 * 20) + (adjustment2 / 10);
                this.player2Lose = 20 + (Convert.ToInt32(10 - matchFairness) / 10 * 20) + (adjustment2 / 10);
                Console.WriteLine("3");
            }
        }
    }
}
