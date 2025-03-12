using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterEmployee
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Master_Prefix_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? MiddleName { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? LastName { get; set; }

    public string? ProfilePic { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Master_BloodGroup_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Master_Department_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Master_Gender_UUID { get; set; }


    public string? Master_Roles_UUID { get; set; }
    public string? Allowed_Company_UUID { get; set; }
    public string? Allowed_Company { get; set; }
    public string? Allowed_Environment_UUID { get; set; }
    public string? Allowed_Environment { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool IsLoginActive { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? PersonalEmail { get; set; }

    [Required(ErrorMessage = "Required!")]

    
    public string? Mobile { get; set; }


    public string? Landline { get; set; }


    public string? AltMobile { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? CompanyEmail { get; set; }


    public string? CompanyContactNo { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? BuildingNo { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? StreetAddress { get; set; }

    public string? StreetAddress2 { get; set; }

    public string? PostCode { get; set; }
    public bool IsCurrentAddressSame { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Master_City_UUID { get; set; }

    public string? Master_City_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Master_State_UUID { get; set; }

    public string? Master_State_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Master_Country_UUID { get; set; }

    public string? Master_Country_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentBuildingNo { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentStreetAddress { get; set; }

    public string? CurrentStreetAddress2 { get; set; }

    public string? CurrentPostCode { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentMaster_City_UUID { get; set; }

    public string? CurrentMaster_City_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentMaster_State_UUID { get; set; }

    public string? CurrentMaster_State_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentMaster_Country_UUID { get; set; }

    public string? CurrentMaster_Country_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CurrentLandmark { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Landmark { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? BirthDate { get; set; }
    public string? Father_Name { get; set; }
    public string? Father_Occupation { get; set; }
 
    public string? Father_MobileNo { get; set; }
    public string? Mother_Name { get; set; }
    public string? Mother_Occupation { get; set; }

    public string? Mother_MobileNo { get; set; }
    public string? Sibling1_Name { get; set; }
    public string? Sibling1_Occupation { get; set; }
  
    public string? Sibling1_MobileNo { get; set; }
    public string? Sibling2_Name { get; set; }
    public string? Sibling2_Occupation { get; set; }
    public string? Employee_Code { get; set; }
    public string? Payslip_Category_UUID { get; set; }
    public DateTime? Date_of_Joining { get; set; }
    public decimal? BP_Code { get; set; }
    public bool IsEmployee_DoneFromPreviousCompany { get; set; }
    public bool Is_Employee_Relived { get; set; }
    public DateTime? Date_of_Reliving { get; set; }
    public string? Reason_of_Leave { get; set; }
    public bool IsSubmittedAll_Assests { get; set; }
    public DateTime? Submitted_Date { get; set; }
    public string? Hr_Designation_UUID { get; set; }
    public string? PF_No { get; set; }
    public string? ESIC_No { get; set; }
    public string? UAN_No { get; set; }
    public string? Tax_Regime_UUID { get; set; }
    public string? Employeement_Type_UUID { get; set; }
    public string? ExpenseLimitDesignation_UUID { get; set; }
    public string? ExpenseWorkflowDesignation_UUID { get; set; }
    public string? Reporting_Designation { get; set; }
    public string? Alternate_Reporting_Designation { get; set; }
    public string? Menu_Role_UUID { get; set; }
    public string? Shift_UUID { get; set; }
    public TimeSpan? Shift_Start_Time { get; set; }
    public TimeSpan? Shift_End_Time { get; set; }
    public TimeSpan? Lunch_Start_Time { get; set; }
    public TimeSpan? Lunch_End_Time { get; set; }
    public string? Sibling2_MobileNo { get; set; }
    public bool IsLibraryAllowed { get; set; }

    public bool IsTreeViewSearchAllowed { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

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