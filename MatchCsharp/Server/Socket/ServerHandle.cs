using MatchCsharp.Server.Socket.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatchCsharp.Server.Socket
{
    class ServerHandle
    {
        public static void Disconnect(SocketUser user, Packet packet)
        {
            
        }

        public static void JoinRoom(SocketUser user, Packet packet)
        {

        }
        public static void CreateRoom(SocketUser user, Packet packet)
        {
            int maxPlayers = Math.Clamp(packet.ReadInt(), 0, 10);
            string roomName = packet.ReadString().Substring(0, 20);

            Room room = Server.MatchmakingServer.CreateRoom(roomName, user, new RoomCreateOptions { MaxPlayers = maxPlayers });
            if(room != null)
            {
                ServerSend.JoinRoom()
            }
        }
        public static void LeaveRoom(SocketUser user, Packet packet)
        {

        }

        public static void ListRoom(SocketUser user, Packet packet)
        {
            ServerSend.ListRoom(user);
        }

        public static void Event(SocketUser user, Packet packet)
        {

        }
    }
}
