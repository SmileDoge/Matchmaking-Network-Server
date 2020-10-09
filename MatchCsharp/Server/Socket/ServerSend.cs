using MatchCsharp.Server.Socket.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatchCsharp.Server.Socket
{
    class ServerSend
    {
        public static void RoomError(SocketUser user, RoomErrorEnum type, string roomName)
        {
            using (Packet packet = new Packet((int)PacketEnum.SERVER_ROOM_ERROR))
            {
                packet.Write(roomName);
                packet.Write((int)type);
                user.SendPacket(packet);
            }
        }

        public static void JoinRoom(SocketUser user, Room room)
        {
            foreach (var users in room.Users)
            {
                if (users.UserID != user.UserID)
                {
                    using (Packet packet = new Packet((int)PacketEnum.SERVER_JOIN_ROOM))
                    {
                        packet.Write(users.UserID);
                        packet.Write(users.Nickname);
                        user.SendPacket(packet);
                    }
                }
            }

            using (Packet packet = new Packet((int)PacketEnum.SERVER_JOIN_ROOM))
            {
                packet.Write(user.UserID);
                packet.Write(user.Nickname);
                foreach (var users in room.Users)
                {
                    users.SendPacket(packet);
                }
            }
        }

        public static void LeaveRoom(SocketUser user, Room room)
        {
            using (Packet packet = new Packet((int)PacketEnum.SERVER_LEAVE_ROOM))
            {
                packet.Write(user.UserID);
                foreach (var users in room.Users)
                {
                    users.SendPacket(packet);
                }
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
            try
            {
                using (Packet packet = new Packet((int)PacketEnum.SERVER_CONNECT))
                {
                    packet.Write(user.UserID);
                    user.SendPacket(packet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
