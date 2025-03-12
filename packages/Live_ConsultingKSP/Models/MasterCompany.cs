﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterCompany
{
    public decimal CompanyId { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CompanyName { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Company_ShortName { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? GSTIN_Number { get; set; }

    public string? Uuid { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? ContactPersonName_Sales { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? ContactPersonName_Support { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateOnly? DateOf_Establishment { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Email_Id_Sales { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Email_Id_Support { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Email_Id_Personal { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CountryName_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? StateName_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? CityName_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Address1 { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Address2 { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Phone_Number { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Alternate_PhoneNumber { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Mobile_Number { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Alternate_Mobile_Number { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? URL1 { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? URL2 { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Logo { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Signature { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Stamp { get; set; }
    [Required(ErrorMessage = "Required!")]
    public bool IsDisplay { get; set; }

    public bool IsActive { get; set; }

    public DateTime? IsAdddedOn { get; set; }

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
