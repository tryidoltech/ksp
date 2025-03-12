namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Research_ManageResearchGroup
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ManageResearchGroup_Id { get; set; }

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
        public string ResearchGroup_Name { get; set; }

        [StringLength(50)]
        public string Designation_UUID { get; set; }

        public string DataMining_TeamMember { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ResearchData_Count { get; set; }

        [StringLength(50)]
        public string FilterationDesignation_UUID { get; set; }

        public string DataFilteration_Teammember { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Filteration_Datacount { get; set; }

        [StringLength(50)]
        public string BDDesignation_UUID { get; set; }

        public string BD_TeamMember { get; set; }

        [StringLength(50)]
        public string Country_UUID { get; set; }

        [StringLength(50)]
        public string State_UUID { get; set; }

        [StringLength(50)]
        public string City_UUID { get; set; }

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
