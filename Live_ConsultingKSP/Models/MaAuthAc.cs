using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MaAuthAc
{
    public decimal Id { get; set; }

    public string? Uuid { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }

    public DateTime? TknExpiryStart { get; set; }

    public DateTime? TknExpiryEnd { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public string? IsDeletedBy { get; set; }
}
