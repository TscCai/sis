using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class QueryModel
    {
        public bool IsBoilerChecked { get; set; }

        public bool IsTurbineChecked { get; set; }

        public bool IsMotorChecked { get; set; }

        public DateTime Date_01 { get; set; }

        public DateTime Date_02 { get; set; }

        public UnitData[] Data { get; set; }

    }
}