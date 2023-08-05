using Microsoft.Extensions.DependencyInjection;
using System;

namespace FlightSimulator.Extentions
{
    public static class Extentions
    {
        private static readonly Random random = new();
        private static readonly char[] ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static string GenerateRandomFlightName()
        {
            string s = "";
            for (int i = 0; i < 2; i++)
            {
                s += ABC[random.Next(ABC.Length)];
            }
            s += "-" + random.Next(1000, 9999);
            return s;
        }
    }
}
