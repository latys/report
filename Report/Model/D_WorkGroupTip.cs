namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_WorkGroupTip
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string MachineNo { get; set; }

        [StringLength(50)]
        public string workunit { get; set; }

        [StringLength(50)]
        public string workgroup { get; set; }

        [StringLength(500)]
        public string tip { get; set; }

        public DateTime? reportdate { get; set; }
    }
}
