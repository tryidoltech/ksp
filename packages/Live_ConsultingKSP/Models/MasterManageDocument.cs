using System;
using System.Collections.Generic;

namespace Live_ConsultingKSP.Models;

public partial class MasterManageDocument
{
    public decimal Id { get; set; }

    public string? UUID { get; set; }

    public string? Master_Company_UUID { get; set; }

    public string? Master_Environment_UUID { get; set; }

    public string? LibraryCategory_UUID { get; set; }

    public string? LibraryCategoryTag_UUID { get; set; }

    public string? LibraryDocumentCategory_UUID { get; set; }

    public string? LibraryDocumentCategoryTag_UUID { get; set; }

    public string? Year_UUID { get; set; }

    public string? Document_Titile { get; set; }

    public string? Document_Upload { get; set; }

    public bool? IsRenewable { get; set; }

    public DateOnly? Next_RenewableDate { get; set; }

    public decimal? RemindMeBefore_Days { get; set; }

    public DateOnly? NextReminder_Date { get; set; }

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
