using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class MotorInfo
    {
        /// <summary>
        /// 机组编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 有功功率 Active Power
        /// </summary>
        public decimal AP { get; set; }

        /// <summary>
        /// 无功功率 Reactive Power
        /// </summary>
        public decimal RP{get;set;}


    }
}