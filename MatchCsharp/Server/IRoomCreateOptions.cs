using System;
using System.Collections.Generic;
using System.Text;

namespace MatchCsharp.Server
{
    interface IRoomCreateOptions
    {
        int MaxPlayers { get; set; }

    }

    class RoomCreateOptions : IRoomCreateOptions
    {
        public int MaxPlayers { get; set; }
    }
}
