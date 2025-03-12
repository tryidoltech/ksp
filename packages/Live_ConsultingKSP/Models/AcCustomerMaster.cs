using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class AcCustomerMaster
{
    public decimal CustomerMaster_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? CustomerMaster_Name { get; set; }

    public string? CustomerEmail_Id { get; set; }

    public decimal? Mobile_No { get; set; }

    public decimal? Landline_No { get; set; }

    public string? Company_Name { get; set; }

    public string? Company_Registration_No { get; set; }

    public decimal? Tax_No { get; set; }

    public string? Currency_UUID { get; set; }

    public string? Billing_Building_No { get; set; }

    public string? Billing_Street_1 { get; set; }

    public string? Billing_Street_2 { get; set; }

    public string? Billing_Postal_Code { get; set; }

    public string? Billing_Country_UUID { get; set; }

    public string? Billing_State_UUID { get; set; }

    public string? Billing_City_UUID { get; set; }

    public bool? IsShippingAddressSame { get; set; }

    public string? Shipping_Building_No { get; set; }

    public string? Shipping_Street_1 { get; set; }

    public string? Shipping_Street_2 { get; set; }

    public string? Shipping_Postal_Code { get; set; }

    public string? Shipping_Country_UUID { get; set; }

    public string? Shipping_State_UUID { get; set; }

    public string? Shipping_City_UUID { get; set; }

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
