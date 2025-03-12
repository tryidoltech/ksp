﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class AcPaymentMeansCode
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Code { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? PaymentMeansCode_Name { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Usage_in_EN16931 { get; set; }

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
