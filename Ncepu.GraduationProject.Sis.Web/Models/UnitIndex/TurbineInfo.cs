using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class TurbineInfo
    {
        /// <summary>
        /// 机组编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 主汽压力 Main Steam Pressure
        /// </summary>
        public decimal MSP { get; set; }

        /// <summary>
        /// 主汽温度 Main Steam Temperature
        /// </summary>
        public decimal MST { get; set; }

        /// <summary>
        /// 再热气压 Reheat Steam Pressure
        /// </summary>
        public decimal RSP { get; set; }

        /// <summary>
        /// 再热温度 Reheat Steam Temperature
        /// </summary>
        public decimal RST { get; set; }

        /// <summary>
        /// 主汽流量 Main Steam Flow
        /// </summary>
        public decimal MSF { get; set; }

        /// <summary>
        /// 凝汽机真空 Condensing Engine Vaccum
        /// </summary>
        public decimal CEV { get; set; }
    }
}