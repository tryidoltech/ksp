namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AC_CustomerMaster
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CustomerMaster_Id { get; set; }

        [StringLength(50)]
        public string UUID { get; set; }

        [StringLength(50)]
        public string Master_Company_UUID { get; set; }

        [StringLength(50)]
        public string Master_Environment_UUID { get; set; }

        [StringLength(50)]
        public string CustomerMaster_Name { get; set; }

        [StringLength(50)]
        public string CustomerEmail_Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Mobile_No { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Landline_No { get; set; }

        [StringLength(50)]
        public string Company_Name { get; set; }

        [StringLength(50)]
        public string Company_Registration_No { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tax_No { get; set; }

        [StringLength(50)]
        public string Currency_UUID { get; set; }

        [StringLength(50)]
        public string Billing_Building_No { get; set; }

        [StringLength(50)]
        public string Billing_Street_1 { get; set; }

        [StringLength(50)]
        public string Billing_Street_2 { get; set; }

        [StringLength(50)]
        public string Billing_Postal_Code { get; set; }

        [StringLength(50)]
        public string Billing_Country_UUID { get; set; }

        [StringLength(50)]
        public string Billing_State_UUID { get; set; }

        [StringLength(50)]
        public string Billing_City_UUID { get; set; }

        public bool? IsShippingAddressSame { get; set; }

        [StringLength(50)]
        public string Shipping_Building_No { get; set; }

        [StringLength(50)]
        public string Shipping_Street_1 { get; set; }

        [StringLength(50)]
        public string Shipping_Street_2 { get; set; }

        [StringLength(50)]
        public string Shipping_Postal_Code { get; set; }

        [StringLength(50)]
        public string Shipping_Country_UUID { get; set; }

        [StringLength(50)]
        public string Shipping_State_UUID { get; set; }

        [StringLength(50)]
        public string Shipping_City_UUID { get; set; }

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
