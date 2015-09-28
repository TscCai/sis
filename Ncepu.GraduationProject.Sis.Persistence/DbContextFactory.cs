using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Ncepu.GraduationProject.Sis.Persistence
{
    /// <summary>
    /// 模块：
    /// 摘要：
    /// 作者：Tsccai
    /// 编写日期：2012/5/11 09:43:28
    /// </summary>
    public class DbContextFactory
    {
        static string conStr = ConfigurationManager.ConnectionStrings["DbModel"].ConnectionString;
        public static DbModelDataContext Create()
        {
            DbModelDataContext db = new DbModelDataContext(conStr);
            return db;
        }
    }
}
