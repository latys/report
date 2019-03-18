namespace Report.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
        }

        public virtual DbSet<D_StatData> D_StatData { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<D_BoxCollectResult> D_BoxCollectResult { get; set; }
        public virtual DbSet<D_ErrorCode> D_ErrorCode { get; set; }
        public virtual DbSet<D_QuickMessage> D_QuickMessage { get; set; }
        public virtual DbSet<D_UserInfo> D_UserInfo { get; set; }
        public virtual DbSet<D_UserMachine> D_UserMachine { get; set; }
        public virtual DbSet<D_VerWasteCode> D_VerWasteCode { get; set; }
        public virtual DbSet<D_WorkGroupTip> D_WorkGroupTip { get; set; }
        public virtual DbSet<DIC_ChipType> DIC_ChipType { get; set; }
        public virtual DbSet<DIC_ErrorType> DIC_ErrorType { get; set; }
        public virtual DbSet<DIC_Machines> DIC_Machines { get; set; }
        public virtual DbSet<DIC_ProcessType> DIC_ProcessType { get; set; }
        public virtual DbSet<DIC_Product> DIC_Product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<D_StatData>()
                .Property(e => e.processtype)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.reportdate)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.producttype)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.head)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.workgroup)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.workunit)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.machineno)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.startcode)
                .IsUnicode(false);

            modelBuilder.Entity<D_StatData>()
                .Property(e => e.endcode)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.TaskCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.CarCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.ProcessType)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.ProductType)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.WorkUnit)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.WorkGroup)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.MachineNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.head)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.StartCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.EndCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.CheckUser)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.chip)
                .IsUnicode(false);

            modelBuilder.Entity<D_BoxCollectResult>()
                .Property(e => e.GX13Leader)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.TaskCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.MachineNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.BoxNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.SubBoxNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.ErrCode)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.processType)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.productType)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.workUnit)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.workGroup)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.errtype)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.checkerName)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.checkerWorkUnit)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.wasteCheckerName)
                .IsUnicode(false);

            modelBuilder.Entity<D_ErrorCode>()
                .Property(e => e.wasteCheckerWorkUnit)
                .IsUnicode(false);

            modelBuilder.Entity<D_QuickMessage>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.loginname)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.pwd)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.cardcode)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.processtype)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserInfo>()
                .Property(e => e.team)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserMachine>()
                .Property(e => e.usercode)
                .IsUnicode(false);

            modelBuilder.Entity<D_UserMachine>()
                .Property(e => e.machineno)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.MachineNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.CheckerNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.errType)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.ProcessType)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.workUnit)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<D_VerWasteCode>()
                .Property(e => e.codeRange)
                .IsUnicode(false);

            modelBuilder.Entity<D_WorkGroupTip>()
                .Property(e => e.MachineNo)
                .IsUnicode(false);

            modelBuilder.Entity<D_WorkGroupTip>()
                .Property(e => e.workunit)
                .IsUnicode(false);

            modelBuilder.Entity<D_WorkGroupTip>()
                .Property(e => e.workgroup)
                .IsUnicode(false);

            modelBuilder.Entity<D_WorkGroupTip>()
                .Property(e => e.tip)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ChipType>()
                .Property(e => e.chiptype)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ChipType>()
                .Property(e => e.chipname)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ErrorType>()
                .Property(e => e.errcode)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ErrorType>()
                .Property(e => e.errname)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ErrorType>()
                .Property(e => e.processtype)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Machines>()
                .Property(e => e.machineno)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Machines>()
                .Property(e => e.machinename)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Machines>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Machines>()
                .Property(e => e.processtype)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Machines>()
                .Property(e => e.manager)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ProcessType>()
                .Property(e => e.ProcessType)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ProcessType>()
                .Property(e => e.ProcessName)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ProcessType>()
                .Property(e => e.Metarial)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ProcessType>()
                .Property(e => e.Unite)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_ProcessType>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.ProductType)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.ProductGroup)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.unite)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.processtype)
                .IsUnicode(false);

            modelBuilder.Entity<DIC_Product>()
                .Property(e => e.groupcode)
                .IsUnicode(false);
        }
    }
}
