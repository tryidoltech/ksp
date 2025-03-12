using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class ProjectCreateProjectPhase
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Project_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Phase_Name { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Project_Description { get; set; }

    [Required(ErrorMessage = "Required!")]
    //[Range(0, double.MaxValue, ErrorMessage = "Enter a valid decimal number!")]

    public decimal? Phase_Cost { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Employee_UUID { get; set; }


    public string? PhaseProgress { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? Start_Date { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? End_Date { get; set; }

    [Required(ErrorMessage = "Required!")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed!")]

    public string? Phase_Expected_Hours { get; set; }

    public string? Select_Team_Member_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Select_Team_Member_Name { get; set; }

    public bool? Is_Project_Phase_Linked_with_Invoice { get; set; }

    public string? Invoice_UUID { get; set; }

    public string? Remark { get; set; }

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
