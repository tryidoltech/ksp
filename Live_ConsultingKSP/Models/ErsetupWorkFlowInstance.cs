using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class ErsetupWorkFlowInstance
{
    public decimal WorkFlowInstance_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? ExpenseType_UUID { get; set; }

    public string? ExpenseSubType_UUID { get; set; }

    public string? WorkFlow_InstanceType { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Instance_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? OriginatorDesignation_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? ReportDesignation_UUID { get; set; }

    public string? ReportingSubstitute_Designation { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

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
