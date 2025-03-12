using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class AcItemMaster
{
    public decimal ItemMaster_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? ItemGroup_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Mode { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Item_Name { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Purchase_Unit_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Item_Code { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Sales_Unit_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Pack_Unit_UUID { get; set; }
    public string? HSN_SAC_No { get; set; }

    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

    public string? IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

    public string? IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

    public string? IsDeletedBy { get; set; }

    public string? AddedIP { get; set; }

    public string? UpdatedIP { get; set; }

    public string? DeletedIP { get; set; }

    public decimal? RecordNo { get; set; }
}
