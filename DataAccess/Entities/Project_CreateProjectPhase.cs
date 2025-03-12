namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_CreateProjectPhase
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
        public string Project_UUID { get; set; }

        [StringLength(150)]
        public string Phase_Name { get; set; }

        [Column(TypeName = "text")]
        public string Project_Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Phase_Cost { get; set; }

        [StringLength(50)]
        public string Employee_UUID { get; set; }

        [StringLength(50)]
        public string PhaseProgress { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Start_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? End_Date { get; set; }

        [StringLength(50)]
        public string Phase_Expected_Hours { get; set; }

        [StringLength(256)]
        public string Select_Team_Member_UUID { get; set; }

        [StringLength(256)]
        public string Select_Team_Member_Name { get; set; }

        public bool? Is_Project_Phase_Linked_with_Invoice { get; set; }

        [StringLength(50)]
        public string Invoice_UUID { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

        public bool? IsDisplay { get; set; }

        public bool IsActive { get; set; }

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
