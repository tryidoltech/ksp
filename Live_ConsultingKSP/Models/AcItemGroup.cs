﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class AcItemGroup
{
    public decimal ItemGroup_Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Mode { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Group_Name { get; set; }

    [Required(ErrorMessage = "Required!")]
    public string? Group_Code { get; set; }

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