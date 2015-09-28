using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.General.Index
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIdChecked { get; set; }

        public decimal CurrentLoad { get; set; }
        public List<decimal> HourlyAvgLoadList { get; set; }
        public bool IsHourlyAvgLoadChecked { get; set; }

        public decimal InstalledCapacity { get; set; }
        public bool IsInstalledCapacityChecked { get; set; }

        public UnitStatus Status { get; set; }
        public bool IsStatusChecked { get; set; }

    }

    public enum UnitStatus
    {
        Running,
        Stop
    }

}