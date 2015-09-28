using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperWebSocket;
using SuperWebSocket.Protocol;

namespace Ncepu.GraduationProject.Sis.WsServer
{
    public class ServerManager
    {
        static IBootstrap bootstrap;
        static OpcUserServer ouSrv;
        static object sessionRoot = new object();
        public static void Start()
        {
            bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                return;
            }
            ouSrv = (OpcUserServer)bootstrap.AppServers.First();
            ouSrv.NewSessionConnected += new SessionHandler<OpcUserSession>(SessionConnected);
            ouSrv.SessionClosed += new SessionHandler<OpcUserSession, CloseReason>(SessionClosed);
            StartResult result = bootstrap.Start();
        }

        static void SessionClosed(OpcUserSession session, CloseReason reason)
        {
            lock (sessionRoot)
            {
                if (ouSrv.SessionCollection != null && ouSrv.SessionCollection.Count > 0)
                {
                    if (ouSrv.SessionCollection.Keys.Contains(session.Type) && ouSrv.SessionCollection[session.Type].Contains(session))
                    {
                        ouSrv.SessionCollection[session.Type].Remove(session);
                    }
                }
            }
            session.Logger.Info(reason);
        }

        static void SessionConnected(OpcUserSession session)
        {
            string type = session.Path.Substring(1);
            session.Type = type;

            if (!ouSrv.SessionCollection.Keys.Contains(session.Type))
            {
                List<OpcUserSession> sessionList = new List<OpcUserSession>();
                lock (sessionRoot)
                {
                    sessionList.Add(session);
                    ouSrv.SessionCollection.Add(session.Type, sessionList);
                }
            }
            else
            {
                lock (sessionRoot)
                {
                    ouSrv.SessionCollection[session.Type].Add(session);
                }
            }
        }

        public static void OuSrv_NewMessageReceived(OpcUserSession session, string message)
        {
            try
            {
                string[] type_msg = message.Split(':');
                if (type_msg[0] == "type")
                {
                    session.Type = type_msg[1];
                }
                if (!ouSrv.SessionCollection.Keys.Contains(session.Type))
                {
                    List<OpcUserSession> sessionList = new List<OpcUserSession>();
                    sessionList.Add(session);
                    ouSrv.SessionCollection.Add(session.Type, sessionList);
                }
                else
                {
                    ouSrv.SessionCollection[session.Type].Add(session);
                }
            }
            catch { }
        }

        public static void Stop()
        {
            bootstrap.Stop();
        }

        public static void BroadCast(string type, string message)
        {
            lock (sessionRoot)
            {
                IEnumerable<OpcUserSession> sessions = ouSrv.SessionCollection[type];
                foreach (OpcUserSession s in sessions)
                {
                    s.Send(message);
                }
            }
        }
    }
}
