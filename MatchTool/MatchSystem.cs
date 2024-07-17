using System;
using Classes;

namespace MatchSystem
{
    public class Matchmaker
    {
        public double MatchFairness(Match match)
        {
            double hrDifference = Math.Abs(match.player1.hiddenRating - match.player2.hiddenRating);
            return hrDifference <= 300 ? (10 / (1 + Math.Pow(Math.E, (-0.01 * (hrDifference - 300))))) : (10 / (1 + Math.Pow(Math.E, (-0.03 * (hrDifference - 300)))));
        }
    }
}
