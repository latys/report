namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_ChipType
    {
        public int id { get; set; }

        [StringLength(20)]
        public string chiptype { get; set; }

        [StringLength(50)]
        public string chipname { get; set; }
    }
}
