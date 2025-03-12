using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterMenu
{
    public decimal MenuId { get; set; }

    public string? Uuid { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? MenuName { get; set; }

    public string? MenuIcon { get; set; }
    [Required(ErrorMessage = "Required!")]
    public decimal? MenuLevel { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? MainParentUUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? SubParentUUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public decimal? Sequence { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Url { get; set; }

    public bool IsParent { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

    public DateTime? IsAdddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdatedBy { get; set; }

    public DateTime? IsDeletedOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? AddedIP { get; set; }

    public string? UpdatedIP { get; set; }

    public string? DeletedIP { get; set; }

    public decimal? RecordNo { get; set; }
}