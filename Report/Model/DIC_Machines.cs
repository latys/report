namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_Machines
    {
        public int id { get; set; }

        [StringLength(50)]
        public string machineno { get; set; }

        [StringLength(50)]
        public string machinename { get; set; }

        [StringLength(50)]
        public string note { get; set; }

        [StringLength(50)]
        public string processtype { get; set; }

        [StringLength(50)]
        public string manager { get; set; }
    }
}
