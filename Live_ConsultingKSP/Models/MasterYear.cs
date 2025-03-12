using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterYear
{
    public decimal Year_id { get; set; }

    public string? Uuid { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Year_name { get; set; }

    public bool IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? AddedIp { get; set; }

    public string? UpdatedIp { get; set; }

    public string? DeletedIp { get; set; }

    public decimal? RecordNo { get; set; }
}
