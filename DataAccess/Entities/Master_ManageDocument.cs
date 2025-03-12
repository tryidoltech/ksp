namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_ManageDocument
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
        public string LibraryCategory_UUID { get; set; }

        [StringLength(50)]
        public string LibraryCategoryTag_UUID { get; set; }

        [StringLength(50)]
        public string LibraryDocumentCategory_UUID { get; set; }

        [StringLength(50)]
        public string LibraryDocumentCategoryTag_UUID { get; set; }

        [StringLength(50)]
        public string Year_UUID { get; set; }

        [StringLength(20)]
        public string Document_Titile { get; set; }

        [StringLength(50)]
        public string Document_Upload { get; set; }

        public bool? IsRenewable { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Next_RenewableDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RemindMeBefore_Days { get; set; }

        [StringLength(50)]
        public string parent_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NextReminder_Date { get; set; }

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
