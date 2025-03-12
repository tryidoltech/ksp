namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_Employee_DocumentSetup
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
        public string Document_Type_UUID { get; set; }

        [StringLength(50)]
        public string Document_Name_UUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Valid_From { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Valid_To { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Renew_Date { get; set; }

        [StringLength(80)]
        public string Document_File { get; set; }

        [StringLength(50)]
        public string Country_Visa { get; set; }

        [StringLength(50)]
        public string Visa_Type { get; set; }

        [StringLength(50)]
        public string Remark { get; set; }

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

        [StringLength(50)]
        public string Employee_UUID { get; set; }
    }
}
