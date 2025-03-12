namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_ProjectMeeting
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
        public string Company_Name { get; set; }

        [StringLength(50)]
        public string Contact_Person_Name { get; set; }

        [StringLength(50)]
        public string IndustrySector_UUID { get; set; }

        public DateTime? Meeting_Date { get; set; }

        public TimeSpan? Meeting_Time { get; set; }

        [StringLength(80)]
        public string Meeting_Document { get; set; }

        [StringLength(256)]
        public string Attendees_UUID { get; set; }

        [StringLength(256)]
        public string Attendees_Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

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
