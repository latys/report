namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_BoxCollectResult
    {
        public int id { get; set; }

        [StringLength(50)]
        public string TaskCode { get; set; }

        [StringLength(50)]
        public string CarCode { get; set; }

        [StringLength(50)]
        public string ProcessType { get; set; }

        [StringLength(50)]
        public string ProductType { get; set; }

        [StringLength(50)]
        public string WorkUnit { get; set; }

        [StringLength(50)]
        public string WorkGroup { get; set; }

        [StringLength(50)]
        public string MachineNo { get; set; }

        public int? BoxNo { get; set; }

        [StringLength(4)]
        public string head { get; set; }

        [StringLength(50)]
        public string StartCode { get; set; }

        [StringLength(50)]
        public string EndCode { get; set; }

        public DateTime? RecordTime { get; set; }

        public int? istate { get; set; }

        [StringLength(50)]
        public string CheckUser { get; set; }

        public DateTime? CheckTime { get; set; }

        public int? isModify { get; set; }

        [StringLength(100)]
        public string note { get; set; }

        public DateTime? reportdate { get; set; }

        [StringLength(20)]
        public string chip { get; set; }

        public int? totalnum { get; set; }

        public short? tickettype { get; set; }

        public short? finishstate { get; set; }

        [StringLength(50)]
        public string GX13Leader { get; set; }
    }
}
