using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.EnvironmentalIndex
{
    public class DataCollection
    {
        public decimal SO2 { get; set; }

        public decimal NOX { get; set; }

        public decimal DSPC { get; set; }

        public decimal DNPC { get; set; }

        public DateTime Timestamp { get; set; }

    }
}