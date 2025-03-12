using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class TimesheetHeader
{

    public decimal ID { get; set; }

  
    public string UUID { get; set; }

    public string Emp_UUID { get; set; }

    
    public DateTime? From_Date { get; set; }

    
    public DateTime? To_Date { get; set; }

   
    public string Total_Hours { get; set; }

   
    public string Timesheet_Status { get; set; }

    
    public string Restriction_Reason { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IsAddedOn { get; set; }

 
    public string IsAddedBy { get; set; }

    public DateTime? IsUpdatedOn { get; set; }

 
    public string IsUpdateBy { get; set; }

    public DateTime? IsDeleteOn { get; set; }

 
    public string IsDeletedBy { get; set; }

 
    public string AddedIP { get; set; }

  
    public string UpdatedIP { get; set; }

 
    public string DeletedIP { get; set; }

   
    public decimal? RecordNo { get; set; }
}
