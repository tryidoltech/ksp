using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterUserMenuRight
{
    public decimal UserMenuRightsId { get; set; }

    public string? Uuid { get; set; }

    public string? CompanyUuid { get; set; }

    public string? EnvironmentUuid { get; set; }

    public string? MenuUuid { get; set; }

    public bool? IsRead { get; set; }

    public bool? IsWrite { get; set; }

    public bool? IsEdit { get; set; }

    public bool? IsDelete { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

    public DateTime? IsAdddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdatedBy { get; set; }

    public DateTime? IsDeletedOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? UserRoleUuid { get; set; }
    public string? AddedIp { get; set; }

    public string? UpdatedIp { get; set; }

    public string? DeletedIp { get; set; }

    public decimal? RecordNo { get; set; }
}