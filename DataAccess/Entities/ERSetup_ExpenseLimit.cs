namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ERSetup_ExpenseLimit
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ExpenseLimit_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseType_UUID { get; set; }

        [StringLength(50)]
        public string ExpenseSubType_UUID { get; set; }

        [StringLength(50)]
        public string Expense_Allocation_Type { get; set; }

        [StringLength(50)]
        public string Expense_Applicable_Type { get; set; }

        [StringLength(50)]
        public string Count_On { get; set; }

        [StringLength(50)]
        public string CostCategory_UUID { get; set; }

        [StringLength(50)]
        public string CostCityCategory_UUID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Limit_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Max_Amount { get; set; }

        public bool? IsRemakable_Mandatory { get; set; }

        public bool? IsMaxLimit { get; set; }

        public bool? IsDisplay { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? RecordNo { get; set; }
    }
}
