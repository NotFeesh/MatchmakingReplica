using System;
using Classes;
using LoadSystem;
using MatchSystem;

Loader loader = new Loader();

//Load from JSON file
List<Game> games = loader.Load("data.json");

while (true)
{
    string[] arguments = Console.ReadLine().Split(' ');
    string cmd = arguments[0];
    arguments = arguments.Skip(1).ToArray();

    List<string> gameNames = new List<string>();
    foreach (Game game in games)
    {
        gameNames.Add(game.name);
    }

    switch (cmd)
    {
        case "clear":
        case "":
            Console.Clear();
            break;
        case "save":
            loader.Save("data.json", games);
            Console.WriteLine("Data saved successfully!");
            break;
        case "exit":
            Environment.Exit(0);
            break;
        case "list":
            

            if (arguments.Length > 0)
            {
                if (gameNames.Contains(arguments[0]))
                {
                    Game selectedGame = games[gameNames.IndexOf(arguments[0])] as Game;
                    foreach (Player player in selectedGame.players)
                    {
                        Console.WriteLine($"{player.username}\nCurrent Elo: {player.elo}\nPlacements Left: {(player.placementGamesLeft <= 0 ? "0" : player.placementGamesLeft)}\n");
                    }
                    break;
                }
            }
            
            foreach (string gameName in gameNames)
            {
                Console.WriteLine(gameName);
            }
            break;
        case "create":
            string newNameRaw = arguments.Length > 0 ? arguments[0] : "Game";
            string newName = newNameRaw;
            int counter = 0;

            while (gameNames.Contains(newName))
            {
                counter++;
                newName = newNameRaw + counter.ToString();
            }  

            Game newGame = new Game(newName, new List<Player>());
            games.Add(newGame);
            Console.WriteLine($"Game '{newName}' was added successfully!");

            break;
        case "add":
            if (arguments.Length > 0 && gameNames.Contains(arguments[0]))
            {
                Game selectedGame = games[gameNames.IndexOf(arguments[0])];
                newNameRaw = arguments.Length > 1 ? arguments[1] : "Player";
                newName = newNameRaw;
                counter = 0;

                List<string> playerNames = new List<string>();
                foreach(Player player in selectedGame.players)
                {
                    playerNames.Add(player.username);
                }

                while (playerNames.Contains(newName))
                {
                    counter++;
                    newName = newNameRaw + counter.ToString();
                }

                Player newPlayer = new Player(newName, 0, 0, 5);
                selectedGame.players.Add(newPlayer);
                Console.WriteLine($"Player '{newName}' was added succesfully to '{selectedGame.name}'");
            }
            else
            {
                Console.WriteLine("Error: Not enough arguments! Use 'help' for proper usage.");
            }
            break;
        case "remove":
            if (arguments.Length > 0)
            {
                if (arguments.Length > 1 && gameNames.Contains(arguments[1]))
                {
                    if (arguments[0] == "game")
                    {
                        games.RemoveAt(gameNames.IndexOf(arguments[1]));
                        Console.WriteLine($"Game {arguments[1]} was removed successfully!");
                    } else if (arguments.Length > 2 && arguments[0] == "player")
                    {
                        Game selectedGame = games[gameNames.IndexOf(arguments[1])] as Game;
                        foreach(Player player in selectedGame.players)
                        {
                            if (arguments[2] == player.username)
                            {
                                selectedGame.players.Remove(player);
                                Console.WriteLine($"Successfully removed {player.username}!");
                            }
                        }
                    } else
                    {
                        Console.WriteLine("Error: Incorrect Usage. Use 'help' to see usage.");
                    }
                } else
                {
                    Console.WriteLine("Error! That game doesn't exist.");
                }
                
            } else
            {
                Console.WriteLine("Error: Not enough arguments! Use 'help' to see usage.");
            }
           
            break;
        case "matchmake":
            if (arguments.Length > 0)
            {

            } else
            {
                Console.WriteLine("Error: Not enough arguments! Use 'help' to see usage.");
            }
            break;
        case "match":
            break;
        case "help":
            Console.WriteLine(@"clear: Clears the console.
save: Writes all new data to your save file.
exit: Closes the program.
list <optional: game>: Prints a list of all games. If a game is specified, instead prints a list of all players in the specified game.
create <optional: name>: Creates a new game with specified name. If no name is specified, the name will default to 'Game.'
add <game> <optional: name>: Creates a new player with specified name in the specified game. If no name is specified, the name will default to 'Player.'
remove game <game>: Removes specified game.
remove player <game> <player>: Removes specified player in specified game
matchmake <game>: Recommends best matches for specified game.
match <game> <player1> <player2>: Begins a match between specified players.
");
            break;
        default:
            Console.WriteLine("Error: That's not a command! Use 'help' to see a list of commands.");
            break;
    }
}
    