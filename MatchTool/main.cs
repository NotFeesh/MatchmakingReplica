using System;
using Classes;
using LoadSystem;

Loader loader = new Loader();

//Load from JSON file
List<Game> games = loader.Load("data.json");

while (true)
{
    string[] arguments = Console.ReadLine().Split(' ');
    string cmd = arguments[0];
    arguments = arguments.Skip(1).ToArray();
    switch (cmd)
    {
        case "clear":
        case "":
            Console.Clear();
            break;
        case "list":
            List<string> gameNames = new List<string>();

            foreach (Game game in games)
            {
                gameNames.Add(game.name);
            }

            if (arguments.Length > 0)
            {
                if (gameNames.Contains(arguments[0]))
                {
                    Game selectedGame = games[gameNames.IndexOf(arguments[0])] as Game;
                    foreach (Player player in selectedGame.players)
                    {
                        Console.WriteLine($"{player.username}\nCurrent Elo: {player.elo}\n{(player.placementGamesLeft <= 0 ? "" : $"Placements Left: {player.placementGamesLeft}")}");
                    }
                    break;
                }
            }
            
            foreach (string gameName in gameNames)
            {
                Console.WriteLine(gameName);
            }
            break;
        case "help":
            Console.WriteLine(@"clear: Clears the console.
list <optional: game>: Prints a list of all games. If a game is specified, instead prints a list of all players in the specified game.
");
            break;
        default:
            Console.WriteLine("Error: That's not a command! Use 'help' to see a list of commands.");
            break;
    }
}
    