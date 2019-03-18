namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_QuickMessage
    {
        public int id { get; set; }

        [StringLength(100)]
        public string message { get; set; }
    }
}
