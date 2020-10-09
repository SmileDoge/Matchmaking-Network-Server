using System;
using System.Collections.Generic;
using System.Text;

namespace MatchCsharp.Server
{
    enum RoomErrorEnum
    {
        roomNotFound,
        roomMaxPlayers,
        roomAlreadyJoined,
        roomAlreadyUsedName,

        roomJoined,
    }

    class Room
    {
        public SocketUser Host { get; private set; }
        public string RoomName { get; private set; }
        public int MaxPlayers { get; private set; }

        public List<SocketUser> Users { get; private set; }

        public Room(string roomName, SocketUser host, IRoomCreateOptions options) {
            RoomName = roomName;
            Host = host;
            Users = new List<SocketUser>();
            MaxPlayers = options.MaxPlayers;
        }

        public RoomErrorEnum Join(SocketUser user)
        {
            if(user.UserState == UserState.inRoom)
            {
                return RoomErrorEnum.roomAlreadyJoined;
            }
            else if(Users.Count == MaxPlayers)
            {
                return RoomErrorEnum.roomMaxPlayers;
            }
            Users.Add(user);
            user.JoinRoom(this);
            return RoomErrorEnum.roomJoined;
        }

        public void Leave(SocketUser user) 
        {
            if(user.UserState == UserState.connected)
            {
                return;
            }
            else if(Users.Exists(x => x.UserID == user.UserID) == false)
            {
                return;
            }
            Users.Remove(user);
            user.LeaveRoom();
        }

        public SocketUser GetUserByName(string nickname)
        {
            foreach (var user in Users)
            {
                if(user.Nickname == nickname)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
