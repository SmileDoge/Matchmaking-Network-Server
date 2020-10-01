using System;

namespace MatchCsharp.Server.Util
{
    class RandomUtil
    {
        private static Random _random;

        public static void Init()
        {
            _random = new Random();
        }
        public static int GetInt(int min, int max)
        {
            if(_random == null) { Init(); }
            return _random.Next(min, max);
        }
        public static double GetDouble()
        {
            if (_random == null) { Init(); }
            return _random.NextDouble();
        }
    }
}
