using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class BdCommunicationHistory
{
    public decimal CommunicationHistory_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    
    public string? History_Type_Name { get; set; }

    public bool IsDocumentMandatory { get; set; }

    public bool Is_Data_Required { get; set; }
    [Required(ErrorMessage = "Required!")]

    public string? Color_Code { get; set; }

    public bool IsDisplay { get; set; }

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
