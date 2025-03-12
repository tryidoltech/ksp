using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class ResearchUploadResearchDatum
{
    public decimal UploadResearchData_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? ResearchChannel_UUID { get; set; }

    public string? ResearchAudience_UUID { get; set; }

    public string? ManageResearchGroup_UUID { get; set; }

    public string? Upload_DataFiles { get; set; }

    public string? Contact_Name { get; set; }

    public string? Company_Name { get; set; }

    public decimal? GST_No { get; set; }

    public string? Website { get; set; }

    public decimal? Email_Id { get; set; }

    public decimal? Alternate_Email { get; set; }

    public decimal? Mobile_No { get; set; }

    public decimal? Alternate_MobileNo { get; set; }

    public string? Address { get; set; }

    public string? Country_UUID { get; set; }

    public string? State_UUID { get; set; }

    public string? City_UUID { get; set; }

    public decimal? PinCode { get; set; }

    public decimal? LandLine_No { get; set; }

    public string? Industry_UUID { get; set; }

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
