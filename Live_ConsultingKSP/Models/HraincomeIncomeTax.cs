using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class HraincomeIncomeTax
{
    public decimal IncomeMaster_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? TaxRegime_UUID { get; set; }

    public string? Gender_UUID { get; set; }

    public string? IncomeTax_Name { get; set; }

    public string? YearlyIncome_From { get; set; }

    public string? YearlyIncome_To { get; set; }

    public string? Percentage { get; set; }

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
