using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class HraManageLeave
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Applied_By { get; set; }

    public DateTime? Applied_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Leave_StartDate { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Leave_EndDate { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? LeaveType_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Leave_Parameter { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Reason { get; set; }
    [Required(ErrorMessage = "Required!")]
    public decimal? Contact_Number { get; set; }

    public bool? Status { get; set; }

    public string? Action_TakenBy { get; set; }

    public string? Remark { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? AddedIP { get; set; }

    public string? UpdatedIP { get; set; }

    public string? DeletedIP { get; set; }

    public decimal? RecordNo { get; set; }
}
