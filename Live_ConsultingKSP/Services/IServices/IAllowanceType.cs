using Azure.Core;
using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IAllowanceType
    {
        void AddAllowanceType(AcAllowanceType acAllowance);
        List<AcAllowanceType> GetAllAllowanceType(string cmpuuid, string envuuid);
        AcAllowanceType GetByAllowanceType(Guid uuid);
        void UpdateAllowanceType(AcAllowanceType acAllowance);
        void DeleteAllowanceType(Guid uuid);
    }
}
