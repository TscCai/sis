using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ncepu.GraduationProject.Sis.Web.Models.Account
{
    public class ModifyPasswordModel
    {
        public string OriginPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    }
}