using MatchCsharp.Server.Socket;
using MatchCsharp.Server.Socket.Packets;
using MatchCsharp.Server.Util;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MatchCsharp.Server
{
    class SocketUser: WebSocketBehavior
    {
        public int UserID { get; private set; }
        public string Nickname { get; private set; }
        public Room Room { get; private set; }
        public UserState UserState { get; private set; }
        

        public delegate void PacketHandler(SocketUser user, Packet packet);
        private Dictionary<int, PacketHandler> _packetHandlers = new Dictionary<int, PacketHandler>();

        public void JoinRoom(Room room)
        {
            Room = room;
            UserState = UserState.inRoom;
            Console.WriteLine($"Player ({Nickname}) Joined to Room ({Room.RoomName})");
        }

        public void LeaveRoom()
        {
            if (UserState == UserState.inRoom)
            {
                Room = null;
                UserState = UserState.connected;
            }
        }

        public void SendPacket(Packet packet)
        {
            Send(packet.ToArray());
        }

        protected override void OnOpen()
        {
            UserID = MatchmakingServer.GenerateID();
            Nickname = Context.QueryString["nickname"];
            if(Nickname == null)
            {
                Nickname = "Player " + RandomUtil.GetInt(1, 9999);
            }
            InitializePackets();
            Server.MatchmakingServer.Users.Add(UserID, this);
            Console.WriteLine($"{Nickname} Connected to Server");
        }

        private void InitializePackets()
        {
            _packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)PacketEnum.CLIENT_DISCONNECT, ServerHandle.Disconnect},
                { (int)PacketEnum.CLIENT_JOIN_ROOM, ServerHandle.JoinRoom},
                { (int)PacketEnum.CLIENT_LEAVE_ROOM, ServerHandle.LeaveRoom},
                { (int)PacketEnum.CLIENT_LIST_ROOM, ServerHandle.ListRoom},
                { (int)PacketEnum.CLIENT_EVENT, ServerHandle.Event},
            };
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsBinary)
            {
                var data = e.RawData;
                using (var packet = new Packet(data))
                {
                    var packet_id = packet.ReadInt();
                    if (_packetHandlers.TryGetValue(packet_id, out PacketHandler value))
                    {
                        value(this, packet);
                    }
                }
            }
        }
    }
}
