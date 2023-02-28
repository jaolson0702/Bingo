using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace BingoMonitor
{
    public class Bingo
    {
        private static Bingo instance;
        public Dictionary<int, bool> Values = new();

        private Bingo() => Reset();

        public List<int> UncalledNumbers => Values.Keys.WithValuesThatSatisfy(v => !Values[v]).ToList();
        public List<int> CalledNumbers => Values.Keys.WithValuesThatSatisfy(v => Values[v]).ToList();

        public bool HasCalled(int given)
        {
            Validate(given);
            return Values[given];
        }

        public string Call()
        {
            int called = UncalledNumbers.GetRandomElement();
            Values[called] = true;
            return ToString(called);
        }

        public void Reset()
        {
            Values = new();
            Values.Add(0, true);
            for (int a = 1; a <= 75; a++) Values.Add(a, false);
        }

        public static char LetterOf(int given)
        {
            Validate(given);
            if (given == 0) return 'N';
            if (given <= 15) return 'B';
            if (given <= 30) return 'I';
            if (given <= 45) return 'N';
            if (given <= 60) return 'G';
            return 'O';
        }

        public static string ToString(int given) => given == 0 ? "Free Space" : $"{LetterOf(given)}-{given}";

        public static Bingo Get
        {
            get
            {
                if (instance is null) instance = new();
                return instance;
            }
        }

        public static void Validate(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "The number is too low.");
            if (value > 75) throw new ArgumentOutOfRangeException(nameof(value), "The number is too high.");
        }
    }
}