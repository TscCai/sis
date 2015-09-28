using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.General.Index
{
    public class GeneralInfo
    {
        /// <summary>
        /// 总装机容量
        /// </summary>
        public decimal TotalInstalledCapacity { get; set; }
        public bool IsTotalInstalledCapacityChecked { get; set; }

        /// <summary>
        /// 实时总负荷
        /// </summary>
        public decimal CurrentTotalLoad { get; set; }
        public bool IsHourlyTotalAvgLoadChecked { get; set; }
        public List<decimal> HourlyAvgTotalLoadList { get; set; }

        /// <summary>
        /// 负荷率
        /// </summary>
        public decimal LoadRate { get; set; }

        /// <summary>
        /// 当日发电量，每小时更新
        /// </summary>
        public decimal DailyGenerate { get; set; }

        public bool IsDailyGenerateChecked { get; set; }

        /// <summary>
        /// 月累计发电量
        /// </summary>
        public decimal MonthlyGenerate { get; set; }
        public bool IsMonthlyGenerateChecked { get; set; }


        /// <summary>
        /// 年累计发电量
        /// </summary>
        public decimal YearlyGenerate { get; set; }
        public bool IsYearlyGenerateChecked { get; set; }
    }
}