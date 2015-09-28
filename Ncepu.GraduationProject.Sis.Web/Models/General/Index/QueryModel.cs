using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.General.Index
{
    public class QueryModel
    {
        //public string[] Common { get; set; }

        //public string[] Unit { get; set; }

        //public int[] UnitId { get; set; }

        //public string[] HourlyIndex { get; set; }

        public GeneralInfo GeneralInfo { get; set; }


        public Unit[] Unit { get; set; }

        public DateTime Date_01 { get; set; }

        public DateTime Date_02 { get; set; }

    }


}