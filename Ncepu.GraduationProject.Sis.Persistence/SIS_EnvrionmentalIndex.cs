//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ncepu.GraduationProject.Sis.Persistence
{
    using System;
    using System.Collections.Generic;
    
    public partial class SIS_EnvrionmentalIndex
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public Nullable<decimal> SO2 { get; set; }
        public Nullable<decimal> NOX { get; set; }
        public string DSPC { get; set; }
        public Nullable<decimal> DNPC { get; set; }
        public System.DateTime Timestamp { get; set; }
    }
}
