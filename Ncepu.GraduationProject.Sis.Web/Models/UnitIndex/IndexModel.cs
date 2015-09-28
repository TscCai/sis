using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class IndexModel
    {
#if NODB
        /// <summary>
        /// 锅炉系统信息
        /// </summary>
        public BoilerInfo[] BoilerInfos { get; set; }

        /// <summary>
        /// 汽轮机系统信息
        /// </summary>
        public TurbineInfo[] TurbineInfos{get;set;}

        /// <summary>
        /// 电机系统信息
        /// </summary>
        public MotorInfo[] MotorInfos{get;set;}
#else
         /// <summary>
        /// 锅炉系统信息
        /// </summary>
        public IQueryable<BoilerInfo> BoilerInfos { get; set; }

        /// <summary>
        /// 汽轮机系统信息
        /// </summary>
        public IQueryable<TurbineInfo> TurbineInfos{get;set;}

        /// <summary>
        /// 电机系统信息
        /// </summary>
        public IQueryable<MotorInfo> MotorInfos{get;set;}
#endif
    }
}