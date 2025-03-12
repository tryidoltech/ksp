namespace Live_ConsultingKSP.Models
{
    public class EmployeeSetupViewModel
    {
        public MasterEmployeeHrSetup HrSetup { get; set; } = new MasterEmployeeHrSetup();
        public MasterEmployeeDocumentSetup DocumentSetup { get; set; } = new MasterEmployeeDocumentSetup();
        public List<MasterEmployeeDocumentSetup> DocumentSetupList { get; set; } = new List<MasterEmployeeDocumentSetup>();
        public MasterEmployeeLeaveAuthorisation LeaveAuthorisation { get; set; } = new MasterEmployeeLeaveAuthorisation();
    }
}
