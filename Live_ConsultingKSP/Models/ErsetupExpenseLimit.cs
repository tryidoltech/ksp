using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class ErsetupExpenseLimit
{
    public decimal ExpenseLimit_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? ExpenseType_UUID { get; set; }

    public string? ExpenseSubType_UUID { get; set; }

    public string? Expense_Allocation_Type { get; set; }

    public string? Expense_Applicable_Type { get; set; }

    public string? Count_On { get; set; }

    public string? CostCategory_UUID { get; set; }

    public string? CostCityCategory_UUID { get; set; }

    public decimal? Limit_Amount { get; set; }

    public decimal? Max_Amount { get; set; }

    public bool? IsRemakable_Mandatory { get; set; }

    public bool? IsMaxLimit { get; set; }

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
