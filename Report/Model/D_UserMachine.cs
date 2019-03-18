namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_UserMachine
    {
        public int id { get; set; }

        [StringLength(20)]
        public string usercode { get; set; }

        [StringLength(20)]
        public string machineno { get; set; }
    }
}
