using System;
using System.Collections.Generic;
using System.Text;

namespace MatchCsharp.Server
{
    class Room
    {
        private SocketUser _host;
        private string _roomName;
        private int _maxPlayers;


        public SocketUser Host { get { return _host; } }
        public string RoomName { get { return _roomName; } }
        public int MaxPlayers { get { return _maxPlayers; } }

        public List<SocketUser> Users = new List<SocketUser>();

        public Room(string roomName, SocketUser host, IRoomCreateOptions options) {
            _roomName = roomName;
            _host = host;

            _maxPlayers = options.MaxPlayers;
        }
    }
}
