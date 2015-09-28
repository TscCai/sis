using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.General.Index
{
    public class IndexModel
    {
        public GeneralInfo GeneralInfo { get; set; }
#if NODB
        public Unit[] Unit { get; set; }
#else
        public IQueryable<Unit> Unit{get;set;}
#endif

    }
}