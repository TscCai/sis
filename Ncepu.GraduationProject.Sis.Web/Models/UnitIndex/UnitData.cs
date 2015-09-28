using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ncepu.GraduationProject.Sis.Persistence;

namespace Ncepu.GraduationProject.Sis.Web.Models.UnitIndex
{
    public class UnitData
    {
        public int Id { get; set; }

        public bool IsChecked { get; set; }

        public List<SIS_BoilerIndex>[] BoilerCollection { get; set; }

        public List<SIS_TurbineIndex>[] TurbineCollection { get; set; }

        public List<SIS_MotorIndex>[] MotorCollection { get; set; }
    }
}