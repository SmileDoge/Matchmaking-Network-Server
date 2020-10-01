using MatchCsharp.Server;
using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MatchCsharp
{
    class Program
    {

        static void Main(string[] args)
        {
            MatchmakingServer.Start(7000);

            Console.ReadKey();
        }
    }
}
