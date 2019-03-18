namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_StatData
    {
        public int id { get; set; }

        [StringLength(50)]
        public string processtype { get; set; }

        [StringLength(20)]
        public string reportdate { get; set; }

        [StringLength(50)]
        public string producttype { get; set; }

        [StringLength(10)]
        public string head { get; set; }

        public int? productnum { get; set; }

        [StringLength(50)]
        public string workgroup { get; set; }

        [StringLength(50)]
        public string workunit { get; set; }

        [StringLength(50)]
        public string machineno { get; set; }

        [StringLength(50)]
        public string startcode { get; set; }

        [StringLength(50)]
        public string endcode { get; set; }

        public int? startbox { get; set; }

        public int? endbox { get; set; }

        public int? istate { get; set; }
    }
}
