using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class TimesheetLine
{
    
    public decimal ID { get; set; }

    
    public string UUID { get; set; }

   
    public string TimesheetHeader_UUID { get; set; }

    
    public string Project_UUID { get; set; }

   
    public string Task_UUID { get; set; }

   
    public string Day_of_Week { get; set; }

    
    public DateTime? Date_of_Task { get; set; }

   
    public string Hours { get; set; }

   
    public string Remark { get; set; }

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
