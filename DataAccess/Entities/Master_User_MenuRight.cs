namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_User_MenuRight
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal UserMenuRightsId { get; set; }

        [StringLength(50)]
        public string Uuid { get; set; }

        [StringLength(50)]
        public string CompanyUuid { get; set; }

        [StringLength(50)]
        public string EnvironmentUuid { get; set; }

        [StringLength(50)]
        public string MenuUuid { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsWrite { get; set; }

        public bool? IsEdit { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsAdddedOn { get; set; }

        [StringLength(50)]
        public string IsAddedBy { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdatedBy { get; set; }

        public DateTime? IsDeletedOn { get; set; }

        [StringLength(50)]
        public string IsDeletedBy { get; set; }

        [StringLength(50)]
        public string UserRoleUuid { get; set; }

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
