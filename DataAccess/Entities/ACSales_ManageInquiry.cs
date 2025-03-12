namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACSales_ManageInquiry
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
        public string Customer_UUID { get; set; }

        [StringLength(50)]
        public string Country_UUID { get; set; }

        [StringLength(50)]
        public string Currency_UUID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Order_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestedDelivery_Date { get; set; }

        [StringLength(25)]
        public string Project_Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Inquiry_No { get; set; }

        [StringLength(50)]
        public string ItemType_UUID { get; set; }

        [StringLength(50)]
        public string ItemName_UUID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Quantity { get; set; }

        [StringLength(50)]
        public string Unit_UUID { get; set; }

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
