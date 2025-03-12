namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoleExpenceDetailMaster")]
    public partial class SoleExpenceDetailMaster
    {
        [Key]
        public long SoleExpenceDetailId { get; set; }

        [StringLength(1)]
        public string UUID { get; set; }

        [StringLength(1)]
        public string SoleExpence_UUID { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public int? CityId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quantity { get; set; }

        public long? UnitTypeId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UnitCost { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalCost { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaxAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BillAmount { get; set; }

        public long? Unit { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string CustomerName { get; set; }

        public string UploadBillImage { get; set; }

        public string Remarks { get; set; }

        public string AddedIp { get; set; }

        public string UpdatedIp { get; set; }

        public string DeletedIp { get; set; }

        public string CreatedBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? IsAddedOn { get; set; }

        public DateTime? IsUpdatedOn { get; set; }
    }
}
