namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_ErrorCode
    {
        public int id { get; set; }

        [StringLength(50)]
        public string TaskCode { get; set; }

        [StringLength(50)]
        public string MachineNo { get; set; }

        [StringLength(10)]
        public string BoxNo { get; set; }

        [StringLength(10)]
        public string SubBoxNo { get; set; }

        [StringLength(20)]
        public string ErrCode { get; set; }

        public DateTime? RecordTime { get; set; }

        public int? istate { get; set; }

        [StringLength(50)]
        public string note { get; set; }

        [StringLength(50)]
        public string processType { get; set; }

        [StringLength(50)]
        public string productType { get; set; }

        [StringLength(50)]
        public string workUnit { get; set; }

        [StringLength(50)]
        public string workGroup { get; set; }

        [StringLength(20)]
        public string errtype { get; set; }

        public DateTime? checkTime { get; set; }

        [StringLength(50)]
        public string checkerName { get; set; }

        [StringLength(50)]
        public string checkerWorkUnit { get; set; }

        public int? IsAppendCode { get; set; }

        [StringLength(50)]
        public string wasteCheckerName { get; set; }

        [StringLength(50)]
        public string wasteCheckerWorkUnit { get; set; }

        public DateTime? wasteCheckTime { get; set; }

        public DateTime? bumaUserName { get; set; }
    }
}
