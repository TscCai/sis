using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperWebSocket;

namespace Ncepu.GraduationProject.Sis.WsServer
{
    public class OpcUserServer:WebSocketServer<OpcUserSession>
    {
        public Dictionary<string, List<OpcUserSession>> SessionCollection{get;set;}
        public OpcUserServer()
            : base()
        {
            SessionCollection = new Dictionary<string, List<OpcUserSession>>();
        }

    }
}
