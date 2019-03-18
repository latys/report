namespace Report.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_VerWasteCode
    {
        public int id { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(10)]
        public string MachineNo { get; set; }

        public DateTime? ProductionDate { get; set; }

        [StringLength(50)]
        public string CheckerNo { get; set; }

        public DateTime? CheckDate { get; set; }

        [StringLength(50)]
        public string errType { get; set; }

        [StringLength(20)]
        public string ProcessType { get; set; }

        [StringLength(20)]
        public string workUnit { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        [StringLength(50)]
        public string codeRange { get; set; }

        public override string ToString()
        {
            return "code:"+code+" MachineNO:"+MachineNo+" productionDate:"+ProductionDate.ToString()+" errType:"+errType+" processType:"+ProcessType+" workUnit:"+workUnit+" Note:"+Note+" Coderange:"+codeRange;
        }
    }
}
