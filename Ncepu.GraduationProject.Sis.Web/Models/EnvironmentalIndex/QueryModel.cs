using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.EnvironmentalIndex
{
    public class QueryModel
    {
        public bool IsSO2Checked { get; set; }

        public bool IsNOxChecked { get; set; }

        public bool IsDSPCChecked { get; set; }

        public bool IsDNPCChecked { get; set; }

        public List<DataCollection>[] Data { get; set; }

        public DateTime Date_01 { get; set; }

        public DateTime Date_02 { get; set; }

    }
}