using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHraNomineeService
    {
        void AddNominee(HraNominee nominee);
        List<HraNominee> GetAllNominees();
        HraNominee GetNomineeByUUID(string uuid);
        void UpdateNominee(HraNominee nominee);
        void DeleteNominee(string uuid);
    }
}
