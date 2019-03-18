namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_Product
    {
        public int id { get; set; }

        [StringLength(50)]
        public string ProductType { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string ProductGroup { get; set; }

        [StringLength(50)]
        public string note { get; set; }

        [StringLength(20)]
        public string unite { get; set; }

        [StringLength(50)]
        public string processtype { get; set; }

        [StringLength(50)]
        public string groupcode { get; set; }
    }
}
