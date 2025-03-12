using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class ErsetupExpenseEligibility
{
    public decimal ExpenseEligibilityId { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Expense_Availablity { get; set; }

    public string? ExpenseType_UUID { get; set; }

    public string? ExpenseSubType_UUID { get; set; }

    public string? Employee_UUID { get; set; }

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
