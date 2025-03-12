using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models
{
    public class MasterEmployeeEducationDetails
    {
        public decimal Id { get; set; }

       
        public string? UUID { get; set; }

      
        public string? Master_Company_UUID { get; set; }

     
        public string? Master_Environment_UUID { get; set; }

        public string? Education_Name { get; set; }

      
        public string? School_UniversityName { get; set; }

      
        public DateTime? Start_Date { get; set; }

     
        public DateTime? End_Date { get; set; }

     
        public decimal? Percentage_Grade { get; set; }

     
        public string? Marksheet_File { get; set; }

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

       
        public string? Employee_UUID { get; set; }
    }
}
