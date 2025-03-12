using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterEmployeeLeaveAuthorisation
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Branch_Name_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Employee_Name_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Leave_Type_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public decimal? No_Of_Leave_Allowed { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? EndDate { get; set; }

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

    public string? Employee_UUID { get; set; }
}
