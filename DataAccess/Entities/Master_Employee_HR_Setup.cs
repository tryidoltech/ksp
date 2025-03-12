namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_Employee_HR_Setup
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

        [StringLength(50)]
        public string Employee_Code { get; set; }

        [StringLength(50)]
        public string Payslip_Category_UUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_Joining { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BP_Code { get; set; }

        public bool? IsEmployee_DoneFromPreviousCompany { get; set; }

        public bool? Is_Employee_Relived { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_of_Reliving { get; set; }

        [StringLength(50)]
        public string Reason_of_Leave { get; set; }

        public bool? IsSubmittedAll_Assests { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Submitted_Date { get; set; }

        [StringLength(50)]
        public string Hr_Designation_UUID { get; set; }

        [StringLength(50)]
        public string PF_No { get; set; }

        [StringLength(50)]
        public string ESIC_No { get; set; }

        [StringLength(50)]
        public string UAN_No { get; set; }

        [StringLength(50)]
        public string Tax_Regime_UUID { get; set; }

        [StringLength(50)]
        public string Employeement_Type_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseLimitDesignation_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseWorkflowDesignation_UUID { get; set; }

        [StringLength(50)]
        public string Reporting_Designation { get; set; }

        [StringLength(50)]
        public string Alternate_Reporting_Designation { get; set; }

        [StringLength(50)]
        public string Menu_Role_UUID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? IsLoginActive { get; set; }

        [StringLength(50)]
        public string Shift_UUID { get; set; }

        public TimeSpan? Shift_Start_Time { get; set; }

        public TimeSpan? Shift_End_Time { get; set; }

        public TimeSpan? Lunch_Start_Time { get; set; }

        public TimeSpan? Lunch_End_Time { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsAddedOn { get; set; }

        [StringLength(50)]
        public string IsAddedBy { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdatedBy { get; set; }

        public DateTime? IsDeletedOn { get; set; }

        [StringLength(50)]
        public string IsDeletedBy { get; set; }

        [StringLength(30)]
        public string AddedIP { get; set; }

        [StringLength(30)]
        public string UpdatedIP { get; set; }

        [StringLength(30)]
        public string DeletedIP { get; set; }

        [StringLength(50)]
        public string Employee_UUID { get; set; }
    }
}
