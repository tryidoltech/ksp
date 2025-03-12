namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_AuthAC
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [StringLength(40)]
        public string Uuid { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(20)]
        public string password { get; set; }

        [Column(TypeName = "text")]
        public string Token { get; set; }

        public DateTime? Tkn_Expiry_Start { get; set; }

        public DateTime? Tkn_Expiry_End { get; set; }

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
    }
}
