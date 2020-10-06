using System;
using System.Collections.Generic;
using System.Text;

namespace MatchCsharp.Server
{
    class Room
    {
        public SocketUser Host { get; private set; }
        public string RoomName { get; private set; }
        public int MaxPlayers { get; private set; }

        public List<SocketUser> Users { get; set; }

        public Room(string roomName, SocketUser host, IRoomCreateOptions options) {
            RoomName = roomName;
            Host = host;
            Users = new List<SocketUser>();
            MaxPlayers = options.MaxPlayers;
        }
    }
}
