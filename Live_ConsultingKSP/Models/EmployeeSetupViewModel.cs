namespace Live_ConsultingKSP.Models
{
    public class EmployeeSetupViewModel
    {
        public MasterEmployee HrSetup { get; set; } = new MasterEmployee();
        public MasterEmployeeDocumentSetup DocumentSetup { get; set; } = new MasterEmployeeDocumentSetup();
        public List<MasterEmployeeDocumentSetup> DocumentSetupList { get; set; } = new List<MasterEmployeeDocumentSetup>();
        public MasterEmployeeLeaveAuthorisation LeaveAuthorisation { get; set; } = new MasterEmployeeLeaveAuthorisation();
        public MasterEmployeeAssestsDetails Assests { get; set; } = new MasterEmployeeAssestsDetails();
        public MasterEmployeeEducationDetails Education { get; set; } = new MasterEmployeeEducationDetails();
        public MasterEmployeeExperienceDetail Experience { get; set; } = new MasterEmployeeExperienceDetail();
    }
}
