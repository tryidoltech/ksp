using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class ResearchManageResearchGroup
{
    public decimal ManageResearchGroup_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? ResearchChannel_UUID { get; set; }

    public string? ResearchAudience_UUID { get; set; }

    public string? ResearchGroup_Name { get; set; }

    public string? Designation_UUID { get; set; }

    public string? DataMining_TeamMember { get; set; }

    public decimal? ResearchData_Count { get; set; }

    public string? FilterationDesignation_UUID { get; set; }

    public string? DataFilteration_Teammember { get; set; }

    public decimal? Filteration_Datacount { get; set; }

    public string? BDDesignation_UUID { get; set; }

    public string? BD_TeamMember { get; set; }

    public string? Country_UUID { get; set; }

    public string? State_UUID { get; set; }

    public string? City_UUID { get; set; }

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
