namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_UserInfo
    {
        public int id { get; set; }

        [StringLength(50)]
        public string loginname { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string pwd { get; set; }

        public int? roleid { get; set; }

        [StringLength(20)]
        public string cardcode { get; set; }

        [StringLength(50)]
        public string processtype { get; set; }

        [StringLength(50)]
        public string team { get; set; }
    }
}
