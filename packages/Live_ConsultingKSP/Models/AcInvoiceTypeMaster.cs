using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class AcInvoiceTypeMaster
{
    public decimal InvoiceTypeId { get; set; }

    public string? Uuid { get; set; }

    public string? MasterCompanyUuid { get; set; }

    public string? MasterEnvironmentUuid { get; set; }

    public string? InvoiceType { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? AddedIp { get; set; }

    public string? UpdatedIp { get; set; }

    public string? DeletedIp { get; set; }

    public decimal? RecordNo { get; set; }
}
