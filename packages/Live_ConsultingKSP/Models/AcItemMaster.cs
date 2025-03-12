using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class AcItemMaster
{
    public decimal ItemMaster_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? ItemGroup_UUID { get; set; }

    public string? Mode { get; set; }

    public string? Item_Name { get; set; }

    public string? Purchase_Unit_UUID { get; set; }

    public string? Item_Code { get; set; }

    public string? Sales_Unit_UUID { get; set; }

    public string? Description { get; set; }

    public string? Pack_Unit_UUID { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

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
