using MatchCsharp.Server.Socket.Packets;
using System;

namespace MatchCsharp.Server.Socket
{
    class ServerHandle
    {
        public static void Disconnect(SocketUser user, Packet packet)
        {
            
        }

        public static void JoinRoom(SocketUser user, Packet packet)
        {
            string roomName = packet.ReadString();//.Substring(1,20);
            Room room = MatchmakingServer.GetRoom(roomName);
            if (room != null)
            {
                RoomErrorEnum result = room.Join(user);
                if(result == RoomErrorEnum.roomJoined)
                {
                    ServerSend.JoinRoom(user, room);
                }
                else
                {
                    ServerSend.RoomError(user, result, roomName);
                }
            }
            else
            {
                ServerSend.RoomError(user, RoomErrorEnum.roomNotFound, roomName);
            }
        }
        public static void CreateRoom(SocketUser user, Packet packet)
        {
            Console.WriteLine("Create room from " + user.Nickname);

            int maxPlayers = Math.Clamp(packet.ReadInt(), 1, 10);
            string roomName = packet.ReadString();//.Substring(1, 20);

            if (user.UserState == UserState.connected)
            {
                Room room = MatchmakingServer.CreateRoom(roomName, user, new RoomCreateOptions { MaxPlayers = maxPlayers });
                if (room != null)
                {
                    ServerSend.JoinRoom(user, room);
                }
                else
                {
                    ServerSend.RoomError(user, RoomErrorEnum.roomAlreadyUsedName, roomName);
                }
            }
            else
            {
                ServerSend.RoomError(user, RoomErrorEnum.roomAlreadyJoined, roomName);
            }
        }
        public static void LeaveRoom(SocketUser user, Packet packet)
        {
            Room currentRoom = user.Room;
            if(currentRoom != null)
            {
                currentRoom.Leave(user);
                ServerSend.LeaveRoom(user, currentRoom);
            }
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
