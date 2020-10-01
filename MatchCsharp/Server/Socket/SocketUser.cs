using MatchCsharp.Server.Socket.Packet;
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
        #region ID
        private int _id;
        public int UserID { get { return _id; } }
        #endregion

        #region Nickname
        private string _nickname;
        public string Nickname { get { return _nickname; } }
        #endregion

        #region Room
        private Room _room;
        public Room Room { get { return _room; } }
        #endregion

        #region State
        private UserState _state = UserState.connected;
        public UserState UserState { get { return _state; } }
        #endregion



        public void JoinRoom(Room room)
        {
            _room = room;
            _state = UserState.inRoom;
            Console.WriteLine($"Player ({_nickname}) Joined to Room ({_room.RoomName})");
        }

        public void LeaveRoom()
        {
            if (_state == UserState.inRoom)
            {
                _room = null;
                _state = UserState.connected;
            }
        }

        protected override void OnOpen()
        {
            _id = MatchmakingServer.GenerateID();
            _nickname = Context.QueryString["nickname"];
            if(_nickname == null)
            {
                _nickname = "Player " + RandomUtil.GetInt(1, 9999);
            }
            Console.WriteLine($"{_nickname} Connected to Server");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsBinary)
            {
                var packet = new Packet(e.RawData);
                var packet_id = packet.ReadInt();


            }
        }
    }
}
