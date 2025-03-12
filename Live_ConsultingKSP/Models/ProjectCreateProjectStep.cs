using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class ProjectCreateProjectStep
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Project_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? ProjectPhase_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Project_Step_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Required!")]
    //[Range(0, double.MaxValue, ErrorMessage = "Enter a valid decimal number!")]

    public string? Step_Cost { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Start_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? End_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed!")]

    public string? Total_Hours { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Select_Team_Member { get; set; }

    public string? Select_Team_Member_UUID { get; set; }

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
