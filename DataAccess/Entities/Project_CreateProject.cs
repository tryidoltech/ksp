namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_CreateProject
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

        [StringLength(150)]
        public string Project_Title { get; set; }

        [StringLength(50)]
        public string Customer_UUID { get; set; }

        [StringLength(50)]
        public string Employee_UUID { get; set; }

        [Column(TypeName = "text")]
        public string Project_Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Start_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? End_Date { get; set; }

        [StringLength(50)]
        public string Expected_Total_Hours { get; set; }

        [StringLength(50)]
        public string Project_Cost { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDisplay { get; set; }

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

        [Column(TypeName = "date")]
        public DateTime? Actual_Start_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Actual_End_Date { get; set; }

        [StringLength(50)]
        public string Actual_Total_Hours { get; set; }

        [StringLength(50)]
        public string Project_Status_UUID { get; set; }
    }
}
