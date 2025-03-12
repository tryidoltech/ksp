namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HRAEmployeeSalary_SalaryParameter
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Parameter_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string Company_UUID { get; set; }

        [StringLength(50)]
        public string MasterCompanyBranch_UUID { get; set; }

        [StringLength(50)]
        public string PayslipCategory_UUID { get; set; }

        [StringLength(50)]
        public string Employee_UUID { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Advance { get; set; }

        [StringLength(50)]
        public string Incentive { get; set; }

        [StringLength(50)]
        public string Loan { get; set; }

        [StringLength(50)]
        public string Misc_Recovery { get; set; }

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
