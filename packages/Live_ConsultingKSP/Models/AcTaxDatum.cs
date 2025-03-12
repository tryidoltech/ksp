﻿using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class AcTaxDatum
{
    public decimal TaxData_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? TaxGroup_UUID { get; set; }

    public string? TaxCode_UUID { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? From_Date { get; set; }

    public DateTime? To_Date { get; set; }

    public bool? IsReserve_Charged { get; set; }

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
