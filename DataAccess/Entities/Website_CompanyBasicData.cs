namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Website_CompanyBasicData
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
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string EmailId { get; set; }

        [StringLength(100)]
        public string CompanyAddress { get; set; }

        [StringLength(50)]
        public string FaceBookURL { get; set; }

        [StringLength(50)]
        public string InstagramURL { get; set; }

        [StringLength(50)]
        public string TwitterURL { get; set; }

        [StringLength(50)]
        public string linkedinURL { get; set; }

        [StringLength(50)]
        public string WorkingDays { get; set; }

        [StringLength(50)]
        public string StartTime { get; set; }

        [StringLength(50)]
        public string EndTime { get; set; }

        [StringLength(10)]
        public string Logo { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdateBy { get; set; }

        [StringLength(30)]
        public string UpdatedIP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecordNo { get; set; }
    }
}
