using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket;

namespace Ncepu.GraduationProject.Sis.WsServer
{
    public class OpcUserSession:WebSocketSession<OpcUserSession>
    {
        public string Type { get; set; }
    }
}
