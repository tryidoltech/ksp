namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoleExpenceMaster")]
    public partial class SoleExpenceMaster
    {
        [Key]
        public long SoleExpenceId { get; set; }

        [StringLength(1)]
        public string UUID { get; set; }

        public string SoleExpenceCode { get; set; }

        public int? ProjectId { get; set; }

        public int? ProjectTaskId { get; set; }

        public DateTime? DocumentFormDate { get; set; }

        public DateTime? DocumentToDate { get; set; }

        public long? ExpenseTypeId { get; set; }

        public long? ExpenseSubTypeId { get; set; }

        public string ExpenseGrossTotal { get; set; }

        public long? EmployeeId { get; set; }

        public int? MainApproval { get; set; }

        public int? NextApprovalDesignation { get; set; }

        public int? ProfitCenterId { get; set; }

        public int? GeneralLedgerId { get; set; }

        public string Remarks { get; set; }

        public string AddedIp { get; set; }

        public string UpdatedIp { get; set; }

        public string DeletedIp { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? IsAddedOn { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public bool? Isforeignexp { get; set; }

        public bool? Isapplyforapproval { get; set; }
    }
}
