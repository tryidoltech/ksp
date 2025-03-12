using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models;

public partial class TimesheetMdl
{

    public string HeaderUUID { get; set; }
    public string EmpUUID { get; set; }

    public string DateRangeV { get; set; }
    public string From_Date { get; set; }


    public string To_Date { get; set; }
    public string EmpName { get; set; }


    public string Total_Hours { get; set; }


    public string Timesheet_Status { get; set; }

}
