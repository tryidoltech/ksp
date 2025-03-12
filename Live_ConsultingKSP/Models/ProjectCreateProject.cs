using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Live_ConsultingKSP.Models;

public partial class ProjectCreateProject
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Project_Title { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Customer_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Employee_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Project_Description { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Start_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? End_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed!")]
    public string? Expected_Total_Hours { get; set; }
    [Required(ErrorMessage = "Required!")]
    //[Range(0, double.MaxValue, ErrorMessage = "Enter a valid decimal number!")]
    public string? Project_Cost { get; set; }

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
    public DateTime? Actual_Start_Date { get; set; }

    public DateTime? Actual_End_Date { get; set; }

    public string? Actual_Total_Hours { get; set; }

    public string? Project_Status_UUID { get; set; }
}