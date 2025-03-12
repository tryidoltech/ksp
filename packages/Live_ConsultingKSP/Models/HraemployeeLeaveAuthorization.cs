using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class HraemployeeLeaveAuthorization
{
    public decimal LeaveAuthorization_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Company_UUID { get; set; }

    public string? MasterCompanyBranch_UUID { get; set; }

    public string? Employee_UUID { get; set; }

    public string? LeaveType_UUID { get; set; }

    public string? NumberOfAllow_leave { get; set; }

    public DateTime? start_Date { get; set; }

    public DateTime? End_Date { get; set; }

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
