using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsclab.OpcAccess;

namespace Ncepu.GraduationProject.Sis.OpcListener
{
    /// <summary>
    /// 模块：OPC数据处理
    /// 摘要：将读取到的OPC数据处理为网页显示的模式
    /// 作者：Tsccai
    /// 编写日期：2013/04/26 15:12:52
    /// </summary>
    public class GpRawDataProcessor
    {

        public static object CalcAP(object[] values)
        {
            decimal result=0m;
            if (values != null && values.Length > 0)
            {
                result = Convert.ToDecimal(values[0] )* 0.95m;
            }
            return result;
        }

        public static object CalcRP(object[] values)
        {
            decimal result = 0m;
            if (values != null && values.Length > 0)
            {
                result = Convert.ToDecimal(values[0]) * 0.05m;
            }
            return result;
        }

    }
}
