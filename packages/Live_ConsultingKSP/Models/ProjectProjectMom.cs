using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class ProjectProjectMom
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Company { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? Meeting_Date { get; set; }

    [Required(ErrorMessage = "Required!")]
    public TimeSpan? Meeting_Time { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Meeting_Type { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Meeting_Attendees_from_Client_Side { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Attendees_from_Our_Company_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Attendees_from_Our_Company_Name { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Meeting_Ajenda { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Meeting_Document { get; set; }

    public bool Is_Any_Further_Meeting_Schedule { get; set; }

    [Required(ErrorMessage = "Required!")]
    public DateTime? Next_Meeting_Date { get; set; }

    [Required(ErrorMessage = "Required!")]
    public TimeSpan? Next_Meeting_Time { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Next_Meeting_Type { get; set; }

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
