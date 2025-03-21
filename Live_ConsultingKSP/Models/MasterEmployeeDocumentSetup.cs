﻿using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterEmployeeDocumentSetup
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? Document_Type_UUID { get; set; }

    public string? Document_Name_UUID { get; set; }

    public DateTime? Valid_From { get; set; }
    public DateTime? Valid_To { get; set; }
    public DateTime? Renew_Date { get; set; }
    public string? Document_File { get; set; }
    public string? Country_Visa { get; set; }
    public string? Visa_Type { get; set; }
    public string? Remark { get; set; }
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

    public string? Employee_UUID { get; set; }
}
