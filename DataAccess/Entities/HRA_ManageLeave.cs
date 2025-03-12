namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HRA_ManageLeave
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(20)]
        public string Applied_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Applied_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Leave_StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Leave_EndDate { get; set; }

        [StringLength(50)]
        public string LeaveType_UUID { get; set; }

        [StringLength(50)]
        public string Leave_Parameter { get; set; }

        [Column(TypeName = "text")]
        public string Reason { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Contact_Number { get; set; }

        public bool? Status { get; set; }

        [StringLength(30)]
        public string Action_TakenBy { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsAddedOn { get; set; }

        [StringLength(50)]
        public string IsAddedBy { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdateBy { get; set; }

        public DateTime? IsDeleteOn { get; set; }

        [StringLength(50)]
        public string IsDeletedBy { get; set; }

        [StringLength(30)]
        public string AddedIP { get; set; }

        [StringLength(30)]
        public string UpdatedIP { get; set; }

        [StringLength(30)]
        public string DeletedIP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecordNo { get; set; }
    }
}
