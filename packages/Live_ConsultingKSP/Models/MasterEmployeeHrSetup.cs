using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterEmployeeHrSetup
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Payslip_Category_UUID { get; set; }

    public DateOnly? Date_of_Joining { get; set; }

    public decimal? BP_Code { get; set; }

    public bool? Is_Employee_Relived { get; set; }

    public DateOnly? Date_of_Reliving { get; set; }

    public string? Reason_of_Leave { get; set; }

    public string? Hr_Designation_UUID { get; set; }

    public string? PF_No { get; set; }

    public string? ESIC_No { get; set; }

    public string? UAN_No { get; set; }

    public string? Tax_Regime { get; set; }

    public string? Employeement_Type_UUID { get; set; }

    public string? Shift { get; set; }

    public TimeOnly? Shift_Start_Time { get; set; }

    public TimeOnly? Shift_End_Time { get; set; }

    public TimeOnly? Lunch_Start_Time { get; set; }

    public TimeOnly? Lunch_End_Time { get; set; }

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

    public string? Employee_UUID { get; set; }
}
