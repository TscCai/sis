using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class BoilerInfo
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
        /// 减温水流量 Desuperheater Water Flow
        /// </summary>
        public decimal DWF { get; set; }

        /// <summary>
        /// 给水压力 Feed Water Pressure
        /// </summary>
        public decimal FWP { get; set; }

        /// <summary>
        /// 给水流量 Feed Water Flow
        /// </summary>
        public decimal FWF { get; set; }

        /// <summary>
        /// 排烟氧含量 Smoke Exhaust Oxygen content
        /// </summary>
        public decimal SEOC { get; set; }

        /// <summary>
        /// 排烟温度 Smoke Exhast Oxygen Temperature
        /// </summary>
        public decimal SEOT { get; set; }

        /// <summary>
        /// 燃煤量 Amount of Coal Burnning
        /// </summary>
        public decimal ACB { get; set; }

        /// <summary>
        /// 锅炉补给水 Boiler Feed Water
        /// </summary>
        public decimal BFW { get; set; }

        /// <summary>
        /// 空预器漏风率 Air Leakage Rate
        /// </summary>
        public decimal ALR { get; set; }
    }
}