namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_ProcessType
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ProcessType { get; set; }

        [StringLength(50)]
        public string ProcessName { get; set; }

        [StringLength(50)]
        public string Metarial { get; set; }

        [StringLength(50)]
        public string Unite { get; set; }

        public bool? IsBCode { get; set; }

        [StringLength(50)]
        public string note { get; set; }
    }
}
