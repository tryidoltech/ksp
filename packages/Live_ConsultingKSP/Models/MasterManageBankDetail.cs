using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class MasterManageBankDetail
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Employee_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Bank_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Account_UUID { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Printed_NameOn_Passbook { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? BankAC_Number { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? IFSC_Code { get; set; }
    [Required(ErrorMessage = "Required!")]
    public DateTime? Opening_Date { get; set; }
    [Required(ErrorMessage = "Required!")]
    public string? Remark { get; set; }

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
