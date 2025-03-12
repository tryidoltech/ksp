using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterEmployee
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Master_Prefix_UUID { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? ProfilePic { get; set; }

    public string? Master_BloodGroup_UUID { get; set; }

    public string? Master_Department_UUID { get; set; }

    public string? Master_Gender_UUID { get; set; }

    public string? EmployeeCode { get; set; }

    public string? ExpLimitDesignation { get; set; }

    public string? ExpWorkflowDesignation { get; set; }

    public string? ReportingDesignation { get; set; }

    public string? Master_Roles_UUID { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? IsLoginActive { get; set; }

    public string? PersonalEmail { get; set; }

    public string? Mobile { get; set; }

    public string? Landline { get; set; }

    public string? AltMobile { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyContactNo { get; set; }

    public string? BuildingNo { get; set; }

    public string? StreetAddress { get; set; }

    public string? StreetAddress2 { get; set; }

    public string? PostCode { get; set; }

    public string? Master_City_UUID { get; set; }

    public string? Master_City_Name { get; set; }

    public string? Master_State_UUID { get; set; }

    public string? Master_State_Name { get; set; }

    public string? Master_Country_UUID { get; set; }

    public string? Master_Country_Name { get; set; }

    public string? CurrentBuildingNo { get; set; }

    public string? CurrentStreetAddress { get; set; }

    public string? CurrentStreetAddress2 { get; set; }

    public string? CurrentPostCode { get; set; }

    public string? CurrentMaster_City_UUID { get; set; }

    public string? CurrentMaster_City_Name { get; set; }

    public string? CurrentMaster_State_UUID { get; set; }

    public string? CurrentMaster_State_Name { get; set; }

    public string? CurrentMaster_Country_UUID { get; set; }

    public string? CurrentMaster_Country_Name { get; set; }

    public string? CurrentLandmark { get; set; }

    public string? Landmark { get; set; }

    public DateOnly? BirthDate { get; set; }

    public bool? IsLibraryAllowed { get; set; }

    public bool? IsTreeViewSearchAllowed { get; set; }

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
}
