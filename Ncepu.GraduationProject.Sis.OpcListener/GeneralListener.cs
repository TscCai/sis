using System;
using System.Collections;
using Tsclab.OpcAccess.Core;

namespace Ncepu.GraduationProject.Sis.OpcListener
{
    /// <summary>
    /// 模块：OpcListener
    /// 摘要：定制本系统所需的OpcListener
    /// 作者：Tsccai
    /// 编写日期：2013/2/3 19:28:38
    /// </summary>
    public class GeneralListener : Tsclab.OpcAccess.Core.OpcListener
    {
        public GeneralListener(OpcConfig config) : base(config) { }

        protected override Hashtable DoExtraProcess(Hashtable h)
        {
            if (!h.Contains("T_SU"))
            {
                h.Add("T_SU",0);
            }
            if (!h.ContainsKey("01_SU"))
            {
                h.Add("01_SU",0);
            }
            if (!h.ContainsKey("02_SU"))
            {
                h.Add("02_SU", 0);
            }
            if (!h.ContainsKey("03_SU"))
            {
                h.Add("03_SU", 0);
            }
            if (!h.ContainsKey("04_SU"))
            {
                h.Add("04_SU", 0);
            }

            return h;
        }
    }
}
