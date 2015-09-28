using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsclab.OpcAccess.Core;

namespace Ncepu.GraduationProject.Sis.OpcListener
{
    /// <summary>
    /// 模块：OpcListener
    /// 摘要：
    /// 作者：Tsccai
    /// 编写日期：2013/5/11 09:43:28
    /// </summary>
    public class ListenerManager
    {
        static IOpcListener genListener;
        static IOpcListener untIdxListener;
        static IOpcListener envIdxListener;
        
        static string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/Config/Tsclab.OpcAccess.config";
        public static void Start()
        {

            genListener = new GeneralListener(
             OpcConfigManager.Configure(configPath, "General.Index")
             );
            genListener.OPCDataChanged += new OpcDataChangedEventHandler(IOpcListener_OPCDataChanged);
            genListener.Start();

            untIdxListener = new Tsclab.OpcAccess.Core.OpcListener(
                OpcConfigManager.Configure(configPath, "UnitIndex.Index")
                );
            untIdxListener.OPCDataChanged += new OpcDataChangedEventHandler(IOpcListener_OPCDataChanged);
            untIdxListener.Start();

            envIdxListener = new Tsclab.OpcAccess.Core.OpcListener(
                OpcConfigManager.Configure(configPath, "EnvironmentalIndex.Index")
                );
            envIdxListener.OPCDataChanged += new OpcDataChangedEventHandler(IOpcListener_OPCDataChanged);
            envIdxListener.Start();
        }
        public static void Stop()
        {
            genListener.Close();
            untIdxListener.Close();
            envIdxListener.Close();
        }
        static void IOpcListener_OPCDataChanged(object sender, OpcDataChangedEventArgs e)
        {
            WsServer.ServerManager.BroadCast(e.Type, e.DataChangedResult.ToJSON());
        }

    }
}
