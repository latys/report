namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_ErrorType
    {
        public int id { get; set; }

        [StringLength(20)]
        public string errcode { get; set; }

        [StringLength(20)]
        public string errname { get; set; }

        [StringLength(50)]
        public string processtype { get; set; }
    }
}
