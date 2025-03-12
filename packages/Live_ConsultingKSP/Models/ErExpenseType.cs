using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class ErExpenseType
{
    public decimal ExpenseType_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? ExpenseType_Name { get; set; }

    public bool? HasSubExpenseType { get; set; }

    public string? UnitType_UUID { get; set; }

    public string? Expense_Applicable { get; set; }

    public bool? IsBillMandatory { get; set; }

    public bool? IsMultiLocationAllowed { get; set; }

    public bool? AllowsupportDocument { get; set; }

    public bool? IsAvailableForTrip { get; set; }

    public bool? IsAvailableForSole { get; set; }

    public bool? AllowAssets { get; set; }

    public bool? TakeAssets { get; set; }

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
