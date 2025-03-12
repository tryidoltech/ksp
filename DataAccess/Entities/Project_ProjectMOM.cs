namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project_ProjectMOM
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
        public string Company { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Meeting_Date { get; set; }

        public TimeSpan? Meeting_Time { get; set; }

        [StringLength(50)]
        public string Meeting_Type { get; set; }

        [StringLength(50)]
        public string Meeting_Attendees_from_Client_Side { get; set; }

        [StringLength(256)]
        public string Attendees_from_Our_Company_UUID { get; set; }

        [StringLength(256)]
        public string Attendees_from_Our_Company_Name { get; set; }

        [Column(TypeName = "text")]
        public string Meeting_Ajenda { get; set; }

        [StringLength(80)]
        public string Meeting_Document { get; set; }

        public bool Is_Any_Further_Meeting_Schedule { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Next_Meeting_Date { get; set; }

        public TimeSpan? Next_Meeting_Time { get; set; }

        [StringLength(50)]
        public string Next_Meeting_Type { get; set; }

        public bool IsDisplay { get; set; }

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
