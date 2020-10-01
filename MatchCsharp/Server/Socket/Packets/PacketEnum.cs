using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchCsharp.Server.Socket.Packets
{
    enum PacketEnum
    {
        CLIENT_DISCONNECT = 1,
        CLIENT_JOIN_ROOM,
        CLIENT_LEAVE_ROOM,
        CLIENT_LIST_ROOM,
        CLIENT_EVENT,

        SERVER_JOIN_ROOM = 100,
        SERVER_LEAVE_ROOM,
        SERVER_LIST_ROOM,
        SERVER_EVENT,
        SERVER_CONNECT,
        SERVER_DISCONNECT
    }
}
