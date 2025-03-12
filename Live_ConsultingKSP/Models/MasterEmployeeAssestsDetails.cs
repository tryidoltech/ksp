using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Live_ConsultingKSP.Models
{
    public class MasterEmployeeAssestsDetails
    {
        public decimal Id { get; set; }

        public string? UUID { get; set; }

        
        public string? Master_Company_UUID { get; set; }

      
        public string? Master_Environment_UUID { get; set; }

       
        public string? Assets_Name_UUID { get; set; }

      
        public DateTime? Date_of_Issue { get; set; }

   
        public string? Assests_Code { get; set; }

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
