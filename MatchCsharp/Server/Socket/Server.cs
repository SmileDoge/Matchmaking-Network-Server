using MatchCsharp.Server.Util;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp.Server;

namespace MatchCsharp.Server
{
    enum UserState
    {
        connected = 1,
        inRoom,
    }

    class MatchmakingServer
    {
        private static Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
        private static Dictionary<int, SocketUser> _users = new Dictionary<int, SocketUser>();
        private static WebSocketServer _websocket;

        public static bool Started { get; private set; }

        public static int Port { get; private set; }

        public static void Start(int port)
        {
            Port = port;

            Console.WriteLine("Server Started");

            Started = true;

            RandomUtil.Init();

            _websocket = new WebSocketServer(port);
            _websocket.AddWebSocketService<SocketUser>("/");
            _websocket.Start();
        }

        public static int GenerateID()
        {
            int id = RandomUtil.GetInt(0, 1000);
            if(_users.ContainsKey(id)) {
                return GenerateID();
            }else{
                return id;
            }

        }

        public static Room CreateRoom(string roomName, SocketUser host, IRoomCreateOptions options)
        {
            if (Started)
            {
                if (_rooms.ContainsKey(roomName))
                {
                    return null;
                }
                else
                {
                    Room room = new Room(roomName, host, options);
                    _rooms.Add(roomName, room);
                    host.JoinRoom(room);
                    return room;
                }
            }
            else
            {
                return null;
            }
        }

        public static Room GetRoom(string roomName)
        {
            if (Started)
            {
                if (_rooms.TryGetValue(roomName, out Room room))
                {
                    return room;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
