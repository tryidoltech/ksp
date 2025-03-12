namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Research_UploadResearchData
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal UploadResearchData_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string ResearchChannel_UUID { get; set; }

        [StringLength(50)]
        public string ResearchAudience_UUID { get; set; }

        [StringLength(50)]
        public string ManageResearchGroup_UUID { get; set; }

        [StringLength(200)]
        public string Upload_DataFiles { get; set; }

        [StringLength(50)]
        public string Contact_Name { get; set; }

        [StringLength(50)]
        public string Company_Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GST_No { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Email_Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Alternate_Email { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Mobile_No { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Alternate_MobileNo { get; set; }

        [Column(TypeName = "text")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Country_UUID { get; set; }

        [StringLength(50)]
        public string State_UUID { get; set; }

        [StringLength(50)]
        public string City_UUID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PinCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LandLine_No { get; set; }

        [StringLength(50)]
        public string Industry_UUID { get; set; }

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
