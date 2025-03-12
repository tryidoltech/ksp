using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class HraemployeeSalarySalaryParameter
{
    public decimal Parameter_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Company_UUID { get; set; }

    public string? MasterCompanyBranch_UUID { get; set; }

    public string? PayslipCategory_UUID { get; set; }

    public string? Employee_UUID { get; set; }

    public string? Month { get; set; }

    public string? Advance { get; set; }

    public string? Incentive { get; set; }

    public string? Loan { get; set; }

    public string? Misc_Recovery { get; set; }

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
