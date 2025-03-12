namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ERSetup_CostCity
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CostCityCategory_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string Cost_CityCategory_Name { get; set; }

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
