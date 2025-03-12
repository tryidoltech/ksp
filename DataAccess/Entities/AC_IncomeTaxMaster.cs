namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AC_IncomeTaxMaster
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IncomeTax_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string Regime_UUID { get; set; }

        [StringLength(50)]
        public string Gender_UUID { get; set; }

        [StringLength(50)]
        public string IncomeTax_Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Yearly_Income_From { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Yearly_Income_To { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Percentage { get; set; }

        public bool? IsDisplay { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? IsAddedOn { get; set; }

        [StringLength(50)]
        public string IsAddedBy { get; set; }

        public DateTime? IsUpdatedOn { get; set; }

        [StringLength(50)]
        public string IsUpdateBy { get; set; }

        public DateTime? IsDeleteOn { get; set; }

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
