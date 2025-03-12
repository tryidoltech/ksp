namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_Company
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CompanyId { get; set; }

        [StringLength(255)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string Company_ShortName { get; set; }

        [StringLength(50)]
        public string GSTIN_Number { get; set; }

        [StringLength(50)]
        public string Uuid { get; set; }

        [StringLength(50)]
        public string ContactPersonName_Sales { get; set; }

        [StringLength(50)]
        public string ContactPersonName_Support { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOf_Establishment { get; set; }

        [StringLength(50)]
        public string Email_Id_Sales { get; set; }

        [StringLength(50)]
        public string Email_Id_Support { get; set; }

        [StringLength(50)]
        public string Email_Id_Personal { get; set; }

        [StringLength(50)]
        public string CountryName_UUID { get; set; }

        [StringLength(50)]
        public string StateName_UUID { get; set; }

        [StringLength(50)]
        public string CityName_UUID { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string Phone_Number { get; set; }

        [StringLength(50)]
        public string Alternate_PhoneNumber { get; set; }

        [StringLength(50)]
        public string Mobile_Number { get; set; }

        [StringLength(50)]
        public string Alternate_Mobile_Number { get; set; }

        [StringLength(70)]
        public string URL1 { get; set; }

        [StringLength(70)]
        public string URL2 { get; set; }

        [StringLength(80)]
        public string Logo { get; set; }

        [StringLength(80)]
        public string Signature { get; set; }

        [StringLength(80)]
        public string Stamp { get; set; }

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

        [StringLength(30)]
        public string AddedIP { get; set; }

        [StringLength(30)]
        public string UpdatedIP { get; set; }

        [StringLength(30)]
        public string DeletedIP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecordNo { get; set; }

        [StringLength(50)]
        public string Bank_Name_UUID { get; set; }

        [StringLength(50)]
        public string Bank_AccountType_UUID { get; set; }

        [StringLength(50)]
        public string Beneficary_Name { get; set; }

        [StringLength(50)]
        public string Beneficary_Bank_Name { get; set; }

        [StringLength(50)]
        public string Bank_AccountNumber { get; set; }

        [StringLength(50)]
        public string IFSC_Code { get; set; }

        [StringLength(50)]
        public string Swift_Code { get; set; }
    }
}
