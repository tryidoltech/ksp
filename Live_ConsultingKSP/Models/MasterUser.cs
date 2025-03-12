using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterUser
{
    public decimal UserId { get; set; }

    public Guid? Uuid { get; set; }

    public decimal? CompanyUuid { get; set; }

    public decimal? EnvironmentUuid { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleUuid { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAdddedOn { get; set; }
}
