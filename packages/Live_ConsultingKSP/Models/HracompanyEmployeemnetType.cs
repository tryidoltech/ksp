using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class HracompanyEmployeemnetType
{
    public decimal EmployeemnetTypeId { get; set; }

    public string Uuid { get; set; } = null!;

    public string? MasterCompanyUuid { get; set; }

    public string? MasterEnvironmentUuid { get; set; }

    public string? CompanyUuid { get; set; }

    public string EmployeemnetTypeName { get; set; } = null!;

    public bool IsDuration { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public decimal? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public decimal? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public decimal? IsDeletedBy { get; set; }

    public string? AddedIp { get; set; }

    public string? UpdatedIp { get; set; }

    public string? DeletedIp { get; set; }

    public decimal? RecordNo { get; set; }
}
