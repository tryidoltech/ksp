using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class AcsalesManageInquiry
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Customer_UUID { get; set; }

    public string? Country_UUID { get; set; }

    public string? Currency_UUID { get; set; }

    public DateOnly? Order_Date { get; set; }

    public DateOnly? RequestedDelivery_Date { get; set; }

    public string? Project_Name { get; set; }

    public decimal? Inquiry_No { get; set; }

    public string? ItemType_UUID { get; set; }

    public string? ItemName_UUID { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit_UUID { get; set; }

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
