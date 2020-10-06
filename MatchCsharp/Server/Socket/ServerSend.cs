using MatchCsharp.Server.Socket.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchCsharp.Server.Socket
{
    class ServerSend
    {
        public static void JoinRoom(SocketUser user, Room room)
        {
            using (Packet packet = new Packet((int)PacketEnum.SERVER_JOIN_ROOM))
            {
                packet.Write(user.UserID);
                packet.
            }
        }

        public static void ListRoom(SocketUser user)
        {
            using (Packet packet = new Packet((int)PacketEnum.SERVER_LIST_ROOM))
            {
                var rooms = Server.MatchmakingServer.Rooms.Take(50).ToArray();
                packet.Write(rooms.Length);
                foreach (var roomKeyValue in rooms)
                {
                    var room = roomKeyValue.Value;
                    packet.Write(room.RoomName);
                    packet.Write(room.Users.Count);
                    packet.Write(room.MaxPlayers);
                    packet.Write(room.Host.Nickname);
                }

                if(user != null)
                {
                    user.SendPacket(packet);
                }
            }
        }
        public static void Connect(SocketUser user)
        {
            using (Packet packet = new Packet((int)PacketEnum.SERVER_CONNECT))
            {
                packet.Write(user.UserID);
                user.SendPacket(packet);
            }
        }
    }
}
