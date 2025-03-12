namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Timesheet_Header
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [StringLength(40)]
        public string UUID { get; set; }

        [StringLength(40)]
        public string Emp_UUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? From_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }

        [StringLength(20)]
        public string Total_Hours { get; set; }

        [StringLength(50)]
        public string Timesheet_Status { get; set; }

        [Column(TypeName = "text")]
        public string Restriction_Reason { get; set; }

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
