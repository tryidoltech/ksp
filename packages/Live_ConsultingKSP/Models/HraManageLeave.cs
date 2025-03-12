using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class HraManageLeave
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Applied_By { get; set; }

    public DateOnly? Applied_Date { get; set; }

    public DateOnly? Leave_StartDate { get; set; }

    public DateOnly? Leave_EndDate { get; set; }

    public string? LeaveType_UUID { get; set; }

    public string? Reason { get; set; }

    public decimal? Contact_Number { get; set; }

    public string? Status { get; set; }

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
