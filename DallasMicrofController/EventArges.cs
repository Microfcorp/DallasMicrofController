using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DallasMicrofController
{
    class ServerStatusEventArg : EventArgs
    {
        public ServerStatus Status;

        public ServerStatusEventArg(ServerStatus status)
        {
            Status = status;
        }
    }
    public enum ServerStatus : byte
    {
        ServerStop,
        ServerStart,
        ServerError,
        ServerReadData,
    }
}
