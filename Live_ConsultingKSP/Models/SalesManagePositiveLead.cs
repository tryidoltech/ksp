using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class SalesManagePositiveLead
{
    public decimal ManagePositiveLead_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? CommunicationHistory_UUID { get; set; }

    public string? ReasearchCommunicationStatus_UUID { get; set; }

    public string? Remark { get; set; }

    public string? Upload_Files { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

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
