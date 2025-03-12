using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class ProjectProjectMeeting
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Company_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Contact_Person_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? IndustrySector_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Meeting_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    public TimeSpan? Meeting_Time { get; set; }

    public string? Meeting_Document { get; set; }

    public string? Attendees_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Attendees_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Description { get; set; }

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
